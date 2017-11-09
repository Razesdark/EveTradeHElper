using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveTradingHelper.Data;

namespace EveTradingHelper.SearchConditionTypes
{
    public class PricePercentageSearch : BasePanel
    {
        ComboBox cb;
        TextBox tb;

        public PricePercentageSearch() : base()
        {
            cb = BasePanel.StandardComboBox(new[] { "==", "!=", ">=", "<=", ">", "<" });
            
            cb.SelectedIndex = 2;
            tb = new TextBox();

            cb.Dock = DockStyle.Left;
            tb.Dock = DockStyle.Fill;

            tb.Margin = new Padding(0, 0, 10, 0);
            cb.Margin = new Padding(0, 0, 10, 0);


            tb.TextChanged += TriggerAction;
            cb.TextChanged += TriggerAction;
            cb.SelectedIndexChanged += TriggerAction;

            this.Controls.Add(tb);
            this.Controls.Add(cb);


            cb.Width = 70;


            this.PerformLayout();
            this.Update();
        }

        public override bool DataIsValid()
        {
            if (this.cb.Text.Length == 0 || this.tb.Text.Length == 0)
                return false;
            try
            {
                return double.Parse(this.tb.Text) <= 1.0;
            }
            catch { return false; }
        }

        public override Func<KeyValuePair<long, Order>, bool> GetPredicate()
        {
            switch (this.cb.Text)
            {
                case "==":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.OrderPercentRemaining == double.Parse(this.tb.Text) && key.Value.IsActive);
                case "!=":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.OrderPercentRemaining != double.Parse(this.tb.Text) && key.Value.IsActive);
                case ">=":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.OrderPercentRemaining >= double.Parse(this.tb.Text) && key.Value.IsActive);
                case "<=":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.OrderPercentRemaining <= double.Parse(this.tb.Text) && key.Value.IsActive);
                case ">":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.OrderPercentRemaining > double.Parse(this.tb.Text) && key.Value.IsActive);
                case "<":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.OrderPercentRemaining < double.Parse(this.tb.Text) && key.Value.IsActive);
            }
            return null;
        }

        public override string ToString()
        {
            return this.cb.Text + " " + this.tb.Text;
        }
    }
}
