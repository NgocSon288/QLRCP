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
    public partial class fSeat : Form
    {
        private List<string> seatSelected;
        private Dictionary<string, bool> seat;
        private int seatCount;
        private int seatCountSelected;

        public fSeat(List<string> seatSelected, Dictionary<string, bool> seat, int seatCount)
        {
            InitializeComponent();

            this.seatSelected = seatSelected;
            this.seat = seat;
            this.seatCount = seatCount;
            this.seatCountSelected = 0;

            Load();
        }

        #region Events

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in seatSelected)
            {
                seat[item] = !seat[item];
            }
            seatSelected.Clear();

            this.Close();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var key = btn.Tag.ToString();

            if (seatCountSelected >= seatCount && seat[key])
            {
                MessageBox.Show("Số lượng ghế bạn chọn đã đủ", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            seat[key] = !seat[key];

            if (!seat[key])
            {
                seatCountSelected++;
                seatSelected.Add(key);
            }
            else
            {
                seatCountSelected--;
                seatSelected.Remove(key);
            }
            ChangeLabelSeatCount();
            LoadButtonOrder();

            btn.BackColor = !seat[key] ? Constants.BUTTON_SEAT_BG_SELECTED : Constants.BUTTON_SEAT_BG_EMPTY;
            btn.ForeColor = !seat[key] ? Constants.BUTTON_SEAT_FORE_SELECTED : Constants.BUTTON_SEAT_FORE_EMPTY;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private void LoadButtonOrder()
        {
            var check = seatCount == seatCountSelected;
            btnOrder.Enabled = check;
            btnOrder.Cursor = check ? Cursors.Hand : Cursors.No;
            btnOrder.BackColor = !check ? Color.FromArgb(246, 255, 160) : Color.FromArgb(249, 202, 36);
            btnOrder.ForeColor = !check ? Color.FromArgb(255, 230, 200) : Color.FromArgb(153, 112, 26);
        }

        new private void Load()
        {
            lblSeatCount.Text = string.Format("VUI LÒNG CHỌN: {0} GHẾ", seatCount);

            ChangeLabelSeatCount();
            LoadSeat();
            LoadButtonOrder();
        }

        private void ChangeLabelSeatCount()
        {
            lblSeatSelected.Text = string.Format("Đã chọn: {0}/{1} ghế", seatCountSelected, seatCount);
        }

        private void LoadSeat()
        {
            foreach (var item in seat)
            {
                Button btn = new Button();
                btn.Width = Constants.WIDTH_SEAT;
                btn.Height = Constants.HEIGHT_SEAT;
                btn.Text = item.Key;
                btn.BackColor = item.Value ? Constants.BUTTON_SEAT_BG_EMPTY : Constants.BUTTON_SEAT_BG_BOUGHT;
                btn.ForeColor = item.Value ? Constants.BUTTON_SEAT_FORE_EMPTY : Constants.BUTTON_SEAT_FORE_BOUGHT;
                btn.Enabled = item.Value;
                btn.Tag = item.Key;
                btn.Margin = new Padding(20, 20, 20, 30);
                btn.Cursor = item.Value ? Cursors.Hand : Cursors.No;


                btn.Click += Btn_Click;

                flpSeat.Controls.Add(btn);
            }
        }

        #endregion
    }
}
