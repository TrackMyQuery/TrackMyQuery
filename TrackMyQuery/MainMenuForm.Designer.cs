namespace TrackMyQuery
{
    partial class MainMenuForm
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
            this.ButtonUseCXPacket = new System.Windows.Forms.Button();
            this.ButtonQueryProfiles = new System.Windows.Forms.Button();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonUseCXPacket
            // 
            this.ButtonUseCXPacket.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonUseCXPacket.Location = new System.Drawing.Point(12, 211);
            this.ButtonUseCXPacket.Name = "ButtonUseCXPacket";
            this.ButtonUseCXPacket.Size = new System.Drawing.Size(578, 180);
            this.ButtonUseCXPacket.TabIndex = 0;
            this.ButtonUseCXPacket.Text = "Use CXPacket Waits";
            this.ButtonUseCXPacket.UseVisualStyleBackColor = true;
            this.ButtonUseCXPacket.Click += new System.EventHandler(this.ButtonUseCXPacket_Click);
            // 
            // ButtonQueryProfiles
            // 
            this.ButtonQueryProfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonQueryProfiles.Location = new System.Drawing.Point(12, 414);
            this.ButtonQueryProfiles.Name = "ButtonQueryProfiles";
            this.ButtonQueryProfiles.Size = new System.Drawing.Size(578, 180);
            this.ButtonQueryProfiles.TabIndex = 1;
            this.ButtonQueryProfiles.Text = "Use sys.dm_exec_query_profiles";
            this.ButtonQueryProfiles.UseVisualStyleBackColor = true;
            this.ButtonQueryProfiles.Click += new System.EventHandler(this.ButtonQueryProfiles_Click);
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxServerName.Location = new System.Drawing.Point(11, 8);
            this.textBoxServerName.Multiline = true;
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(285, 180);
            this.textBoxServerName.TabIndex = 2;
            this.textBoxServerName.Text = "\r\n\r\n\r\nType Server Name";
            this.textBoxServerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxServerName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxServerName_MouseClick);
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTestConnection.Location = new System.Drawing.Point(304, 8);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(285, 180);
            this.buttonTestConnection.TabIndex = 3;
            this.buttonTestConnection.Text = "Test Connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 605);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.textBoxServerName);
            this.Controls.Add(this.ButtonQueryProfiles);
            this.Controls.Add(this.ButtonUseCXPacket);
            this.Name = "MainMenuForm";
            this.Text = "Track My Query - Main Menu";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonUseCXPacket;
        private System.Windows.Forms.Button ButtonQueryProfiles;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Button buttonTestConnection;
    }
}

