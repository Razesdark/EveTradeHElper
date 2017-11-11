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
    public partial class MiniatureOutput : Form
    {
        public MiniatureOutput(SearchCondition[] search)
        {
            InitializeComponent();
            this.label1.Text = string.Join(" AND ", search.Select(s => s.ToString()).ToArray());
            this.Text = this.label1.Text;
            this.listView1.Columns.AddRange(Data.Order.GenerateMiniatureOutputHeaders());

            this.listView1.Sorting = SortOrder.Ascending;
            Func <KeyValuePair<long, Data.Order>, bool> [] predicates = search.Select(s => s.GetPrediacte()).ToArray();
            KeyValuePair<long, Data.Order>[] orders = Data.Order.Orders.ToArray();
            foreach (Func<KeyValuePair<long, Data.Order>, bool> d in predicates)
                orders = orders.Where(d).ToArray();

            this.listView1.Items.AddRange(orders
                .Select(order => order.Value.GenerateMiniatureOutput())
                .ToArray());

            foreach(ColumnHeader c in this.listView1.Columns)
                c.Width = -2;
        }
    }
}
