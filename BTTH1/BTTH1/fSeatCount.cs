using BTTH1.Common;
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
    public partial class fSeatCount : Form
    {
        public fSeatCount(fRoomManagement f)
        {
            InitializeComponent();

            this.f = f;
        }

        fRoomManagement f;

        private void button1_Click(object sender, EventArgs e)
        {
            this.f.seatMax = -1;
            this.Close();
        }

        private void txtSeatMax_Enter(object sender, EventArgs e)
        {
            txtSeatMax.ForeColor = Constants.ACTIVE_TEXTBOX_MEMBER_DETAIL;
            pnlSeatMax.BackColor = Constants.ACTIVE_PANEL_MEMBER_DETAIL;
        }

        private void txtSeatMax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSeatMax.Text) || !Int32.TryParse(txtSeatMax.Text, out int res))
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!Int32.TryParse(txtSeatMax.Text, out f.seatMax))
            {
                MessageBox.Show("Số ghế không hợp lệ!");
            }
            else if (f.seatMax < 20 || f.seatMax > 200)
            {
                MessageBox.Show("Số ghế phải từ 20 đến 200");
            }
            else
            {
                this.Close();
            }
        }
    }
}
