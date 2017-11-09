using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveTradingHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.mainPanel.Visible = false;
            this.dateTimePicker1.Value = DateTime.Now.AddDays(-1);
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Data.Order.Import();
            this.Visible = false;
            this.Visible = true;
        }

        /// <summary>
        /// Finds the appropriate Orders and put them in a listvievw
        /// </summary>
        /// <param name="sender">A listview</param>
        /// <param name="e">Arguments</param>
        private void reloadList(object sender, EventArgs e)
        {
            ListView s = (ListView)sender;

            ListViewItem[] items;
            if(s.Visible)
            {
                switch (s.Name)
                {
                    case "allOrdersList":
                        items = Data.Order.Orders
                            .Where(order => order.Value.IsSellOrder)
                            .Select(order => new[] { order.Key.ToString(), order.Value.Type, order.Value.IsActive ? "Y" : "N" })
                            .Select(item => new ListViewItem(item))
                            .ToArray<ListViewItem>();
                        break;
                    case "activeOrdersList":
                        items = Data.Order.Orders
                            .Where(order => order.Value.IsActive)
                            .Where(order => order.Value.IsSellOrder)
                            .Select(order => new[] { order.Key.ToString(), order.Value.Type, order.Value.IsActive ? "Y" : "N" })
                            .Select(item => new ListViewItem(item))
                            .ToArray<ListViewItem>();
                        break;
                    case "InactiveOrdersList":
                        items = Data.Order.Orders
                            .Where(order => order.Value.IsSellOrder)
                            .Where(order => !order.Value.IsActive)
                            .Select(order => new[] { order.Key.ToString(), order.Value.Type, order.Value.IsActive ? "Y" : "N" })
                            .Select(item => new ListViewItem(item))
                            .ToArray<ListViewItem>();
                        break;
                    case "RecentlyExpiredOrders":
                        items = Data.Order.Orders
                            .Where(order => order.Value.IsSellOrder)
                            .Where(order => !order.Value.IsActive)
                            .Where(order => order.Value.WasActive(this.dateTimePicker1.Value))
                            .Select(order => new[] { order.Key.ToString(), order.Value.Type, order.Value.IsActive ? "Y" : "N" })
                            .Select(item => new ListViewItem(item))
                            .ToArray<ListViewItem>();
                        break;
                    default:
                        items = new ListViewItem[0];
                        break;
                }
                s.Items.Clear();
                s.Items.AddRange(items);
                this.orderCounter.Text = "Orders: " + items.Length;
            }
        }

        /// <summary>
        /// Shows information about an order on screen
        /// </summary>
        /// <param name="o">The order to expand on</param>
        public void ShowItem(Data.Order o)
        {
            this.label1.Text = o.Type;
            this.label2.Text = o.Station + " @ " + o.Region;
            this.label3.Text = "Active: " + o.FirstSeen + " -> " + (o.IsActive ? "Current" : o.LastSeen.ToString());
           
            this.label4.Text = "Price: " + o.Price.ToString("#,##0.00");
            this.label5.Text = "Items: " + o.InitialOrderSize + " / " + o.OrderSizeAt(DateTime.Now);


            this.listView1.Items.Clear();
            this.listView1.Items.AddRange(o.EntityViewItems());

            this.mainPanel.Visible = true;
        }
        private void ListItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            long index = long.Parse(e.Item.SubItems[0].Text);
            this.ShowItem(Data.Order.Orders[index]);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.RecentlyExpiredOrders.Visible = false;
            this.RecentlyExpiredOrders.Visible = true;

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            ToolStripTextBox textbox = (ToolStripTextBox)sender;

            ListView listView = new[] { allOrdersList, activeOrdersList, InactiveOrdersList, RecentlyExpiredOrders }
                    .Where(x => x.Visible).First();

            bool hasText = textbox.Text.Length > 0;
            foreach(ListViewItem l in listView.Items)
            {

                l.BackColor = hasText && l.SubItems[1].Text.Contains(textbox.Text) ? Color.Gray : Color.White;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            (new ImprovedSearchForm()).ShowDialog();
        }
    }
}
