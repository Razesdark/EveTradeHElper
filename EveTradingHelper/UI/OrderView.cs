using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveTradingHelper
{
    public partial class OrderView : UserControl
    {
        Data.Order o;
        public OrderView(Data.Order o, DockStyle d = DockStyle.Fill)
        {
            
            InitializeComponent();
            this.Dock = d;
            this.o = o;

            UpdateView();
        }

        public Data.Order Order
        {
            get { return this.o; }
            set {
                this.o = value;
                UpdateView();
            }
        }
        private void UpdateView()
        {

            this.SuspendLayout();
            // General information assignments
            this.NameLabel.Text = o.Type;
            this.OwnerLabel.Text = o.Character;
            this.StationLabel.Text = o.Station;
            this.ItemStringLabel.Text = o.OrderSizeAt(o.LastSeen).ToString("N0") + " / " + o.InitialOrderSize.ToString("N0");
            this.ItemPercentLabel.Text = (100.0 * o.OrderPercentRemaining).ToString("N0") + "%";
            this.pictureBox1.Image = o.ItemImage;

            // Other Specific Top Panel
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "Daily Average", o.DailyAverage.ToString("N0") + " ISK");
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "Stack Price", (o.Price * o.InitialOrderSize).ToString("N0") + " ISK");
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "Lowest Price", "Not yet implemented");
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "Highest Price", "Not yet implemented");
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "Current Price", o.Price.ToString("N0") + " ISK");
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "Expires at", o.OrderExpiry.ToLongDateString());
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "Last seen", o.LastSeen.ToLongDateString());
            this.AddOrEditListItem(this.OtherSpecificTopPanel, "First seen", o.FirstSeen.ToLongDateString());
            this.ResumeLayout();

        }

        private void AddOrEditListItem(Panel p, string s1, string s2)
        {
            var items = p.Controls.OfType<UI.OrderViewListItem>().Where(x => x.Key.Equals(s1));
            if (items.Count() > 0)
            {
                items.First().Value = s2;
            }
            else
            {
                p.Controls.Add(new UI.OrderViewListItem(s1, s2, DockStyle.Top));
            }
        }

    }
}
