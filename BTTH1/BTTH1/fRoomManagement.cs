using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fRoomManagement : UserControl
    {
        private readonly IRoomService _roomService;
        private readonly IRoomFilmService _roomFilmService;
        private readonly IFilmService _filmService;

        private Room roomSelected;
        private Panel panelSelected;
        private RoomFilm roomFilmSelected;
        private ListViewItem roomFilmSelectedRow;
        private RoomFilm newRoomFilm;
        private int id;

        public int seatMax = 0;

        public fRoomManagement()
        {
            InitializeComponent();

            this._roomService = new RoomService();
            this._roomFilmService = new RoomFilmService();
            this._filmService = new FilmService();

            roomSelected = new Room();
            panelSelected = new Panel();

            Load();
        }

        #region Events

        private void Btn_Click(object sender, EventArgs e)
        {
            var con = sender as Control;

            if (con == null)
            {
                return;
            }

            var parent = con.Parent as Panel;

            roomSelected = parent.Tag as Room;

            panelSelected.BorderStyle = BorderStyle.None;
            panelSelected.BackColor = Constants.LEAVE_BUTTON_BACKGROUND_ROOM_SELECTED;
            parent.BorderStyle = BorderStyle.Fixed3D;
            parent.BackColor = Constants.ACTIVE_BUTTON_BACKGROUND_ROOM_SELECTED;
            panelSelected = parent;

            LoadRoomFilm();
            LoadRoomFilmDetail();
        }

        private void LvFilm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFilm.SelectedItems.Count <= 0)
            {
                return;
            }

            roomFilmSelected = lvFilm.SelectedItems[0].Tag as RoomFilm;
            roomFilmSelectedRow = lvFilm.SelectedItems[0];

            var film = _filmService.GetByID(roomFilmSelected.FilmID);

            LoadRoomFilmDetail();

        }

        private void CbbFilms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFilms.SelectedItem == null)
            {
                return;
            }

            var rf = cbbFilms.SelectedItem as Film;

            newRoomFilm = new RoomFilm() { FilmID = rf.ID, DateShow = dtpDateShow.Value };
        }

        private void btnUpdateRoomFilm_Click(object sender, EventArgs e)
        { 
            if (lvFilm.Items.Count <= 0)
            {
                MessageBox.Show("Không có gì để cập nhật hết");

                return;
            }

            if (!IsValidValue() || dtpDateShow.Value <= DateTime.Now)
            {
                MessageBox.Show("Dữ liệu không hợp lệ");

                return;
            }

            if (MessageBox.Show("Bạn có muốn cập nhật!", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (roomFilmSelected.FilmID != (cbbFilms.SelectedItem as Film).ID || roomFilmSelected.DateShow.ToString().Split(' ')[0] != dtpDateShow.Value.ToString().Split(' ')[0])
                {
                    roomFilmSelected.FilmID = (cbbFilms.SelectedItem as Film).ID;
                    roomFilmSelected.DateShow = dtpDateShow.Value;

                    roomFilmSelectedRow.SubItems[1].Text = _filmService.GetByID(roomFilmSelected.FilmID).Name;
                    roomFilmSelectedRow.SubItems[2].Text = roomFilmSelected.DateShow.ToString();

                    // update bd
                    _roomFilmService.Update(roomFilmSelected);
                }

                MessageBox.Show("Cập nhật thành công!");
            }

        }

        private void dtpDateShow_ValueChanged(object sender, EventArgs e)
        {
            var id = newRoomFilm.ID;
            newRoomFilm = new RoomFilm() { FilmID = id, DateShow = dtpDateShow.Value };
        }

        private void btnCreateRoomFilm_Click(object sender, EventArgs e)
        {
            if (dtpDateShow.Value <= DateTime.Now)
            {
                MessageBox.Show("Dữ liệu không hợp lệ");

                return;
            }

            if (roomFilmSelected != null)
            {
                var c = roomFilmSelected.FilmID;
                var d = cbbFilms.SelectedItem as Film;
                var a = roomFilmSelected.FilmID == (cbbFilms.SelectedItem as Film).ID;

                var b = (roomFilmSelected.FilmID == (cbbFilms.SelectedItem as Film).ID && roomFilmSelected.DateShow.ToString().Split(' ')[0] == dtpDateShow.Value.ToString().Split(' ')[0]);

                if ((!IsValidValue() || (roomFilmSelected.FilmID == (cbbFilms.SelectedItem as Film).ID && roomFilmSelected.DateShow.ToString().Split(' ')[0] == dtpDateShow.Value.ToString().Split(' ')[0])))
                {
                    MessageBox.Show("Dữ liệu không hợp lệ");

                    return;
                }
            }

            if (MessageBox.Show("Bạn có muốn tạo mới xuất chiếu phim", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                newRoomFilm.Seat = "".PadLeft(Convert.ToInt32(txtSeatMax.Text), '1');
                newRoomFilm.RoomID = roomSelected.ID;
                newRoomFilm.FilmID = (cbbFilms.SelectedItem as Film).ID;
                newRoomFilm.DateShow = dtpDateShow.Value;

                // update ui listview
                var item = new ListViewItem();

                item.Text = (id++).ToString();
                item.SubItems.Add(_filmService.GetByID(newRoomFilm.FilmID).Name);
                item.SubItems.Add(newRoomFilm.DateShow.ToString());

                lvFilm.Items.Add(item);



                item.Tag = newRoomFilm;

                // update db
                _roomFilmService.Insert(newRoomFilm);

                LoadRoomFilm();
                roomFilmSelected = newRoomFilm;
                roomFilmSelectedRow = lvFilm.Items[lvFilm.Items.Count - 1];

                MessageBox.Show("Tạo mới thành công!");
            }
        }

        private void btnDeleteRoomFilm_Click(object sender, EventArgs e)
        {
            if(lvFilm.Items.Count <= 0)
            {
                MessageBox.Show("Không có gì để xóa hết");

                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                roomFilmSelected.Status = false;

                _roomFilmService.Update(roomFilmSelected);

                Load(false);
            }
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            // get số ghế từ form
            var f = new fSeatCount(this);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();

            if (seatMax == -1)
                return;


            // thêm vào db
            var rooms = _roomService.GetAll();
            var lastRoom = rooms.Last();
            var name = lastRoom.Name;
            var lastStt = name.Substring(2);
            var stt = Int32.Parse(lastStt);
            stt++;

            var room = new Room("RP" + stt.ToString().PadLeft(3, '0'), seatMax);
            _roomService.Insert(room);

            //// thêm vào ui
            Panel pnl = new Panel();
            pnl.Width = Constants.WIDTH_ROOM;
            pnl.Height = Constants.HEIGHT_ROOM;
            pnl.Margin = new Padding(20);
            pnl.Tag = room;
            pnl.TabStop = false;


            var btn = new Button();
            btn.Width = Constants.WIDTH_ROOM;
            btn.Height = Constants.HEIGHT_ROOM;
            btn.BackgroundImage = new Bitmap(Constants.CINEMA_ICON_PATH);
            btn.BackgroundImageLayout = ImageLayout.Stretch;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.BottomCenter;
            btn.TabStop = false;


            Label lbl = new Label();
            lbl.Text = room.Name;
            lbl.ForeColor = Constants.BUTTON_FORECOLOR_ROOM;
            lbl.Location = new Point((Constants.WIDTH_ROOM - lbl.Width) / 2, Constants.HEIGHT_ROOM - lbl.Height - 10);
            lbl.AutoSize = true;
            lbl.Font = new Font(lbl.Font.FontFamily, 10);
            lbl.BackColor = Constants.MAIN_BACKGROUND;
            lbl.Cursor = Cursors.Hand;

            btn.Click += Btn_Click;
            lbl.Click += Btn_Click;

            pnl.Controls.Add(lbl);
            pnl.Controls.Add(btn);
            flpRoom.Controls.Add(pnl);


        }

        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            // xóa room 
            _roomService.DeleteByID(roomSelected.ID);

            // load lại trang
            Load();
        }

        #endregion

        #region Method

        new private void Load(bool isLoadRoom = true)
        {
            id = 1;
            newRoomFilm = new RoomFilm();
            dtpDateShow.MinDate = DateTime.Now.AddDays(-1);

            if (isLoadRoom)
            {
                LoadRoom();
            }

            LoadRoomFilm();

            LoadRoomFilmDetail();

            lvFilm.SelectedIndexChanged += LvFilm_SelectedIndexChanged;
            cbbFilms.SelectedIndexChanged += CbbFilms_SelectedIndexChanged;

            newRoomFilm.FilmID = roomFilmSelected == null ? default : roomFilmSelected.FilmID;
            newRoomFilm.RoomID = roomFilmSelected == null ? default : roomSelected.ID;
        }

        private void LoadRoom()
        {
            var rooms = _roomService.GetAll();

            flpRoom.Controls.Clear();

            foreach (var item in rooms)
            {
                Panel pnl = new Panel();
                pnl.Width = Constants.WIDTH_ROOM;
                pnl.Height = Constants.HEIGHT_ROOM;
                pnl.Margin = new Padding(20);
                pnl.Tag = item;


                var btn = new Button();
                btn.Width = Constants.WIDTH_ROOM;
                btn.Height = Constants.HEIGHT_ROOM;
                btn.BackgroundImage = new Bitmap(Constants.CINEMA_ICON_PATH);
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.Cursor = Cursors.Hand;
                btn.TextAlign = ContentAlignment.BottomCenter;


                Label lbl = new Label();
                lbl.Text = item.Name;
                lbl.ForeColor = Constants.BUTTON_FORECOLOR_ROOM;
                lbl.Location = new Point((Constants.WIDTH_ROOM - lbl.Width) / 2, Constants.HEIGHT_ROOM - lbl.Height - 10);
                lbl.AutoSize = true;
                lbl.Font = new Font(lbl.Font.FontFamily, 10);
                lbl.BackColor = Constants.MAIN_BACKGROUND;
                lbl.Cursor = Cursors.Hand;

                btn.Click += Btn_Click;
                lbl.Click += Btn_Click;

                pnl.Controls.Add(lbl);
                pnl.Controls.Add(btn);
                flpRoom.Controls.Add(pnl);
            }

            var parent = flpRoom.Controls[0] as Panel;

            roomSelected = parent.Tag as Room;

            panelSelected.BorderStyle = BorderStyle.None;
            panelSelected.BackColor = Constants.LEAVE_BUTTON_BACKGROUND_ROOM_SELECTED;
            parent.BorderStyle = BorderStyle.Fixed3D;
            parent.BackColor = Constants.ACTIVE_BUTTON_BACKGROUND_ROOM_SELECTED;
            panelSelected = parent;
        }

        private void LoadRoomFilm()
        {
            var roomID = roomSelected.ID;
            var roomFilms = _roomFilmService.GetByRoomID(roomID);
            id = 1;

            lvFilm.Items.Clear();
            lvFilm.Columns.Clear();

            lvFilm.Columns.Add("ID", 50, HorizontalAlignment.Left);
            lvFilm.Columns.Add("Tên phim", 530, HorizontalAlignment.Left);
            lvFilm.Columns.Add("Ngày chiếu", 350, HorizontalAlignment.Left);

            foreach (var item in roomFilms)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Tag = item;

                listViewItem.Text = (id++).ToString();
                listViewItem.SubItems.Add(_filmService.GetByID(item.FilmID).Name);
                listViewItem.SubItems.Add(item.DateShow.ToString());

                lvFilm.Items.Add(listViewItem);
            }

            try
            {
                roomFilmSelected = lvFilm.Items[0].Tag as RoomFilm;
                roomFilmSelectedRow = lvFilm.Items[0];
            }
            catch (Exception)
            {
                roomFilmSelected = null;
                roomFilmSelectedRow = null;
            }
        }

        private void LoadRoomFilmDetail()
        {
            var i = 1;
            var idSelect = roomFilmSelected == null ? default : roomFilmSelected.FilmID;
            var films = _filmService.GetAll();
            films.ForEach(f => f.Name = (i++).ToString() + " - " + f.Name.Split('-')[0]);
            cbbFilms.SelectedIndexChanged -= CbbFilms_SelectedIndexChanged;

            lblRoomName.Text = roomSelected.Name;
            cbbFilms.DataSource = films;
            cbbFilms.DisplayMember = "Name";
            cbbFilms.SelectedIndexChanged += CbbFilms_SelectedIndexChanged;
            dtpDateShow.Value = roomFilmSelected == null ? DateTime.Now : roomFilmSelected.DateShow;
            txtSeatMax.Text = roomSelected.SeatMax.ToString();
            txtSeatCount.Text = roomFilmSelected == null ? "0" : GetSeatCount(roomFilmSelected.Seat).ToString(); // sai

            if (roomFilmSelected == null)
            {
                cbbFilms.SelectedIndex = 0;
            }
            else
            {
                cbbFilms.SelectedItem = films.FirstOrDefault(f => f.ID == idSelect);
            }

        }

        private int GetSeatCount(string s)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0') count++;
            }

            return count;
        }

        private bool IsValidValue()
        {
            var filmID = (cbbFilms.SelectedItem as Film).ID;
            var dateShow = dtpDateShow.Value;
            var roomFilms = _roomFilmService.GetByRoomID(roomSelected.ID);

            return !roomFilms.Any(rf => roomFilmSelected.FilmID != rf.FilmID && rf.FilmID == filmID && rf.DateShow == dateShow);
        }

        #endregion

        #region UI

        private void txtSeatMax_Enter(object sender, EventArgs e)
        {
            txtSeatMax.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlSeatMax.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtSeatMax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSeatMax.Text))
            {
                txtSeatMax.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnlSeatMax.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;
            }
            else
            {
                txtSeatMax.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                pnlSeatMax.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
            }
        }

        private void txtSeatCount_Enter(object sender, EventArgs e)
        {
            txtSeatCount.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlSeatCount.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtSeatCount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSeatCount.Text))
            {
                txtSeatCount.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnlSeatCount.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;
            }
            else
            {
                txtSeatCount.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                pnlSeatCount.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;
            }
        }

        #endregion
    }
}
