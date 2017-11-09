using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveTradingHelper
{
    public partial class ImprovedSearchForm : Form
    {
        /// <summary>
        /// Current order filter
        /// </summary>
        private KeyValuePair<long, Data.Order>[] orders;

        /// <summary>
        /// Used to show the right side of the screen
        /// </summary>
        bool panel2_hidden = true;

        /// <summary>
        /// 
        /// </summary>
        public ImprovedSearchForm()
        {
            this.orders = new KeyValuePair<long, Data.Order>[0];

            InitializeComponent();

            SearchCondition i = new SearchCondition();
            i.Dock = DockStyle.Top;
            i.GreenButtonClick += I_GreenButtonClick;
            i.RedButtonClick += I_RedButtonClick;
            i.SearchFieldUpdatedEvent += I_SearchFieldUpdatedEvent;
            this.searchOptions.Controls.Add(i);

            UpdateAccordingToSearchParameters();
            
            foreach(Control c in this.splitContainer1.Panel2.Controls.OfType<Control>().ToArray())
            {
                c.Visible = false;
            }

        }

        /// <summary>
        /// Updates the right side of the window with information about the current order.
        /// </summary>
        /// <param name="order">The order to display</param>
        public void ShowOrder(Data.Order order)
        {
            if (panel2_hidden)
            {
                foreach (Control c in this.splitContainer1.Panel2.Controls.OfType<Control>().ToArray())
                {
                    c.Visible = true;
                }
                panel2_hidden = false;
            }
            this.ItemNameLabel.Text = order.Type;
            this.StationNameLabel.Text = order.Station + " @ " + order.Region;
            this.ItemPriceLabel.Text = "Price: ISK " + order.Price.ToString("###,00");
            this.ItemQuantityLabel.Text = (order.IsActive ? order.OrderSizeAt(order.LastSeen).ToString("##0,00") : "0") + " / " + order.InitialOrderSize.ToString("##0,00") + "(" + (100.0 * order.OrderPercentRemaining) + "%)";
            this.ItemSellerLabel.Text = order.Character;
            this.orderListView.Items.Clear();
            this.orderListView.Items.AddRange(order.EntityViewItems());

            this.TotalItemsInOrders.Text = "Items left in all orders: " + Data.Order.Orders
                .Where(o => o.Value.TypeID == order.TypeID)
                .Where(o => o.Value.IsActive)
                .Select(o => o.Value.OrderSizeAt(o.Value.LastSeen))
                .Sum().ToString("##0,00");
        }

        /// <summary>
        /// Triggered when the red button (or right click on green button) is clicked.
        /// 
        /// Deletes the current SearchOption.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Not used</param>
        private void I_RedButtonClick(SearchCondition sender, EventArgs e)
        {
            if (this.searchOptions.Controls.OfType<SearchCondition>().ToArray().Length > 1)
            {
                this.searchOptions.Controls.Remove((Control)sender);
                this.searchOptions.Controls.OfType<SearchCondition>().First().CurrentMode = SearchConditionStatusCode.Red;
            }

            ((SearchCondition)this.searchOptions.Controls[0]).CurrentMode = SearchConditionStatusCode.Green;
            UpdateAccordingToSearchParameters();
        }

        /// <summary>
        /// Triggered when the any SearchOption value has been updated
        /// </summary>
        /// <param name="sender">The SearchOption</param>
        /// <param name="e">Not used</param>
        private void I_SearchFieldUpdatedEvent(SearchCondition sender, EventArgs e)
        {
            UpdateAccordingToSearchParameters();
        }

        /// <summary>
        /// Triggered when the green button is clicked.
        /// 
        /// Adds a new text field.
        /// </summary>
        /// <param name="sender">A SearchCondition</param>
        /// <param name="e"></param>
        private void I_GreenButtonClick(SearchCondition sender, EventArgs e)
        {
            ((SearchCondition)sender).ToggleRedGreen();
            SearchCondition i = new SearchCondition();
            i.Dock = DockStyle.Top;
            i.GreenButtonClick += I_GreenButtonClick;
            i.RedButtonClick += I_RedButtonClick;
            i.SearchFieldUpdatedEvent += I_SearchFieldUpdatedEvent;

            List<SearchCondition> newConditions = new List<SearchCondition>();
            newConditions.Add(i);
            SearchCondition[] exitingConditions = this.searchOptions.Controls.OfType<SearchCondition>().ToArray();
            newConditions.AddRange(exitingConditions);

            this.searchOptions.Controls.Clear();
            this.searchOptions.Controls.AddRange(newConditions.ToArray());

        }

        /// <summary>
        /// Updates the searchresults according to the search conditions
        /// </summary>
        private void UpdateAccordingToSearchParameters()
        {
            var delegates = this.searchOptions.Controls.OfType<SearchCondition>()
                .Where( control => control.IsValidQuery)
                .Select(control => control.GetDelegate()).ToArray();

            
            orders = Data.Order.Orders.ToArray();
            if (delegates.Length != 0)
            {
                foreach (Func<KeyValuePair<long, Data.Order>, bool> d in delegates)
                {
                    orders = orders.Where(d).ToArray();
                }
            }

            this.listView1.Items.Clear();
            this.listView1.Items.AddRange(
                    orders.Select(order => new ListViewItem(new[] { order.Key.ToString(), order.Value.Type, (order.Value.IsActive ? "Y" : "N"), order.Value.Station })).ToArray()
                );

            this.QueryResultLabel.Text = "Results found: " + orders.Length;
        }



        /// <summary>
        /// Updates the column sizes.
        /// </summary>
        /// <param name="sender">Listview</param>
        /// <param name="e">Not used</param>
        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            var x = this.listView1.Columns.OfType<ColumnHeader>()
                .Where(ch => !ch.Text.Equals("Name"))
                .Select(ch => ch.Width)
                .Sum();
            this.listView1.Columns[1].Width = this.listView1.Width - x - 25;

        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            this.panel2.Width = this.panel1.Width / 2;
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var x = e.Item.Text;
            this.ShowOrder(Data.Order.Orders[long.Parse(x)]);
        }

        private void reimportFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.Order.Import();
            UpdateAccordingToSearchParameters();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Opens a new window based on the search results.
        /// </summary>
        /// <param name="sender">Not Used</param>
        /// <param name="e">Not Used</param>
        private void CreateMiniatureOutputWindow(object sender, EventArgs e)
        {
            (new MiniatureOutput(this.searchOptions.Controls.OfType<SearchCondition>()
                .Where(sc => sc.IsValidQuery)
                .ToArray())).Show();
        }
    }
}
