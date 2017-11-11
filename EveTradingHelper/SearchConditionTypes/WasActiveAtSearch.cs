using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveTradingHelper.Data;

namespace EveTradingHelper.SearchConditionTypes
{
    class WasActiveAtSearch : BasePanel
    {
        DateTimePicker dp;

        public WasActiveAtSearch()
        {
            dp = new DateTimePicker();

            this.dp.Dock = DockStyle.Fill;
            this.dp.Value = DateTime.Now.AddDays(-1);
            this.dp.ValueChanged += TriggerAction;
            this.Controls.Add(this.dp);
            this.Update();
        }

        public override bool DataIsValid()
        {
            return this.dp.Value <= DateTime.Now;
        }

        public override void FromString(string a)
        {
            throw new NotImplementedException();
        }

        public override Func<KeyValuePair<long, Order>, bool> GetPredicate()
        {
            return new Func<KeyValuePair<long, Order>, bool>(order => order.Value.WasActive(this.dp.Value));
        }

        public override string ToString()
        {
            return " at " + this.dp.Value.ToShortDateString();
        } 
        
    }
}
