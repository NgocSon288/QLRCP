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
            if(MessageBox.Show("Bạn có muốn hủy việc mua vé", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in seatSelected)
                {
                    seat[item] = !seat[item];
                }
                seatSelected.Clear();

                this.Close();
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            var parent = control.Parent as FlowLayoutPanel;
            if (parent.Tag == null)
            {
                return;
            }
            var button = parent.Controls[0] as Button;
            var label = parent.Controls[1] as Label;
            var key = parent.Tag.ToString();

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

            button.BackColor = !seat[key] ? Constants.BUTTON_SEAT_BG_SELECTED : Constants.BUTTON_SEAT_BG_EMPTY;
            label.ForeColor = !seat[key] ? Constants.BUTTON_SEAT_BG_SELECTED : Constants.LABEL_SEAT_FORE_EMPTY;
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
                var HEIGHT_LABEL = Constants.HEIGHT_SEAT - Constants.WIDTH_SEAT;

                var indexBonus = Convert.ToInt32(item.Key.Substring(1)) - 1;
                var bonus = item.Key.Contains("A") ? 0 : Constants.BONUS_MARGIN[indexBonus];
                FlowLayoutPanel pnl = new FlowLayoutPanel();
                pnl.Width = Constants.WIDTH_SEAT;
                pnl.Height = Constants.HEIGHT_SEAT;
                pnl.Tag = item.Value ? item.Key : null;

                Button btn = new Button();
                btn.Width = Constants.WIDTH_SEAT;
                btn.Height = Constants.HEIGHT_SEAT - HEIGHT_LABEL;
                btn.BackColor = item.Value ? Constants.BUTTON_SEAT_BG_EMPTY : Constants.BUTTON_SEAT_BG_BOUGHT;
                btn.Cursor = item.Value ? Cursors.Hand : Cursors.No;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.TextAlign = ContentAlignment.BottomCenter;

                Label lbl = new Label();
                lbl.Text = item.Key;
                lbl.Height = HEIGHT_LABEL;
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Bottom;
                lbl.Width = pnl.Width;
                lbl.ForeColor = item.Value ? Constants.LABEL_SEAT_FORE_EMPTY : Constants.LABEL_SEAT_FORE_BOUGHT;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font(lbl.Font.FontFamily, 10);
                lbl.Cursor = item.Value ? Cursors.Hand : Cursors.No;

                // Load position
                if (item.Key.Contains("A"))
                {
                    pnl.Margin = new Padding(25, 20 + bonus, 25, 80);
                }
                else if (item.Key.Contains("4"))
                {
                    pnl.Margin = new Padding(80, 40 + bonus, 20, 40);
                }
                else if (item.Key.Contains("7"))
                {
                    pnl.Margin = new Padding(20, 40 + bonus, 100, 40);
                }
                else
                {
                    pnl.Margin = new Padding(15, 40 + bonus, 15, 40);
                }

                // Load seat style
                if (item.Key.Contains("A"))
                {
                    btn.BackgroundImage = new Bitmap(Constants.SEAT_ICON_STYLE_1_PATH);
                }
                else if (item.Key.Contains("4") || item.Key.Contains("5") || item.Key.Contains("6") || item.Key.Contains("7"))
                {
                    btn.BackgroundImage = new Bitmap(Constants.SEAT_ICON_STYLE_2_PATH);
                }
                else
                {
                    btn.BackgroundImage = new Bitmap(Constants.SEAT_ICON_STYLE_3_PATH);
                }

                btn.Click += Btn_Click;
                lbl.Click += Btn_Click;

                pnl.Controls.Add(btn);
                pnl.Controls.Add(lbl);
                flpSeat.Controls.Add(pnl);
            }
        }

        #endregion
    }
}
