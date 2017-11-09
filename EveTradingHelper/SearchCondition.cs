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

    public enum SearchConditionStatusCode
    {
        Red, Green
    };
    /// <summary>
    /// The Search Condition controller is a UserControl that lets the user 
    /// specify a search query, and the control will show appropriate fields
    /// where the user can tweak the search query.
    /// </summary>
    public partial class SearchCondition : UserControl
    {
        /// <summary>
        /// This event gets called when the Green + Button is pressed
        /// </summary>
        public event ClickGreen GreenButtonClick;
        public delegate void ClickGreen(SearchCondition sender, EventArgs e);

        /// <summary>
        /// This event gets called when the red button is pressed
        /// </summary>
        public event ClickRed RedButtonClick;
        public delegate void ClickRed(SearchCondition sender, EventArgs e);

        /// <summary>
        /// This event is called whenever the search field is updated.
        /// </summary>
        public event SearchFieldUpdate SearchFieldUpdatedEvent;
        public delegate void SearchFieldUpdate(SearchCondition sender, EventArgs e);

        /// <summary>
        /// When True, the control will show a green button
        /// When False, the controll will show a red button
        /// </summary>
        private bool greenMode = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchCondition()
        {
            InitializeComponent();
            HideOptions();

            Data.Order.Import();

            this.RegionOption.Items.AddRange(Data.EVE.Region.GetAllNames());
            this.StationValue.Items.AddRange(Data.EVE.Station.GetAllNames());
            this.CharacterValue.Items.AddRange(Data.EVE.Character.GetAllNames());
            this.NameValue.Items.AddRange(Data.Order.Orders.Select(order => order.Value.Type).Distinct().ToArray());

            this.PercentageOrderCombo.SelectedItem = this.PercentageOrderCombo.Items[3];
            this.PercentageOrderCombo.SelectedIndexChanged += TriggerFieldUpdateEvent;
        }

        /// <summary>
        /// Generic Event that can be used in most event handlers
        /// that will trigger field update event
        /// </summary>
        /// <param name="sender">Any object</param>
        /// <param name="e">Event stuff, not used</param>
        private void TriggerFieldUpdateEvent(object sender, EventArgs e)
        {
            TriggerFieldUpdate();
        }

        /// <summary>
        /// Hides all the options
        /// </summary>
        private void HideOptions()
        {
            foreach(Panel p in (new[] {
                this.IsActiveOptions,
                this.RegionOptionPanel,
                this.StationPanel,
                this.CharacterPanel,
                this.IsBuyPanel,
                this.NamePanel,
                this.WasActiveAtPanel,
                this.PercentageOrderPanel,
                this.MultipleOrdersInStationPanel,
                this.DaysLeftOnOrderPanel
            }))
            {
                p.Visible = false;
                p.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// Selects which filter to use
        /// </summary>
        /// <param name="sender">A combobox containing a list of supported filters</param>
        /// <param name="e">Not used</param>
        private void SearchQuery_SelectFilter(object sender, EventArgs e)
        {

            HideOptions();

            switch (((ComboBox)sender).SelectedItem.ToString())
            {
                case "IsActive":
                    this.IsActiveOptions.Visible = true;
                    break;
                case "WasActiveAt":
                    this.WasActiveAtPanel.Visible = true;
                    this.WasActiveValueDateTime.Value = DateTime.Now;
                    break;
                case "Region":
                    this.RegionOptionPanel.Visible = true;
                    break;
                case "Station":
                    this.StationPanel.Visible = true;
                    break;
                case "Character":
                    this.CharacterPanel.Visible = true;
                    break;
                case "IsBuyOrder?":
                    this.IsBuyPanel.Visible = true;
                    break;
                case "Name":
                    this.NamePanel.Visible = true;
                    break;
                case "PercentageOrder":
                    this.PercentageOrderPanel.Visible = true;
                    break;
                case "MultipleOrders":
                    this.MultipleOrdersInStationPanel.Visible = true;
                    break;
                case "DaysLeft":
                    this.DaysLeftOnOrderPanel.Visible = true;
                    break;
            }

            if(this.IsValidQuery)
            {
                TriggerFieldUpdate();
            }
        }

        /// <summary>
        /// Returns true if the current values in are valid as a query
        /// </summary>
        public bool IsValidQuery
        {
            get
            {
                if (this.QueryType.SelectedItem == null)
                    return false;

                switch(this.QueryType.SelectedItem.ToString())
                {
                    case "MultipleOrders":
                    case "WasActiveAt":
                    case "IsBuyOrder?":
                    case "IsActive":
                        return true;
                    case "Region":
                        return Data.EVE.Region.GetAllNames().Contains(this.RegionOption.Text);
                    case "Station":
                        return this.StationValue.Text.Length > 3;
                    case "Character":
                        return this.CharacterValue.Text.Length > 3;
                    case "Name":
                        return this.NameValue.Text.Length > 3;
                    case "PercentageOrder":
                        if(this.PercentageOrderCombo.SelectedItem == null)
                            return false;
                        try {
                            double.Parse(this.PercentageOrderText.Text);
                            return true;
                        } catch { return false; }
                    case "DaysLeft":
                        if (this.DaysLeftOnOrderCombo.Text.Length == 0)
                            return false;
                        try { int.Parse(this.DaysLeftOnOrderValue.Text); return true; } catch { return false; }
                        
                }
                return false;
            }
        }

        /// <summary>
        /// Returns a predicate that matches the current query
        /// </summary>
        /// <returns>A predicate</returns>
        public Func<KeyValuePair<long, Data.Order>, bool> GetDelegate()
        {
            switch(this.QueryType.SelectedItem.ToString())
            {
                case "WasActiveAt":
                    if(this.WasActiveValueButton.Text.Equals("was active"))
                        return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.FirstSeen < this.WasActiveValueDateTime.Value && k.Value.LastSeen > this.WasActiveValueDateTime.Value);
                    else
                        return new Func<KeyValuePair<long, Data.Order>, bool>(k => !k.Value.WasActive(this.WasActiveValueDateTime.Value));
                case "IsActive":
                    return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.IsActive == this.IsActiveValue.Checked);
                case "IsBuyOrder?":
                    return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.IsBuyOrder == this.IsBuyValue.Checked);
                case "Region":
                    return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.Region.Equals(this.RegionOption.Text));
                case "Station":
                    return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.Station.Contains(this.StationValue.Text));
                case "Character":
                    return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.Character.Contains(this.CharacterValue.Text));
                case "Name":
                    return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.Type.Contains(this.NameValue.Text));
                case "PercentageOrder":
                    double value = double.Parse(this.PercentageOrderText.Text);
                    switch (this.PercentageOrderCombo.Text)
                    {
                        case "==":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.OrderPercentRemaining == value && k.Value.IsActive);
                        case "!=":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.OrderPercentRemaining != value && k.Value.IsActive);
                        case ">=":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.OrderPercentRemaining >= value && k.Value.IsActive);
                        case "<=":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.OrderPercentRemaining <= value && k.Value.IsActive);
                        case ">":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.OrderPercentRemaining > value && k.Value.IsActive);
                        case "<":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.OrderPercentRemaining < value && k.Value.IsActive);

                    }
                    return null;
                case "MultipleOrders":
                    return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.HasMultipleOrdersInStation == this.MultipleOrdersValue.Checked);
                case "DaysLeft":
                    switch(this.DaysLeftOnOrderCombo.Text)
                    {
                        case ">=":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.TimeLeftOnOrder.TotalDays >= int.Parse(this.DaysLeftOnOrderValue.Text));
                        case "<=":
                            return new Func<KeyValuePair<long, Data.Order>, bool>(k => k.Value.TimeLeftOnOrder.TotalDays <= int.Parse(this.DaysLeftOnOrderValue.Text));
                    }
                    return null;

            }
            return null;
        }

        public override string ToString()
        {
            switch(this.QueryType.SelectedItem.ToString())
            {
                case "WasActiveAt":
                        return "WasActiveAt " + this.WasActiveValueButton.Text + this.WasActiveValueDateTime.Value.ToLongDateString();
                case "IsActive":
                    return this.IsActiveValue.Checked ? "IsActive" : "NotActive";
                case "IsBuyOrder?":
                    return this.IsBuyValue.Checked ? "IsActive" : "NotActive";
                case "Region":
                    return "Region == " + this.RegionOption.Text;
                case "Station":
                    return "Station contains " + this.StationValue.Text;
                case "Character":
                    return "Character contains " + this.CharacterValue.Text;
                case "Name":
                    return "Name contains " + this.NameValue.Text;
                case "PercentageOrder":
                    double value = double.Parse(this.PercentageOrderText.Text);
                    return "PercentageOrder " + this.PercentageOrderCombo.SelectedItem.ToString() + " " + value;
                case "MultipleOrders":
                    return this.MultipleOrdersValue.Checked ? "Has Multiple Orders" : "Not Multiple Orders";
                case "DaysLeft":
                    return "DaysLeft" + this.PercentageOrderCombo.Text + " " + this.PercentageOrderText;
                default:
                    return "";
            }
        }

        public SearchConditionStatusCode CurrentMode
        {
            get { return this.greenMode ? SearchConditionStatusCode.Green : SearchConditionStatusCode.Red; }
            set { this.greenMode = value == SearchConditionStatusCode.Green ? true : false; }
        }

        /// <summary>
        /// Toggles the red/green mode
        /// </summary>
        public void ToggleRedGreen()
        {
            this.greenMode = !this.greenMode;

            this.pictureBox1.Image = this.greenMode ? Properties.Resources.plus : Properties.Resources.minus;
        }

        /// <summary>
        /// This function triggers the SearchFieldUpdate event
        /// </summary>
        private void TriggerFieldUpdate()
        {
            SearchFieldUpdatedEvent?.Invoke(this, new EventArgs());
        }


        /// <summary>
        /// Clicks either red or green button
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">EventArgs</param>
        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (this.greenMode)
                if (e.Button == MouseButtons.Left)
                    GreenButtonClick?.Invoke(this, new EventArgs());
                else
                    RedButtonClick?.Invoke(this, new EventArgs());
            else
                RedButtonClick?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Swaps state for a button acting like a boolean switch
        /// </summary>
        /// <param name="sender">A button</param>
        /// <param name="e">Not used</param>
        private void ButtonSwapActiveState(object sender, EventArgs e)
        {
            ((Button)sender).Text = ((Button)sender).Text.Equals("was active") ? "not active" : "was active";
            TriggerFieldUpdate();
        }

    }
}
