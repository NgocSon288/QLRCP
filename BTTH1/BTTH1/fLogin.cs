using BTTH1.Common;
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
    public partial class fLogin : Form
    {
        private readonly IMemberService _memberService;

        public fLogin()
        {
            InitializeComponent();

            _memberService = new MemberService();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Login();
            }
        }

        private void linkForgetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkRegist_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        #region Method

        private bool IsValidate()
        {
            return !(txtUsername.Text.Trim() == "" || txtUsername.Text.Trim() == "");
        }

        private void Login()
        {
            if (!IsValidate())
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không được để trống!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            var username = txtUsername.Text;
            var password = txtPassword.Text;
            var member = _memberService.GetByUsername(username);

            if (member == null)
            {
                MessageBox.Show("Tài khoản không hợp lệ!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.SelectAll();
            }
            else if (member.Password != password)
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
            else if (member.Status == false)
            {
                MessageBox.Show("Tài khoản của bạn đã bị khóa!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.SelectAll();
            }
            else
            {
                Constants.CurrentMember = member;

                this.Close();
            }
        }

        #endregion

        #region UI

        private void btnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            btnLogin.Image = new Bitmap(@"../../Resources/btn-background.png");
        }

        private void btnLogin_MouseUp(object sender, MouseEventArgs e)
        {
            btnLogin.Image = new Bitmap(@"../../Resources/btn-border.png");
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            lblPassword.ForeColor = Constants.ACTIVE_COLOR;
            pnlPassword.BackColor = Constants.ACTIVE_COLOR;
            txtPassword.ForeColor = Constants.ACTIVE_COLOR;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            lblPassword.ForeColor = Constants.LEAVE_COLOR;
            pnlPassword.BackColor = Constants.LEAVE_COLOR;
            txtPassword.ForeColor = Constants.LEAVE_COLOR;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            lblUsername.ForeColor = Constants.ACTIVE_COLOR;
            pnlUsername.BackColor = Constants.ACTIVE_COLOR;
            txtUsername.ForeColor = Constants.ACTIVE_COLOR;
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            lblUsername.ForeColor = Constants.LEAVE_COLOR;
            pnlUsername.BackColor = Constants.LEAVE_COLOR;
            txtUsername.ForeColor = Constants.LEAVE_COLOR;
        }

        private void fLogin_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        #endregion 
    }
}
