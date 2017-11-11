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
        public Dictionary<string, SearchConditionTypes.BasePanel> types;

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
        /// Constructor with Query
        /// </summary>
        /// <param name="query">Query</param>
        public SearchCondition(string query) : this()
        {
            this.FromString(query);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public SearchCondition()
        {
            this.types = new Dictionary<string, SearchConditionTypes.BasePanel>();
            this.types.Add("Name", new SearchConditionTypes.NameSearch());
            this.types.Add("Region", new SearchConditionTypes.RegionSearch());
            this.types.Add("Station", new SearchConditionTypes.StationSearch());
            this.types.Add("Character", new SearchConditionTypes.CharacterSearch());
            this.types.Add("IsActive", new SearchConditionTypes.IsActiveSearch());
            this.types.Add("MultipleOrders", new SearchConditionTypes.MultipleOrdersInStationSearch());
            this.types.Add("Price", new SearchConditionTypes.PriceSearch());
            this.types.Add("OrderPercent", new SearchConditionTypes.PricePercentageSearch());
            this.types.Add("WasActiveAt", new SearchConditionTypes.WasActiveAtSearch());
            InitializeComponent();

            foreach(SearchConditionTypes.BasePanel b in this.types.Values)
                b.SearchFieldUpdatedEvent += TriggerFieldUpdateEvent;
            
            this.QueryType.Items.AddRange(this.types.Keys.ToArray());

        }



        /// <summary>
        /// Generic Event that can be used in most event handlers
        /// that will trigger field update event
        /// </summary>
        /// <param name="sender">Any object</param>
        /// <param name="e">Event stuff, not used</param>
        private void TriggerFieldUpdateEvent(SearchConditionTypes.BasePanel sender, EventArgs e)
        {
            TriggerFieldUpdate();
        }

        /// <summary>
        /// Hides all the options
        /// </summary>
        private void HideOptions()
        {
          
        }

        /// <summary>
        /// Selects which filter to use
        /// </summary>
        /// <param name="sender">A combobox containing a list of supported filters</param>
        /// <param name="e">Not used</param>
        private void SearchQuery_SelectFilter(object sender, EventArgs e)
        {

            HideOptions();
            this.BasePanel.Controls.Clear();
            this.BasePanel.Controls.Add(this.types[this.QueryType.Text]);
            this.types[this.QueryType.Text].Dock = DockStyle.Fill;
            if (this.types[this.QueryType.Text].DataIsValid())
                TriggerFieldUpdate();
            this.BasePanel.Refresh();
        }

        /// <summary>
        /// Returns true if the current values in are valid as a query
        /// </summary>
        public bool IsValidQuery
        {
            get
            {
                if (this.QueryType.Text.Equals(""))
                    return false;
                return this.types[this.QueryType.Text].DataIsValid();
            }
        }

        /// <summary>
        /// Returns a predicate that matches the current query
        /// </summary>
        /// <returns>A predicate</returns>
        public Func<KeyValuePair<long, Data.Order>, bool> GetPrediacte()
        {

            return this.types[this.QueryType.Text].GetPredicate();
        }

        public override string ToString()
        {
            return this.QueryType.Text + " " + this.types[this.QueryType.Text].ToString();
        }

        public void FromString(string query)
        {
            var best_option = this.types
                .Where(x => query.StartsWith(x.Key))
                .OrderBy(x => x.Key.Length)
                .Reverse()
                .First();

            this.QueryType.SelectedItem = best_option.Key;
            best_option.Value.FromString(query.Replace(best_option.Key, "").Trim());
        }

        /// <summary>
        /// Gets and sets the SearchConditionStatusCode
        /// </summary>
        public SearchConditionStatusCode CurrentMode
        {
            get { return this.greenMode ? SearchConditionStatusCode.Green : SearchConditionStatusCode.Red; }
            set { this.greenMode = value == SearchConditionStatusCode.Green ? true : false; this.UpdatePictureBox(); }
        }

        /// <summary>
        /// Toggles the red/green mode
        /// </summary>
        public void ToggleRedGreen()
        {
            this.greenMode = !this.greenMode;
            this.UpdatePictureBox();
        }


        private void UpdatePictureBox()
        {
            this.pictureBox1.Image = this.greenMode ? Properties.Resources.plus : Properties.Resources.minus;
            this.pictureBox1.Invalidate();
            this.pictureBox1.Update();
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
