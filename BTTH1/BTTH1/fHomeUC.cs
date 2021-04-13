using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fHomeUC : UserControl
    {
        private readonly IFilmService _filmService;
        private readonly IRoomFilmService _roomFilmService;

        public fHomeUC()
        {
            InitializeComponent();

            _filmService = new FilmService();
            _roomFilmService = new RoomFilmService();

            Load();
        }

        #region Methods

        new private void Load()
        {
            LoadImage();
        }

        private void LoadImage()
        {
            var films = _filmService.GetByTopCount(3);

            img1.BackgroundImage = new Bitmap("../../Resources/" + films[0].Image);
            img2.BackgroundImage = new Bitmap("../../Resources/" + films[1].Image);
            img3.BackgroundImage = new Bitmap("../../Resources/" + films[2].Image);

            lblName1.Text = films[0].Name.ToUpper().Replace("-", "-\n");
            lblName2.Text = films[1].Name.ToUpper().Replace("-", "-\n");
            lblName3.Text = films[2].Name.ToUpper().Replace("-", "-\n");

            lblTime1.Text = "Ngày chiếu: " + _roomFilmService.GetByFilmID(films[0].ID)[0].DateShow.ToShortDateString();
            lblTime2.Text = "Ngày chiếu: " + _roomFilmService.GetByFilmID(films[1].ID)[0].DateShow.ToShortDateString();
            lblTime3.Text = "Ngày chiếu: " + _roomFilmService.GetByFilmID(films[2].ID)[0].DateShow.ToShortDateString();

            img1.Tag = films[0];
            img2.Tag = films[1];
            img3.Tag = films[2];

            lblName1.Tag = films[0];
            lblName2.Tag = films[1];
            lblName3.Tag = films[2];
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

        private void img1_Click(object sender, EventArgs e)
        {
            var ptb = sender as PictureBox;
            var lbl = sender as Label;

            var film = ptb != null ? ptb.Tag as Film : lbl.Tag as Film;

            UIHelper.ShowControl(new fDetailFilmUC(film, PREVIOUS_FROM.HOME), panelContent);
        }

        #endregion UI
    }
}