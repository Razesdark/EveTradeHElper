using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveTradingHelper.Data;

namespace EveTradingHelper.SearchConditionTypes
{
    public class NameSearch : BasePanel
    {
        ComboBox cb;
        TextBox tb;

        public NameSearch() : base()
        {
            cb = BasePanel.StandardComboBox(new[] { "==", "!=", "includes", "not includes" });
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
            return this.cb.Text.Length > 0 && this.tb.Text.Length > 0;
        }

        public override void FromString(string a)
        {
            this.cb.SelectedItem = this.cb.Items.OfType<string>()
                .Where(x => a.StartsWith(x))
                .First();

            this.tb.Text = a.Replace(this.cb.SelectedItem.ToString(), "").Trim();
        }

        public override Func<KeyValuePair<long, Order>, bool> GetPredicate()
        {
            switch(this.cb.Text)
            {
                case "==":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.Type.Equals(this.tb.Text));
                case "!=":
                    return new Func<KeyValuePair<long, Order>, bool>(key => !key.Value.Type.Equals(this.tb.Text));
                case "includes":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.Type.Contains(this.tb.Text));
                case "not includes":
                    return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.Type.Contains(this.tb.Text));
            }
            return null;
        }

        public override string ToString()
        {
            return this.cb.Text + " " + this.tb.Text;
        }
    }
}
