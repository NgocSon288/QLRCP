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
    public partial class fFilmManagement : UserControl
    {
        private readonly ICategoryFilmService _categoryFilmService;
        private readonly IRoomFilmService _roomFilmService;
        private readonly IFilmService _filmService;

        public fFilmManagement()
        {
            InitializeComponent();

            this._categoryFilmService = new CategoryFilmService();
            this._roomFilmService = new RoomFilmService();
            this._filmService = new FilmService();

            Load();
        }

        private List<Guid> categoryListActive;
        private Dictionary<Guid, string> categoryName;  // cách tăng perfomance, hạn chế truy xuất file
        private Guid currentFilm;
        private ListViewItem currentRow;

        #region Events

        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var cateID = new Guid(btn.Tag.ToString());

            if (btn.ForeColor == Constants.ACTIVE_CATEGORY_BUTTON_BG_COLOR)
            {
                categoryListActive.Add(cateID);
                btn.ForeColor = Constants.LEAVE_CATEGORY_BUTTON_BG_COLOR;
                btn.BackColor = Constants.ACTIVE_CATEGORY_BUTTON_BG_COLOR;
            }
            else
            {
                categoryListActive.RemoveAt(categoryListActive.IndexOf(cateID));
                btn.ForeColor = Constants.ACTIVE_CATEGORY_BUTTON_BG_COLOR;
                btn.BackColor = Constants.LEAVE_CATEGORY_BUTTON_BG_COLOR;
            }

            LoadFilms();
            LoadDetail();
        }

        private void lvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lv = sender as ListView;
            if (lv != null && lv.SelectedItems.Count > 0)
            {
                currentRow = lv.SelectedItems[0];
            }
            if (lv.SelectedItems == null || lv.SelectedItems.Count <= 0)
            {
                return;
            }
            else
            {
                btnDelete.Cursor = Cursors.Hand;
                btnDelete.FlatAppearance.BorderColor = Constants.BUTTON_DELETE_ENABLE_COLOR;
                btnDelete.ForeColor = Constants.BUTTON_DELETE_ENABLE_COLOR;
                btnDelete.IconColor = Constants.BUTTON_DELETE_ENABLE_COLOR;
            }

            currentFilm = new Guid(lv.SelectedItems[0].Tag.ToString());

            LoadDetail();
        }

        /// <summary>
        /// Cập nhật, truyền vào ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrder_Click(object sender, EventArgs e)
        {
            fModifiedFilm f = new fModifiedFilm(this, panelContent, _filmService.GetByID(currentFilm));

            UIHelper.ShowControl(f, panelContent);
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, EventArgs e)
        {
            fModifiedFilm f = new fModifiedFilm(this, panelContent);

            UIHelper.ShowControl(f, panelContent);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvList.SelectedItems.Count <= 0)
                return;

            string mess = string.Format("Bạn có muôn xóa bộ phim:\n{0}", lblName.Text);

            if (MessageBox.Show(mess, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // cập nhật status thành false
                // roomFilm - tìm các roomFilm có filmID == currentFilm && status = true
                var roomFilms = _roomFilmService.GetAllByFilmID(currentFilm);
                roomFilms.ForEach(rf => rf.Status = false);
                _roomFilmService.UpdateRange(roomFilms);

                // film - tìm film có ID = currentID -> status = false
                var film = _filmService.GetByID(currentFilm);
                film.Status = false;
                _filmService.Update(film);

                // Load lại ListView
                Reload(film);

                MessageBox.Show("Xóa thành công!");
            }
        }

        #endregion


        #region Methods

        new public void Load()
        {
            categoryListActive = new List<Guid>();
            categoryName = _categoryFilmService.GetAll().ToDictionary(cf => cf.ID, cf => cf.Name);

            LoadFilter();
            LoadFilms();

            LoadDetail();
            currentRow = lvList.Items[0];
        }

        private void LoadDetail(Guid idFilm = default)
        {
            if (lvList.Items.Count <= 0)
            {
                EmptyData();

                // UI Button delete
                btnDelete.Cursor = Cursors.No;
                btnDelete.FlatAppearance.BorderColor = Constants.BUTTON_DELETE_DISABLE_COLOR;
                btnDelete.ForeColor = Constants.BUTTON_DELETE_DISABLE_COLOR;
                btnDelete.IconColor = Constants.BUTTON_DELETE_DISABLE_COLOR;

                return;
            }

            var id = idFilm;
            if (idFilm == default)
            {
                id = currentFilm;
            }

            var flm = _filmService.GetByID(id);
            var count = flm.OrderCount;
            var price = flm.Price;
            var total = count * price;

            // Load UI
            imgMain.BackgroundImage = new Bitmap("../../Resources/" + flm.Image);
            lblName.Text = flm.Name.ToUpper();
            lblDirector.Text = flm.Director;
            lblNational.Text = flm.National;
            lblLanguage.Text = flm.Language;
            lblYear.Text = flm.CreatedYear.ToString();
            lblTimeLong.Text = flm.TimeLong.ToString();
            lblCategory.Text = categoryName[flm.CategoryFilmID];
            lblOrderCount.Text = flm.OrderCount.ToString();
            lblPrice.Text = flm.Price.ToString("#,##") + " đ";
            lblDirector.Text = flm.Director;
            lblTotal.Text = total == 0 ? "0 đ" : total.ToString("#,##") + " đ";
        }

        private void LoadFilter()
        {
            foreach (var item in categoryName)
            {
                var btn = new Button()
                {
                    Width = Constants.WIDTH_CATEGORY_BUTTON,
                    Height = Constants.HEIGHT_CATEGORY_BUTTON,
                    Text = item.Value,
                    ForeColor = Constants.ACTIVE_CATEGORY_BUTTON_BG_COLOR,
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(40, 5, 80, 5)
                };

                btn.FlatAppearance.BorderSize = 2;
                btn.FlatAppearance.BorderColor = Constants.ACTIVE_CATEGORY_BUTTON_BG_COLOR;
                btn.Tag = item.Key;
                btn.Click += Btn_Click;
                btn.Font = new Font(btn.Font.FontFamily, Constants.FONTSIZE_CATEGORY_BUTTON);
                flpFilter.Controls.Add(btn);
            }
        }

        private void LoadFilms(Guid currentF = default)
        {
            var data = _filmService.GetAll();
            if (categoryListActive != null && categoryListActive.Count > 0)
            {
                data = data.Where(f => categoryListActive.Any(c => c == f.CategoryFilmID)).ToList();
            }
            if (data == null || data.Count <= 0)
            {
                lvList.Items.Clear();
                return;
            }

            // Load UI ListView

            lvList.Items.Clear();
            lvList.Columns.Clear();

            var id = 1;

            lvList.Columns.Add("ID", 50, HorizontalAlignment.Left);
            lvList.Columns.Add("Tên phim", 510, HorizontalAlignment.Left);
            lvList.Columns.Add("Thể loại phim", 200, HorizontalAlignment.Left);
            lvList.Columns.Add("Đạo diễn", 250, HorizontalAlignment.Center);
            lvList.Columns.Add("Quốc gia", 200, HorizontalAlignment.Center);
            lvList.Columns.Add("Lược mua", 200, HorizontalAlignment.Center);
            lvList.Columns.Add("Giá", 200, HorizontalAlignment.Center);
            lvList.Columns.Add("Doanh thu", 250, HorizontalAlignment.Center);

            foreach (var item in data)
            {
                var total = item.OrderCount * item.Price;
                var itemListView = new ListViewItem();
                itemListView.Text = (id++).ToString();
                itemListView.Tag = item.ID;
                itemListView.SubItems.Add(item.Name);
                itemListView.SubItems.Add(categoryName[item.CategoryFilmID]); // dùng Dictionary nhanh hơn nhiều so với việc truy xuất file n lần
                itemListView.SubItems.Add(item.Director);
                itemListView.SubItems.Add(item.National);
                itemListView.SubItems.Add(item.OrderCount.ToString());
                itemListView.SubItems.Add(item.Price.ToString("#,##") + " đ");
                itemListView.SubItems.Add(total == 0 ? "0 đ" : total.ToString("#,##") + " đ");

                lvList.Items.Add(itemListView);
            }

            //currentFilm = currentFilm == default ? data[0].ID : currentFilm; 

            if (data.Any(f => f.ID == currentF))
            {
                currentFilm = currentF != default ? currentF : data[0].ID;
            }
            else
            {
                currentFilm = data[0].ID;
            }
        }

        public void Reload(Film f = null)
        {
            if (f == null)
            {
                Load();
            }
            else
            {
                LoadFilms(currentFilm);
                LoadDetail();
            }
        }

        private void EmptyData()
        {
            // Load UI
            imgMain.BackgroundImage = null;
            lblName.Text = "...";
            lblDirector.Text = "...";
            lblNational.Text = "...";
            lblLanguage.Text = "...";
            lblYear.Text = "...";
            lblTimeLong.Text = "...";
            lblCategory.Text = "...";
            lblOrderCount.Text = "...";
            lblPrice.Text = "...";
            lblDirector.Text = "...";
            lblTotal.Text = "...";
        }

        #endregion
    }
}
