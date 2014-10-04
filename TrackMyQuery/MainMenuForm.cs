using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TrackMyQuery
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void ButtonUseCXPacket_Click(object sender, EventArgs e)
        {
            new CXPacketTrackingForm(textBoxServerName.Text).Show();
        }

        private void ButtonQueryProfiles_Click(object sender, EventArgs e)
        {
            new QueryProfilesTrackingForm(textBoxServerName.Text).Show();
        }

        private void textBoxServerName_MouseClick(Object sender, MouseEventArgs e)
        {
            textBoxServerName.Text = null;
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnector dbHelper = new DbConnector("Server=" + textBoxServerName.Text + ";Trusted_Connection=True;", Providers.SqlServer))
                {
                    dbHelper.Connection.Open();
                    MessageBox.Show("Connected Successfully");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ooops. Something's wrong: \n" + ex.Message.ToString());
            }

        }

    }
}
