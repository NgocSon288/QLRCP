using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fDetailFilmUC : UserControl
    {
        private readonly ICategoryFilmService _categoryFilmService;
        private readonly IRoomFilmService _roomFilmService;
        private readonly IRoomService _roomService;
        private readonly IOrderService _orderService;
        private readonly IFilmService _filmService;

        private Film film;
        private List<RoomFilm> roomFilms;
        private List<Room> rooms;
        private RoomFilm roomFilmSelected;
        private Room roomSelected;
        private List<string> seatSelected;

        private PREVIOUS_FROM previousForm;

        public fDetailFilmUC(Film film, PREVIOUS_FROM pre = PREVIOUS_FROM.DETAIL)
        {
            InitializeComponent();

            _categoryFilmService = new CategoryFilmService();
            _roomFilmService = new RoomFilmService();
            _roomService = new RoomService();
            _orderService = new OrderService();
            _filmService = new FilmService();

            this.film = film;
            this.previousForm = pre;

            Load();
        }

        #region Envents

        private async void button1_Click(object sender, EventArgs e)
        {
            if (Constants.CurrentMember == null)
            {
                MessageBox.Show("Xin vui lòng đăng nhập!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var roomID = (cbpRoom.SelectedValue as Room).ID;
            var filmID = film.ID;
            seatSelected = new List<string>();
            var seat = Converters<object>.StringToDictionary(_roomFilmService.GetByMultiID(roomID, filmID).Seat);


            fSeat fSeat = new fSeat(seatSelected, seat, (int)numCount.Value);

            Constants.MainForm.Hide();
            fSeat.ShowDialog();
            Constants.MainForm.Show();
            // gọi form fSeat chọn ghế

            if(seatSelected.Count <= 0)
            {
                return;
            }

            seatSelected.Sort();
            roomFilmSelected.Seat = Converters<object>.DictionaryToString(seat);

            string mess = string.Format($"Bạn có muốn đặt {numCount.Value.ToString()} vé.\nMã phòng: {roomSelected.Name}.\nTổng tiền là: {lblTotalPrice.Text}");
            if (MessageBox.Show(mess, "Xác nhận đặt vé", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                await Order();

                UIHelper.ShowControl(new fDetailFilmUC(film, previousForm), panelContent);
            }
        }

        private void cbpRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            roomSelected = (sender as ComboBox).SelectedValue as Room;
            roomFilmSelected = roomFilms.FirstOrDefault(rf => rf.RoomID == roomSelected.ID);

            LoadOrderInfo();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (previousForm)
            {
                case PREVIOUS_FROM.HOME:
                    UIHelper.ShowControl(new fHomeUC(), panelContent);
                    break;
                case PREVIOUS_FROM.FILM:
                    UIHelper.ShowControl(new fFilmUC(), panelContent);
                    break;
                default:
                    UIHelper.ShowControl(new fDetailFilmUC(film), panelContent);
                    break;
            }
        }

        #endregion Envents

        #region Methods

        new private void Load()
        {
            roomFilms = _roomFilmService.GetByFilmID(film.ID);
            rooms = _roomService.GetByListID(roomFilms.Select(rf => rf.RoomID).ToList());
            roomFilmSelected = roomFilms.Count > 0 ? roomFilms[0] : null;
            roomSelected = rooms.Count > 0 ? rooms[0] : null;

            imgMain.BackgroundImage = new Bitmap("../../Resources/" + film.Image);
            lblDirector.Text = film.Director;
            lblNational.Text = film.National;
            lblLanguage.Text = film.Language;
            lblYear.Text = film.CreatedYear.ToString();
            lblTimeLong.Text = film.TimeLong.ToString() + " phút";
            lblCategory.Text = _categoryFilmService.GetByID(film.CategoryFilmID).Name;
            lblDateShow.Text = roomSelected != null ? roomFilmSelected.DateShow.ToShortDateString() : "Chưa có lịch chiếu";
            lblOrderCount.Text = film.OrderCount.ToString();
            lblPrice.Text = film.Price.ToString("#,##") + " VNĐ";
            lblDescription.Text = "\t" + film.Description;

            lblTitleFilmName.Text = film.Name.ToUpper();
            pnlTitleFilmName.Width = lblTitleFilmName.Width;

            LoadOrderInfo();
        }

        private void LoadOrderInfo()
        {
            if (IsSortOut())
            {
                pnlOrder.Visible = false;
                return;
            }

            cbpRoom.DataSource = rooms;
            cbpRoom.DisplayMember = "Name";

            lblDateShow.Text = roomSelected != null ? roomFilmSelected.DateShow.ToShortDateString() : "Chưa có lịch chiếu";

            lblSeatCount.Text = (roomSelected.SeatMax - roomSelected.SeatCount).ToString();

            numCount.Minimum = 1;
            numCount.Value = 1;
            numCount.Maximum = roomSelected.SeatMax - roomSelected.SeatCount;
            lblTotalPrice.Text = film.Price.ToString("#,##") + " VNĐ";
        }

        private bool IsSortOut()
        {
            return rooms.All(r => r.SeatCount >= r.SeatMax) || rooms.Count <= 0;
        }

        private async Task Order()
        {
            pnlWait.Visible = true;
            btnOrder.Enabled = false;
            btnOrder.Cursor = Cursors.No;

            var count = numCount.Value;
            var category = _categoryFilmService.GetByID(film.CategoryFilmID);

            // cập nhật số  lượng ghế ngồi RoomFilm
            roomSelected.SeatCount += (int)count;
            _roomService.Update(roomSelected);

            // cập nhật ghế ngồi đã được chọn
            _roomFilmService.Update(roomFilmSelected);

            // Tạo Order
            var order = new Order(roomSelected.ID, film.ID, film.Name, category.Name, film.TimeLong, roomFilmSelected.DateShow, (int)count, film.Price, string.Join(", ", seatSelected));
            _orderService.Insert(order);

            // tăng số lượng mua của film
            film.OrderCount += order.Count;
            _filmService.Update(film);

            // Gửi order qua mail
            await SendMail(order);

            btnOrder.Enabled = true;
            pnlWait.Visible = false;
            btnOrder.Cursor = Cursors.Hand;

            // thông báo đã gửi qua mail
            MessageBox.Show("Thông tin hóa đơn đã được gửi qua mail. \nxin quý khách vui lòng kiểm tra lại mail!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task SendMail(Order order)
        {
            var content = File.ReadAllText("./../../Assets/Template/neworder.html");

            content = content.Replace("{{MemberName}}", Constants.CurrentMember.Name);
            content = content.Replace("{{OrderID}}", order.ID.ToString());
            content = content.Replace("{{Name}}", order.FilmName);
            content = content.Replace("{{Category}}", order.CategoryFilmName);
            content = content.Replace("{{Price}}", order.Price.ToString("#,##") + " VNĐ");
            content = content.Replace("{{Count}}", order.Count.ToString());
            content = content.Replace("{{DateShow}}", order.DateShow.ToShortDateString());
            content = content.Replace("{{TotalPrice}}", (order.Count * order.Price).ToString("#,##") + " VNĐ");
            content = content.Replace("{{Seat}}", string.Join(", ", seatSelected));

            await MailHelper.SendMail(Constants.CurrentMember.Email, "Đơn hàng mới từ CINEMA", content);
        }

        #endregion Methods

        #region UI

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;

            btn.BackColor = Color.FromArgb(255, 34, 101);
            btn.ForeColor = Color.White;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;

            btn.BackColor = Color.FromArgb(40, 40, 40);
            btn.ForeColor = Color.FromArgb(255, 34, 101);
        }

        private void numCount_ValueChanged(object sender, EventArgs e)
        {
            var numberUD = sender as NumericUpDown;
            var price = film.Price;

            lblTotalPrice.Text = (numberUD.Value * price).ToString("#,##") + " VNĐ";
        }

        private void btnStaffMana_MouseEnter(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(34, 34, 34);
            btnBack.IconColor = Color.FromArgb(255, 34, 101);
            btnBack.ForeColor = Color.FromArgb(255, 34, 101);
        }

        private void btnStaffMana_MouseLeave(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(40, 40, 40);
            btnBack.IconColor = Color.FromArgb(68, 226, 255);
            btnBack.ForeColor = Color.FromArgb(68, 226, 255);
        }

        #endregion UI
    }
}