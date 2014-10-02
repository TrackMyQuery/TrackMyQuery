namespace TrackMyQuery
{
    partial class QueryProfilesTrackingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGetPlan = new System.Windows.Forms.Button();
            this.textBoxSessionId = new System.Windows.Forms.TextBox();
            this.showPlanWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // buttonGetPlan
            // 
            this.buttonGetPlan.Location = new System.Drawing.Point(297, 12);
            this.buttonGetPlan.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.buttonGetPlan.Name = "buttonGetPlan";
            this.buttonGetPlan.Size = new System.Drawing.Size(200, 200);
            this.buttonGetPlan.TabIndex = 0;
            this.buttonGetPlan.Text = "Show Plan";
            this.buttonGetPlan.UseVisualStyleBackColor = true;
            this.buttonGetPlan.Click += new System.EventHandler(this.buttonGetPlan_Click);
            // 
            // textBoxSessionId
            // 
            this.textBoxSessionId.Location = new System.Drawing.Point(720, 12);
            this.textBoxSessionId.Multiline = true;
            this.textBoxSessionId.Name = "textBoxSessionId";
            this.textBoxSessionId.Size = new System.Drawing.Size(200, 200);
            this.textBoxSessionId.TabIndex = 2;
            this.textBoxSessionId.Text = "\r\n\r\n\r\nType Session ID";
            this.textBoxSessionId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSessionId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSessionId_MouseClick);
            // 
            // showPlanWebBrowser
            // 
            this.showPlanWebBrowser.Location = new System.Drawing.Point(3, 218);
            this.showPlanWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.showPlanWebBrowser.Name = "showPlanWebBrowser";
            this.showPlanWebBrowser.Size = new System.Drawing.Size(2500, 2500);
            this.showPlanWebBrowser.TabIndex = 3;
            // 
            // QueryProfilesTrackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Controls.Add(this.showPlanWebBrowser);
            this.Controls.Add(this.textBoxSessionId);
            this.Controls.Add(this.buttonGetPlan);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "QueryProfilesTrackingForm";
            this.Text = "sys.dm_exec_query_profiles Tracking";
            this.Load += new System.EventHandler(this.QueryProfilesTrackingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetPlan;        
        private System.Windows.Forms.TextBox textBoxSessionId;
        private System.Windows.Forms.WebBrowser showPlanWebBrowser;
    }
}