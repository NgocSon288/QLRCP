using BTTH1.Common;
using BTTH1.Models;
using BTTH1.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fUpdatePassword : UserControl
    {
        private readonly IMemberService _memberService;

        private Control parentForm;
        private Member member = Constants.CurrentMember;
        private string newPassword = "";

        public fUpdatePassword(Control parentForm)
        {
            InitializeComponent();

            this._memberService = new MemberService();

            this.parentForm = parentForm;

            Load();
        }

        #region Events

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var result = IsValidValue();

            if (result)
            {
                newPassword = txtNewPassword.Text;

                UIHelper.ShowControl(new fVerifyEmail_NewPassword(panelContent, newPassword), panelContent);
            }
        }

        #endregion Events

        #region Methods

        private bool IsValidValue()
        {
            if (!string.Equals(member.Password, txtOldPassword.Text, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Sai mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOldPassword.Focus();
                txtOldPassword.SelectAll();

                return false;
            }

            if (txtNewPassword.Text.Length < 4)
            {
                MessageBox.Show("Mật khẩu mới ít nhất 4 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();

                return false;
            }

            if (!string.Equals(txtConfirmPassword.Text, txtNewPassword.Text, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Xác nhận mật khẩu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();

                return false;
            }

            return true;
        }

        new private void Load()
        {
            txtUsername.Focus();
            txtUsername.SelectAll();

            LoadInfo();
        }

        private void LoadInfo()
        {
            txtUsername.Text = member.Username;
            txtEmail.Text = member.Email;
        }

        private bool CheckValidate(TextBox txt, Panel pnl, bool isNumber = false)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnl.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;

                return false;
            }
            else if (isNumber && !decimal.TryParse(txt.Text, out decimal res))
            {
                txt.ForeColor = Constants.LEAVE_TEXTBOX_INVALID_VALUE_MEMBER_DETAIL;
                pnl.BackColor = Constants.LEAVE_PANELINVALID_VALUE_MEMBER_DETAIL;

                return false;
            }
            else
            {
                txt.ForeColor = Constants.LEAVE_TEXTBOX_MEMBER_DETAIL;
                pnl.BackColor = Constants.LEAVE_PANEL_MEMBER_DETAIL;

                return true;
            }
        }

        #endregion Methods

        #region UI

        private void button1_Click(object sender, EventArgs e)
        {
            UIHelper.ShowCombackControl(parentForm);

            //UIHelper.ShowControl(new MemberUC(), Constants.Root);
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(34, 34, 34);
            btnBack.IconColor = Color.FromArgb(255, 34, 101);
            btnBack.ForeColor = Color.FromArgb(255, 34, 101);
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(40, 40, 40);
            btnBack.IconColor = Color.FromArgb(68, 226, 255);
            btnBack.ForeColor = Color.FromArgb(68, 226, 255);
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlUsername.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtUsername, pnlUsername);
        }

        private void txtOldPassword_Enter(object sender, EventArgs e)
        {
            txtOldPassword.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlOldPassword.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtOldPassword_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtOldPassword, pnlOldPassword);
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            txtNewPassword.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlNewPassword.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtNewPassword, pnlNewPassword);
        }

        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            txtConfirmPassword.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlConfirmNewPassword.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtConfirmPassword, pnlConfirmNewPassword);
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            txtEmail.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlEmail.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            CheckValidate(txtEmail, pnlEmail);
        }

        #endregion UI

    }
}