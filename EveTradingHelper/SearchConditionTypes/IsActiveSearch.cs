﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveTradingHelper.Data;

namespace EveTradingHelper.SearchConditionTypes
{
    public class IsActiveSearch : BasePanel
    {

        CheckBox cb;
        public IsActiveSearch() : base()
        {
            cb = new CheckBox();
            cb.Text = "Is Active?";
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

        public override void FromString(string a)
        {
            this.cb.Checked = a.Equals("is active");
        }

        public override Func<KeyValuePair<long, Order>, bool> GetPredicate()
        {
            return new Func<KeyValuePair<long, Order>, bool>(key => key.Value.IsActive == this.cb.Checked);
        }

        public override string ToString()
        {
            return this.cb.Checked ? "is active" : "is not active";
        }

    }
}
