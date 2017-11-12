using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveTradingHelper.UI
{
    public partial class OrderViewListItem : UserControl
    {
        public string Key { get { return this.label1.Text; } }
        public string Value { get { return this.label2.Text; } set { this.label2.Text = value; } }

        public OrderViewListItem(string label1, string label2, DockStyle d = DockStyle.Fill)
        {
            InitializeComponent();
            this.Dock = d;
            this.label1.Text = label1;
            this.label2.Text = label2;
        }

        private void OrderViewListItem_SizeChanged(object sender, EventArgs e)
        {
            this.label1.MaximumSize = new Size(this.Width - this.panel1.Padding.Left - this.panel1.Padding.Right, this.label1.MaximumSize.Height);
            this.label1.MaximumSize = new Size(this.Width - this.panel2.Padding.Left - this.panel2.Padding.Right, this.label2.MaximumSize.Height);
        }
    }
}
