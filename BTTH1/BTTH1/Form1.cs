using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class Form1 : Form
    {
        private readonly ICategoryMemberService _categoryMemberService;

        private IconButton currentBtn;
        private Panel leftBorderBtn;

        public Form1()
        {
            InitializeComponent(); 

            _categoryMemberService = new CategoryMemberService();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 100);
            panelMenu.Controls.Add(leftBorderBtn);
             
        }

        #region Events

        private void btn1_Click(object sender, System.EventArgs e)
        {
            ActivateButton(sender);

            // Show user control tương ứng 
            var fHome = new HomeUC();
            UIHelper.ShowControl(fHome, panelContent);

        }

        private void btn2_Click(object sender, System.EventArgs e)
        {
            ActivateButton(sender);

            var fFilm = new FilmUC();
            UIHelper.ShowControl(fFilm, panelContent);
        }

        private void btn3_Click(object sender, System.EventArgs e)
        {
            ActivateButton(sender);
             
            var fMember = new MemberUC();
            UIHelper.ShowControl(fMember, panelContent);
        }

        private void btn4_Click(object sender, System.EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btn5_Click(object sender, System.EventArgs e)
        {
            ActivateButton(sender);
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            Reset();

            // Show user control tương ứng 
            var fHome = new HomeUC();
            UIHelper.ShowControl(fHome, panelContent);
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

        #endregion


        #region Method

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
        }

        #endregion

    }
}