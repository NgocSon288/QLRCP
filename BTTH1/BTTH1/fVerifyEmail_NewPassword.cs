using BTTH1.Common;
using BTTH1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTH1
{
    public partial class fVerifyEmail_NewPassword : UserControl
    {
        private readonly IMemberService _memberService;

        private Control parentForm;
        private string newPassword;
        private string OTP;
        private Random rand = new Random();

        public fVerifyEmail_NewPassword(Control parent, string newPassword)
        {
            InitializeComponent();

            this._memberService = new MemberService();

            this.parentForm = parent;
            this.newPassword = newPassword;

            Load();
        }

        #region Events

        private async void button1_Click(object sender, EventArgs e)
        {
            if (txtOTP.Text == OTP)
            {
                await UpdatePassword();

                MessageBox.Show("Đổi mật khẩu thành công");

                UIHelper.ShowControl(new MemberUC(), Constants.Root);
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại mã OTP");

                txtOTP.Focus();
                txtOTP.SelectAll();
            }
        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button1.Enabled = false;

            await SendMail();

            button1.Enabled = true;

            MessageBox.Show("OTP đã được gửi, vui lòng kiểm tra lại email");
        }
         
        #endregion

        #region Methods

        private Task UpdatePassword()
        {
            Task task = new Task(new Action(() =>
            {
                Constants.CurrentMember.Password = newPassword;

                _memberService.Update(Constants.CurrentMember);
            }));

            task.Start();

            return task;
        }

        new private void Load()
        {
            txtOTP.MaxLength = 8;
            txtOTP.Focus();
            txtOTP.SelectAll();

            lblGreating.Text = "Chúng tôi đã gửi mã OTP xác nhận đổi mật khẩu quả địa chỉ email " + Constants.CurrentMember.Email + ", quý khách vui lòng kiểm tra lại email của mình";

            txtOTP.Focus();

            SendOTPToEmail();
        }

        private async void SendOTPToEmail()
        {
            // tạo OTP
            OTP = CreateOTP();

            // Gửi OTP qua email 
            await SendMail();
        }

        private async Task SendMail()
        {
            var content = File.ReadAllText("./../../Assets/Template/verifyemail_newpassword.html");

            content = content.Replace("{{MemberName}}", Constants.CurrentMember.Name);
            content = content.Replace("{{OTP}}", OTP);

            await MailHelper.SendMail(Constants.CurrentMember.Email, "Xác nhận mật khẩu từ CINEMA", content);
        }

        private string CreateOTP()
        {
            string otp = "";
            var i = 0;

            while (i++ < Constants.OTP_LENGTH)
            {
                switch (rand.Next(0, 3))
                {
                    case 0:
                        otp += (char)rand.Next(49, 58);
                        break;
                    case 1:
                        otp += (char)rand.Next(65, 91);
                        break;
                    default:
                        otp += (char)rand.Next(97, 123);
                        break;
                } 
            }

            return otp;
        }

        #endregion


        #region UI

        private void btnBack_Click(object sender, EventArgs e)
        {
            UIHelper.ShowCombackControl(parentForm);
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


        #endregion
    }
}
