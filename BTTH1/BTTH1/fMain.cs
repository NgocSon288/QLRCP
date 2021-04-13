using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fMain : Form
    {
        private readonly ICategoryMemberService _categoryMemberService;
        private readonly ICategoryFilmService _categoryFilmService;
        private readonly IFilmService _filmService;

        private IconButton currentBtn;
        private Panel leftBorderBtn;

        private bool isHeighPermission = false;

        public fMain()
        {
            InitializeComponent();

            _categoryMemberService = new CategoryMemberService();
            _categoryFilmService = new CategoryFilmService();
            _filmService = new FilmService();

            Constants.MainForm = this;
            Constants.Root = panelContent;

            Load();
        }

        #region Events

        private void btn1_Click(object sender, System.EventArgs e)
        {
            isHeighPermission = false;

            ActivateButton(sender);

            // Show user control tương ứng
            var fHome = new fHomeUC();
            UIHelper.ShowControl(fHome, panelContent);
        }

        private void btn2_Click(object sender, System.EventArgs e)
        {
            isHeighPermission = false;

            ActivateButton(sender);

            var fFilm = new fFilmUC();
            UIHelper.ShowControl(fFilm, panelContent);
        }

        private void btn3_Click(object sender, System.EventArgs e)
        {
            isHeighPermission = true;

            ActivateButton(sender);

            var fMember = new fMemberUC();
            UIHelper.ShowControl(fMember, panelContent);
        }

        private void btn4_Click(object sender, System.EventArgs e)
        {
            isHeighPermission = true;

            ActivateButton(sender);

            var filmManagement = new fFilmManagement();
            UIHelper.ShowControl(filmManagement, panelContent);
        }

        private void btn5_Click(object sender, System.EventArgs e)
        {
            isHeighPermission = true;

            ActivateButton(sender);

            var fRoomManagement = new fRoomManagement();
            UIHelper.ShowControl(fRoomManagement, panelContent);
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (Constants.CurrentMember == null)
            {
                Login();
            }
            else
            {
                Logout();

                if (isHeighPermission)
                {
                    Reset();
                }
            }

            isHeighPermission = false;
            LoadLeftMenu();
        }

        #endregion Events

        #region Method

        new private void Load()
        {
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 100);
            panelMenu.Controls.Add(leftBorderBtn);

            VisibleButton(Constants.PERMISSION_NULL);

            Reset();

            
        }

        private void ActivateButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableButton();

                //Button transition
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = Constants.BORDER_MENU_LEFT_COLOR;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Constants.BORDER_MENU_LEFT_COLOR;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button
                leftBorderBtn.BackColor = Constants.BORDER_MENU_LEFT_COLOR;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;

            // Show user control tương ứng
            var fHome = new fHomeUC();
            UIHelper.ShowControl(fHome, panelContent);
        }

        private void Login()
        {
            Form f = new fLogin();

            f.ShowDialog();

            if (Constants.CurrentMember != null)
            {
                btnLogin.Text = "Đăng xuất";

                LoadHeader();
            }
        }

        private void Logout()
        {
            Constants.CurrentMember = null;
            btnLogin.Text = "Đăng nhập";

            LoadHeader();
        }

        private void LoadLeftMenu()
        {
            if (Constants.CurrentMember == null)
            {
                VisibleButton(Constants.PERMISSION_NULL);

                return;
            }

            var category = _categoryMemberService.GetByID(Constants.CurrentMember.CategoryMemberID);
            var permission = category.Name;

            switch (permission)
            {
                case "Admin":
                    VisibleButton(Constants.PERMISSION_ADMIN);
                    break;

                case "Staff":
                    VisibleButton(Constants.PERMISSION_STAFF);
                    break;

                case "Customer":
                    VisibleButton(Constants.PERMISSION_CUSTOMER);
                    break;
            }
        }

        private void VisibleButton(int[] btns)
        {
            btnHome.Visible = btns[0] == 1;
            btnFilm.Visible = btns[1] == 1;
            btnProfile.Visible = btns[2] == 1;
            btnStaffMana.Visible = btns[3] == 1;
            btnAdminMana.Visible = btns[4] == 1;
        }

        #endregion Method




        public void LoadHeader()
        {
            if (Constants.CurrentMember == null)
            {
                lblGreeting.Text = string.Format($"Xin chào quý khách");
                btnAvatar.BackgroundImage = null;
            }
            else
            {
                lblGreeting.Text = string.Format($"Xin chào {Constants.CurrentMember.Name}");
                Image StartImage = Image.FromFile("../../Assets/Images/" + Constants.CurrentMember.Avatar); 
                Image RoundedImage = UIHelper.ClipToCircle(StartImage, Color.FromArgb(31, 30, 68));
                btnAvatar.BackgroundImage = RoundedImage;
            }

            
        }

    }
}