using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fFilmUC : UserControl
    {
        private readonly IFilmService _filmService;
        private readonly IRoomFilmService _roomFilmService;
        private readonly ICategoryFilmService _categoryFilmService;

        private List<Film> data;
        private IEnumerable<Film> films;

        private Button pageSelected;

        // Biến điều kiện
        private int pageCurrent = 1;

        private List<Guid> categoryListActive = new List<Guid>();
        private string keyWord = "";

        public fFilmUC()
        {
            InitializeComponent();

            _filmService = new FilmService();
            _roomFilmService = new RoomFilmService();
            _categoryFilmService = new CategoryFilmService();

            Load();
        }

        #region Events

        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            pageCurrent = Convert.ToInt32(btn.Tag.ToString());

            FilterFilmByCondition(3, pageCurrent, categoryListActive, keyWord);

            // PageSelected đã được gán trong đây
            LoadActive(btn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keyWord = txtKeyWord.Text;

            FilterFilmByCondition(3, 1, categoryListActive, keyWord);

            LoadPagination();
        }

        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                keyWord = txtKeyWord.Text;

                FilterFilmByCondition(3, 1, categoryListActive, keyWord);

                LoadPagination();
            }
        }

        private void Btn_Click1(object sender, EventArgs e)
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

            FilterFilmByCondition(3, 1, categoryListActive, keyWord);

            LoadPagination();
        }

        private void img1_Click(object sender, EventArgs e)
        {
            var ptb = sender as PictureBox;
            var lbl = sender as Label;

            var film = ptb != null ? ptb.Tag as Film : lbl.Tag as Film;

            UIHelper.ShowControl(new fDetailFilmUC(film, PREVIOUS_FROM.FILM), panelContent);
        }

        #endregion Events

        #region Methods

        new private void Load()
        {
            data = _filmService.GetAll();

            // Load categoryFilm
            LoadCategoryFilm();

            // Load trang đầu tiên của film
            FilterFilmByCondition();

            // Sau khi có films
            LoadPagination();
        }

        private void LoadCategoryFilm()
        {
            var categories = _categoryFilmService.GetAll();

            foreach (var item in categories)
            {
                var btn = new Button()
                {
                    Width = Constants.WIDTH_CATEGORY_BUTTON,
                    Height = Constants.HEIGHT_CATEGORY_BUTTON,
                    Text = item.Name,
                    ForeColor = Constants.ACTIVE_CATEGORY_BUTTON_BG_COLOR,
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(40, 5, 80, 5)
                };

                btn.FlatAppearance.BorderSize = 2;
                btn.FlatAppearance.BorderColor = Constants.ACTIVE_CATEGORY_BUTTON_BG_COLOR;
                btn.Click += Btn_Click1;
                btn.Tag = item.ID;
                btn.Font = new Font(btn.Font.FontFamily, Constants.FONTSIZE_CATEGORY_BUTTON);
                flpCategory.Controls.Add(btn);
            }
        }

        private void LoadPagination()
        {
            flpPagination.Controls.Clear();

            var count = films.Count() + 2;
            for (int i = 1; i <= count / 3; i++)
            {
                var btn = new Button()
                {
                    Height = Constants.WIDTH_PAGINATION,
                    Width = Constants.HEIGHT_PAGINATION,
                    Text = i.ToString(),
                    ForeColor = Constants.ACTIVE_PAGINATION_BG_COLOR,
                    Cursor = Cursors.Hand,
                    Padding = new Padding(5),
                    FlatStyle = FlatStyle.Flat
                };

                btn.FlatAppearance.BorderSize = 0;
                btn.Tag = i;

                btn.Click += Btn_Click;

                if (i == 1)
                {
                    LoadActive(btn);
                }

                flpPagination.Controls.Add(btn);
            }
        }

        private void LoadActive(Button btn = null)
        {
            // Set lại color cho button vừa active
            if (pageSelected != null)
            {
                pageSelected.BackColor = Constants.LEAVE_PAGINATION_BG_COLOR;
                pageSelected.BackColor = Constants.LEAVE_PAGINATION_BG_COLOR;
            }

            // Set button mới
            pageSelected = btn;
            pageSelected.BackColor = Constants.ACTIVE_PAGINATION_BG_COLOR;
            pageSelected.ForeColor = Color.White;
        }

        private void FilterFilmByCondition(int pageSize = 3, int page = 1, List<Guid> category = null, string keyWord = "")
        {
            // Lọc theo categoryFilm
            if (category != null && category.Count > 0)
            {
                films = data.Where(f => category.Any(c => c == f.CategoryFilmID));
            }
            else
            {
                films = data;
            }

            // Lọc theo keyWord
            if (!string.IsNullOrEmpty(keyWord) && !string.IsNullOrWhiteSpace(keyWord))
            {
                var keyConnvert = CompareStringHelper.Convert(keyWord.Trim()).ToUpper();
                films = films.Where(f => CompareStringHelper.Convert(f.Name).ToUpper().Contains(keyConnvert));
            }

            try
            {
                LoadFilmData(films.Skip((page - 1) * pageSize).Take(pageSize).ToList());
            }
            catch (Exception)
            {
                LoadFilmData(films.Skip((page - 1) * pageSize).ToList());
            }

            lblCount.Text = $"{films.Count()} phim được tìm thấy";
        }

        private void LoadFilmData(List<Film> films)
        {
            // load image 1
            if (films.Count >= 1)
            {
                var rf = _roomFilmService.GetByFilmID(films[0].ID);
                img1.Tag = films[0];
                lblName1.Tag = films[0];
                img1.BackgroundImage = new Bitmap("../../Resources/" + films[0].Image);
                lblName1.Text = films[0].Name.ToUpper().Replace("-", "-\n");
                lblTime1.Text = rf.Count > 0 ? "Ngày chiếu: " + rf[0].DateShow.ToShortDateString() : "Chưa được công chiếu";
                pnlWrapFilm1.Visible = true;
            }
            else
            {
                pnlWrapFilm1.Visible = false;
            }

            if (films.Count >= 2)
            {
                var rf = _roomFilmService.GetByFilmID(films[1].ID);
                img2.Tag = films[1];
                lblName2.Tag = films[1];
                img2.BackgroundImage = new Bitmap("../../Resources/" + films[1].Image);
                lblName2.Text = films[1].Name.ToUpper().Replace("-", "-\n");
                lblTime2.Text = rf.Count > 0 ? "Ngày chiếu: " + rf[0].DateShow.ToShortDateString() : "Chưa được công chiếu";
                pnlWrapFilm2.Visible = true;
            }
            else
            {
                pnlWrapFilm2.Visible = false;
            }

            if (films.Count >= 3)
            {
                var rf = _roomFilmService.GetByFilmID(films[2].ID);
                img3.Tag = films[2];
                lblName3.Tag = films[2];
                img3.BackgroundImage = new Bitmap("../../Resources/" + films[2].Image);
                lblName3.Text = films[2].Name.ToUpper().Replace("-", "-\n");
                lblTime3.Text = rf.Count > 0 ? "Ngày chiếu: " + rf[0].DateShow.ToShortDateString() : "Chưa được công chiếu";
                pnlWrapFilm3.Visible = true;
            }
            else
            {
                pnlWrapFilm3.Visible = false;
            }
        }

        #endregion Methods

        #region UI

        [Obsolete]
        private void img1_MouseHover(object sender, EventArgs e)
        {
            (sender as PictureBox).Scale((float)1.1);
        }

        [Obsolete]
        private void img1_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Scale((float)(1.0 * 10 / 11));
        }

        private void lblName1_MouseEnter(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Constants.ACTIVE_LABEL_COLOR;
        }

        private void lblName1_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Constants.LEAVE_LABEL_COLOR;
        }

        private void lblName1_Click(object sender, EventArgs e)
        {
            UIHelper.ShowControl(new fDetailFilmUC((sender as PictureBox)?.Tag as Film), panelContent);
        }

        #endregion UI
    }
}