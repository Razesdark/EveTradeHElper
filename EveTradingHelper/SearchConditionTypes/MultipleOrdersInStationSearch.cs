using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveTradingHelper.Data;

namespace EveTradingHelper.SearchConditionTypes
{
    public class MultipleOrdersInStationSearch : BasePanel
    {

        CheckBox cb;
        public MultipleOrdersInStationSearch() : base()
        {
            cb = new CheckBox();
            cb.Text = "Has orders in same station?";
            cb.Dock = DockStyle.Fill;

            cb.CheckedChanged += TriggerAction;

            this.Controls.Add(cb);
            this.PerformLayout();
            this.Update();
            cb.Checked = true;
        }

        public override bool DataIsValid()
        {
            return true;
        }

        public override Func<KeyValuePair<long, Order>, bool> GetPredicate()
        {
            return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.HasMultipleOrdersInStation == this.cb.Checked);
        }

        public override string ToString()
        {
            return this.cb.Checked ? "is active" : "is not active";
        }
    }
}
