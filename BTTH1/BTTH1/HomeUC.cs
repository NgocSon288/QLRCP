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
    public partial class HomeUC : UserControl
    {
        public HomeUC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show User Control, chỉ dùng để demo
            var fRegis = new RegisMemberUC();
            UIHelper.ShowControl(fRegis, panelContent);
        }
    }
}
