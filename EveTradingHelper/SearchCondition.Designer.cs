namespace EveTradingHelper
{
    partial class SearchCondition
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchOptionPanel = new System.Windows.Forms.Panel();
            this.QueryType = new System.Windows.Forms.ComboBox();
            this.IsActiveOptions = new System.Windows.Forms.Panel();
            this.IsActiveValue = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RegionOptionPanel = new System.Windows.Forms.Panel();
            this.RegionOption = new System.Windows.Forms.ComboBox();
            this.StationPanel = new System.Windows.Forms.Panel();
            this.StationValue = new System.Windows.Forms.ComboBox();
            this.CharacterPanel = new System.Windows.Forms.Panel();
            this.CharacterValue = new System.Windows.Forms.ComboBox();
            this.IsBuyPanel = new System.Windows.Forms.Panel();
            this.IsBuyValue = new System.Windows.Forms.CheckBox();
            this.NamePanel = new System.Windows.Forms.Panel();
            this.NameValue = new System.Windows.Forms.ComboBox();
            this.WasActiveAtPanel = new System.Windows.Forms.Panel();
            this.WasActiveValueDateTime = new System.Windows.Forms.DateTimePicker();
            this.WasActiveValueButton = new System.Windows.Forms.Button();
            this.PercentageOrderPanel = new System.Windows.Forms.Panel();
            this.PercentageOrderText = new System.Windows.Forms.DomainUpDown();
            this.PercentageOrderCombo = new System.Windows.Forms.ComboBox();
            this.MultipleOrdersInStationPanel = new System.Windows.Forms.Panel();
            this.MultipleOrdersValue = new System.Windows.Forms.CheckBox();
            this.DaysLeftOnOrderPanel = new System.Windows.Forms.Panel();
            this.DaysLeftOnOrderCombo = new System.Windows.Forms.ComboBox();
            this.DaysLeftOnOrderValue = new System.Windows.Forms.TextBox();
            this.searchOptionPanel.SuspendLayout();
            this.IsActiveOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.RegionOptionPanel.SuspendLayout();
            this.StationPanel.SuspendLayout();
            this.CharacterPanel.SuspendLayout();
            this.IsBuyPanel.SuspendLayout();
            this.NamePanel.SuspendLayout();
            this.WasActiveAtPanel.SuspendLayout();
            this.PercentageOrderPanel.SuspendLayout();
            this.MultipleOrdersInStationPanel.SuspendLayout();
            this.DaysLeftOnOrderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchOptionPanel
            // 
            this.searchOptionPanel.Controls.Add(this.QueryType);
            this.searchOptionPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.searchOptionPanel.Location = new System.Drawing.Point(0, 0);
            this.searchOptionPanel.Name = "searchOptionPanel";
            this.searchOptionPanel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.searchOptionPanel.Size = new System.Drawing.Size(163, 33);
            this.searchOptionPanel.TabIndex = 0;
            // 
            // QueryType
            // 
            this.QueryType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QueryType.FormattingEnabled = true;
            this.QueryType.Items.AddRange(new object[] {
            "Name",
            "IsActive",
            "WasActiveAt",
            "IsBuyOrder?",
            "PercentageOrder",
            "Region",
            "Station",
            "Character",
            "MultipleOrders",
            "DaysLeft",
            "Price"});
            this.QueryType.Location = new System.Drawing.Point(5, 5);
            this.QueryType.Name = "QueryType";
            this.QueryType.Size = new System.Drawing.Size(153, 21);
            this.QueryType.TabIndex = 0;
            this.QueryType.SelectedIndexChanged += new System.EventHandler(this.SearchQuery_SelectFilter);
            // 
            // IsActiveOptions
            // 
            this.IsActiveOptions.Controls.Add(this.IsActiveValue);
            this.IsActiveOptions.Location = new System.Drawing.Point(357, 39);
            this.IsActiveOptions.Name = "IsActiveOptions";
            this.IsActiveOptions.Padding = new System.Windows.Forms.Padding(5);
            this.IsActiveOptions.Size = new System.Drawing.Size(179, 30);
            this.IsActiveOptions.TabIndex = 1;
            this.IsActiveOptions.Visible = false;
            // 
            // IsActiveValue
            // 
            this.IsActiveValue.AutoSize = true;
            this.IsActiveValue.Checked = true;
            this.IsActiveValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsActiveValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IsActiveValue.Location = new System.Drawing.Point(5, 5);
            this.IsActiveValue.Name = "IsActiveValue";
            this.IsActiveValue.Size = new System.Drawing.Size(169, 20);
            this.IsActiveValue.TabIndex = 0;
            this.IsActiveValue.Text = "Is Active?";
            this.IsActiveValue.UseVisualStyleBackColor = true;
            this.IsActiveValue.CheckedChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(503, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(33, 33);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::EveTradingHelper.Properties.Resources.plus;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_Click);
            // 
            // RegionOptionPanel
            // 
            this.RegionOptionPanel.Controls.Add(this.RegionOption);
            this.RegionOptionPanel.Location = new System.Drawing.Point(284, 70);
            this.RegionOptionPanel.Name = "RegionOptionPanel";
            this.RegionOptionPanel.Padding = new System.Windows.Forms.Padding(5);
            this.RegionOptionPanel.Size = new System.Drawing.Size(200, 101);
            this.RegionOptionPanel.TabIndex = 3;
            // 
            // RegionOption
            // 
            this.RegionOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegionOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RegionOption.FormattingEnabled = true;
            this.RegionOption.Location = new System.Drawing.Point(5, 5);
            this.RegionOption.Name = "RegionOption";
            this.RegionOption.Size = new System.Drawing.Size(190, 21);
            this.RegionOption.TabIndex = 0;
            this.RegionOption.SelectedIndexChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            this.RegionOption.TextUpdate += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // StationPanel
            // 
            this.StationPanel.Controls.Add(this.StationValue);
            this.StationPanel.Location = new System.Drawing.Point(306, 130);
            this.StationPanel.Name = "StationPanel";
            this.StationPanel.Padding = new System.Windows.Forms.Padding(5);
            this.StationPanel.Size = new System.Drawing.Size(200, 100);
            this.StationPanel.TabIndex = 4;
            // 
            // StationValue
            // 
            this.StationValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StationValue.FormattingEnabled = true;
            this.StationValue.Location = new System.Drawing.Point(5, 5);
            this.StationValue.Name = "StationValue";
            this.StationValue.Size = new System.Drawing.Size(190, 21);
            this.StationValue.TabIndex = 0;
            this.StationValue.SelectedIndexChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            this.StationValue.TextUpdate += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // CharacterPanel
            // 
            this.CharacterPanel.Controls.Add(this.CharacterValue);
            this.CharacterPanel.Location = new System.Drawing.Point(250, 130);
            this.CharacterPanel.Name = "CharacterPanel";
            this.CharacterPanel.Padding = new System.Windows.Forms.Padding(5);
            this.CharacterPanel.Size = new System.Drawing.Size(340, 33);
            this.CharacterPanel.TabIndex = 5;
            // 
            // CharacterValue
            // 
            this.CharacterValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CharacterValue.FormattingEnabled = true;
            this.CharacterValue.Location = new System.Drawing.Point(5, 5);
            this.CharacterValue.Name = "CharacterValue";
            this.CharacterValue.Size = new System.Drawing.Size(330, 21);
            this.CharacterValue.TabIndex = 0;
            this.CharacterValue.SelectedIndexChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            this.CharacterValue.TextUpdate += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // IsBuyPanel
            // 
            this.IsBuyPanel.Controls.Add(this.IsBuyValue);
            this.IsBuyPanel.Location = new System.Drawing.Point(179, 130);
            this.IsBuyPanel.Name = "IsBuyPanel";
            this.IsBuyPanel.Padding = new System.Windows.Forms.Padding(5);
            this.IsBuyPanel.Size = new System.Drawing.Size(179, 30);
            this.IsBuyPanel.TabIndex = 6;
            this.IsBuyPanel.Visible = false;
            // 
            // IsBuyValue
            // 
            this.IsBuyValue.AutoSize = true;
            this.IsBuyValue.Checked = true;
            this.IsBuyValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsBuyValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IsBuyValue.Location = new System.Drawing.Point(5, 5);
            this.IsBuyValue.Name = "IsBuyValue";
            this.IsBuyValue.Size = new System.Drawing.Size(169, 20);
            this.IsBuyValue.TabIndex = 0;
            this.IsBuyValue.Text = "Is Buy Order?";
            this.IsBuyValue.UseVisualStyleBackColor = true;
            this.IsBuyValue.CheckedChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // NamePanel
            // 
            this.NamePanel.Controls.Add(this.NameValue);
            this.NamePanel.Location = new System.Drawing.Point(169, 130);
            this.NamePanel.Name = "NamePanel";
            this.NamePanel.Padding = new System.Windows.Forms.Padding(5);
            this.NamePanel.Size = new System.Drawing.Size(340, 33);
            this.NamePanel.TabIndex = 7;
            // 
            // NameValue
            // 
            this.NameValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameValue.FormattingEnabled = true;
            this.NameValue.Location = new System.Drawing.Point(5, 5);
            this.NameValue.Name = "NameValue";
            this.NameValue.Size = new System.Drawing.Size(330, 21);
            this.NameValue.TabIndex = 0;
            this.NameValue.SelectedIndexChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            this.NameValue.TextUpdate += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // WasActiveAtPanel
            // 
            this.WasActiveAtPanel.Controls.Add(this.WasActiveValueDateTime);
            this.WasActiveAtPanel.Controls.Add(this.WasActiveValueButton);
            this.WasActiveAtPanel.Location = new System.Drawing.Point(214, 130);
            this.WasActiveAtPanel.Name = "WasActiveAtPanel";
            this.WasActiveAtPanel.Padding = new System.Windows.Forms.Padding(5);
            this.WasActiveAtPanel.Size = new System.Drawing.Size(200, 100);
            this.WasActiveAtPanel.TabIndex = 8;
            // 
            // WasActiveValueDateTime
            // 
            this.WasActiveValueDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WasActiveValueDateTime.Location = new System.Drawing.Point(80, 5);
            this.WasActiveValueDateTime.Name = "WasActiveValueDateTime";
            this.WasActiveValueDateTime.Size = new System.Drawing.Size(115, 20);
            this.WasActiveValueDateTime.TabIndex = 1;
            this.WasActiveValueDateTime.ValueChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // WasActiveValueButton
            // 
            this.WasActiveValueButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.WasActiveValueButton.Location = new System.Drawing.Point(5, 5);
            this.WasActiveValueButton.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.WasActiveValueButton.Name = "WasActiveValueButton";
            this.WasActiveValueButton.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.WasActiveValueButton.Size = new System.Drawing.Size(75, 90);
            this.WasActiveValueButton.TabIndex = 0;
            this.WasActiveValueButton.Text = "was active";
            this.WasActiveValueButton.UseVisualStyleBackColor = true;
            this.WasActiveValueButton.Click += new System.EventHandler(this.ButtonSwapActiveState);
            // 
            // PercentageOrderPanel
            // 
            this.PercentageOrderPanel.Controls.Add(this.PercentageOrderText);
            this.PercentageOrderPanel.Controls.Add(this.PercentageOrderCombo);
            this.PercentageOrderPanel.Location = new System.Drawing.Point(163, 130);
            this.PercentageOrderPanel.Name = "PercentageOrderPanel";
            this.PercentageOrderPanel.Padding = new System.Windows.Forms.Padding(5);
            this.PercentageOrderPanel.Size = new System.Drawing.Size(340, 33);
            this.PercentageOrderPanel.TabIndex = 9;
            // 
            // PercentageOrderText
            // 
            this.PercentageOrderText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PercentageOrderText.Items.Add("0,10");
            this.PercentageOrderText.Items.Add("0,25");
            this.PercentageOrderText.Items.Add("0,50");
            this.PercentageOrderText.Items.Add("0,75");
            this.PercentageOrderText.Items.Add("1,0");
            this.PercentageOrderText.Location = new System.Drawing.Point(58, 5);
            this.PercentageOrderText.Name = "PercentageOrderText";
            this.PercentageOrderText.Size = new System.Drawing.Size(277, 20);
            this.PercentageOrderText.TabIndex = 1;
            this.PercentageOrderText.Text = "0,50";
            this.PercentageOrderText.TextChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // PercentageOrderCombo
            // 
            this.PercentageOrderCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.PercentageOrderCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PercentageOrderCombo.FormattingEnabled = true;
            this.PercentageOrderCombo.Items.AddRange(new object[] {
            "==",
            "!=",
            ">=",
            "<=",
            "<",
            ">"});
            this.PercentageOrderCombo.Location = new System.Drawing.Point(5, 5);
            this.PercentageOrderCombo.Name = "PercentageOrderCombo";
            this.PercentageOrderCombo.Size = new System.Drawing.Size(53, 21);
            this.PercentageOrderCombo.TabIndex = 0;
            this.PercentageOrderCombo.SelectedIndexChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            this.PercentageOrderCombo.TextUpdate += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // MultipleOrdersInStationPanel
            // 
            this.MultipleOrdersInStationPanel.Controls.Add(this.MultipleOrdersValue);
            this.MultipleOrdersInStationPanel.Location = new System.Drawing.Point(163, 130);
            this.MultipleOrdersInStationPanel.Name = "MultipleOrdersInStationPanel";
            this.MultipleOrdersInStationPanel.Padding = new System.Windows.Forms.Padding(5);
            this.MultipleOrdersInStationPanel.Size = new System.Drawing.Size(340, 33);
            this.MultipleOrdersInStationPanel.TabIndex = 10;
            // 
            // MultipleOrdersValue
            // 
            this.MultipleOrdersValue.AutoSize = true;
            this.MultipleOrdersValue.Checked = true;
            this.MultipleOrdersValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MultipleOrdersValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MultipleOrdersValue.Location = new System.Drawing.Point(5, 5);
            this.MultipleOrdersValue.Name = "MultipleOrdersValue";
            this.MultipleOrdersValue.Size = new System.Drawing.Size(330, 23);
            this.MultipleOrdersValue.TabIndex = 0;
            this.MultipleOrdersValue.Text = "Multiple Orders of same type in station";
            this.MultipleOrdersValue.UseVisualStyleBackColor = true;
            this.MultipleOrdersValue.CheckedChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // DaysLeftOnOrderPanel
            // 
            this.DaysLeftOnOrderPanel.Controls.Add(this.DaysLeftOnOrderValue);
            this.DaysLeftOnOrderPanel.Controls.Add(this.DaysLeftOnOrderCombo);
            this.DaysLeftOnOrderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DaysLeftOnOrderPanel.Location = new System.Drawing.Point(163, 0);
            this.DaysLeftOnOrderPanel.Name = "DaysLeftOnOrderPanel";
            this.DaysLeftOnOrderPanel.Padding = new System.Windows.Forms.Padding(5);
            this.DaysLeftOnOrderPanel.Size = new System.Drawing.Size(340, 33);
            this.DaysLeftOnOrderPanel.TabIndex = 11;
            // 
            // DaysLeftOnOrderCombo
            // 
            this.DaysLeftOnOrderCombo.Dock = System.Windows.Forms.DockStyle.Left;
            this.DaysLeftOnOrderCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DaysLeftOnOrderCombo.FormattingEnabled = true;
            this.DaysLeftOnOrderCombo.Items.AddRange(new object[] {
            ">=",
            "<="});
            this.DaysLeftOnOrderCombo.Location = new System.Drawing.Point(5, 5);
            this.DaysLeftOnOrderCombo.Name = "DaysLeftOnOrderCombo";
            this.DaysLeftOnOrderCombo.Size = new System.Drawing.Size(53, 21);
            this.DaysLeftOnOrderCombo.TabIndex = 0;
            this.DaysLeftOnOrderCombo.SelectionChangeCommitted += new System.EventHandler(this.TriggerFieldUpdateEvent);
            this.DaysLeftOnOrderCombo.TextUpdate += new System.EventHandler(this.TriggerFieldUpdateEvent);
            this.DaysLeftOnOrderCombo.TextChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // DaysLeftOnOrderValue
            // 
            this.DaysLeftOnOrderValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DaysLeftOnOrderValue.Location = new System.Drawing.Point(58, 5);
            this.DaysLeftOnOrderValue.Name = "DaysLeftOnOrderValue";
            this.DaysLeftOnOrderValue.Size = new System.Drawing.Size(277, 20);
            this.DaysLeftOnOrderValue.TabIndex = 1;
            this.DaysLeftOnOrderValue.TextChanged += new System.EventHandler(this.TriggerFieldUpdateEvent);
            // 
            // SearchCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.DaysLeftOnOrderPanel);
            this.Controls.Add(this.MultipleOrdersInStationPanel);
            this.Controls.Add(this.PercentageOrderPanel);
            this.Controls.Add(this.WasActiveAtPanel);
            this.Controls.Add(this.NamePanel);
            this.Controls.Add(this.IsBuyPanel);
            this.Controls.Add(this.CharacterPanel);
            this.Controls.Add(this.StationPanel);
            this.Controls.Add(this.RegionOptionPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.IsActiveOptions);
            this.Controls.Add(this.searchOptionPanel);
            this.Name = "SearchCondition";
            this.Size = new System.Drawing.Size(536, 33);
            this.searchOptionPanel.ResumeLayout(false);
            this.IsActiveOptions.ResumeLayout(false);
            this.IsActiveOptions.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.RegionOptionPanel.ResumeLayout(false);
            this.StationPanel.ResumeLayout(false);
            this.CharacterPanel.ResumeLayout(false);
            this.IsBuyPanel.ResumeLayout(false);
            this.IsBuyPanel.PerformLayout();
            this.NamePanel.ResumeLayout(false);
            this.WasActiveAtPanel.ResumeLayout(false);
            this.PercentageOrderPanel.ResumeLayout(false);
            this.MultipleOrdersInStationPanel.ResumeLayout(false);
            this.MultipleOrdersInStationPanel.PerformLayout();
            this.DaysLeftOnOrderPanel.ResumeLayout(false);
            this.DaysLeftOnOrderPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchOptionPanel;
        private System.Windows.Forms.ComboBox QueryType;
        private System.Windows.Forms.Panel IsActiveOptions;
        private System.Windows.Forms.CheckBox IsActiveValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel RegionOptionPanel;
        private System.Windows.Forms.ComboBox RegionOption;
        private System.Windows.Forms.Panel StationPanel;
        private System.Windows.Forms.ComboBox StationValue;
        private System.Windows.Forms.Panel CharacterPanel;
        private System.Windows.Forms.ComboBox CharacterValue;
        private System.Windows.Forms.Panel IsBuyPanel;
        private System.Windows.Forms.CheckBox IsBuyValue;
        private System.Windows.Forms.Panel NamePanel;
        private System.Windows.Forms.ComboBox NameValue;
        private System.Windows.Forms.Panel WasActiveAtPanel;
        private System.Windows.Forms.DateTimePicker WasActiveValueDateTime;
        private System.Windows.Forms.Button WasActiveValueButton;
        private System.Windows.Forms.Panel PercentageOrderPanel;
        private System.Windows.Forms.DomainUpDown PercentageOrderText;
        private System.Windows.Forms.ComboBox PercentageOrderCombo;
        private System.Windows.Forms.Panel MultipleOrdersInStationPanel;
        private System.Windows.Forms.CheckBox MultipleOrdersValue;
        private System.Windows.Forms.Panel DaysLeftOnOrderPanel;
        private System.Windows.Forms.TextBox DaysLeftOnOrderValue;
        private System.Windows.Forms.ComboBox DaysLeftOnOrderCombo;
    }
}
