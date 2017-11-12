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
            this.BasePanel = new System.Windows.Forms.Panel();
            this.searchOptionPanel.SuspendLayout();
            this.IsActiveOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchOptionPanel
            // 
            this.searchOptionPanel.Controls.Add(this.QueryType);
            this.searchOptionPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.searchOptionPanel.Location = new System.Drawing.Point(5, 1);
            this.searchOptionPanel.Name = "searchOptionPanel";
            this.searchOptionPanel.Padding = new System.Windows.Forms.Padding(1, 1, 5, 1);
            this.searchOptionPanel.Size = new System.Drawing.Size(163, 26);
            this.searchOptionPanel.TabIndex = 0;
            // 
            // QueryType
            // 
            this.QueryType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QueryType.FormattingEnabled = true;
            this.QueryType.Location = new System.Drawing.Point(1, 1);
            this.QueryType.Name = "QueryType";
            this.QueryType.Size = new System.Drawing.Size(157, 21);
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
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(505, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(26, 26);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::EveTradingHelper.Properties.Resources.plus;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_Click);
            // 
            // BasePanel
            // 
            this.BasePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BasePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BasePanel.Location = new System.Drawing.Point(168, 1);
            this.BasePanel.Name = "BasePanel";
            this.BasePanel.Size = new System.Drawing.Size(337, 26);
            this.BasePanel.TabIndex = 3;
            // 
            // SearchCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.BasePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.IsActiveOptions);
            this.Controls.Add(this.searchOptionPanel);
            this.Name = "SearchCondition";
            this.Padding = new System.Windows.Forms.Padding(5, 1, 5, 1);
            this.Size = new System.Drawing.Size(536, 28);
            this.searchOptionPanel.ResumeLayout(false);
            this.IsActiveOptions.ResumeLayout(false);
            this.IsActiveOptions.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchOptionPanel;
        private System.Windows.Forms.ComboBox QueryType;
        private System.Windows.Forms.Panel IsActiveOptions;
        private System.Windows.Forms.CheckBox IsActiveValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel BasePanel;
    }
}
