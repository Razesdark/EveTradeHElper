using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveTradingHelper.SearchConditionTypes
{
    public abstract class BasePanel : Panel
    {
        /// <summary>
        /// This event is called whenever the search field is updated.
        /// </summary>
        public event SearchFieldUpdate SearchFieldUpdatedEvent;
        public delegate void SearchFieldUpdate(BasePanel sender, EventArgs e);

        protected BasePanel()
        {
            this.Padding = new Padding(5);

        }


        public abstract Func<KeyValuePair<long, Data.Order>, bool> GetPredicate();
        public abstract bool DataIsValid();
        public abstract override string ToString();

        public static ComboBox StandardComboBox(string[] arguments)
        {
            ComboBox cb = new ComboBox();
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Items.AddRange(arguments);

            return cb;
        }

        protected void TriggerAction(object sender, EventArgs e)
        {
                SearchFieldUpdatedEvent?.Invoke(this, new EventArgs());
        }
    }
}
