using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveTradingHelper.Data;

namespace EveTradingHelper.SearchConditionTypes
{
    public class RegionSearch : BasePanel
    {
        ComboBox cb;


        public RegionSearch() : base()
        {
            cb = BasePanel.StandardComboBox(Data.EVE.Region.GetAllNames());

            cb.Dock = DockStyle.Fill;
            cb.Margin = new Padding(0, 0, 10, 0);

            cb.TextChanged += TriggerAction;
            cb.SelectedIndexChanged += TriggerAction;
            cb.SelectedIndex = 0;

            this.Controls.Add(cb);


            this.PerformLayout();
            this.Update();
        }

        public override bool DataIsValid()
        {
            return this.cb.Text.Length > 0;
        }

        public override void FromString(string a)
        {
            throw new NotImplementedException();
        }

        public override Func<KeyValuePair<long, Order>, bool> GetPredicate()
        {
            return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.Region.Equals(this.cb.Text));
        }

        public override string ToString()
        {
            return "== " + this.cb.Text;
        }
    }
}
