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
using System.Diagnostics;
<<<<<<< HEAD
using System.Data.Common;
=======
using System.Threading;
>>>>>>> parent of ee90067... Added a DBConnector class

namespace TrackMyQuery
{
    public partial class CXPacketTrackingForm : Form
    {

        static bool isThereAPlan = true;
        private DateTime LastClickTime = DateTime.Now;
        private string serverName;
        private DbConnector dbHelper = null;

        public CXPacketTrackingForm(string i_serverName)
        {
            serverName = i_serverName;
            dbHelper = new DbConnector("Server=" + serverName + ";Trusted_Connection=True;");
            InitializeComponent();
        }

        private void CXPacketTrackingForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private static void getCXPacketNodeIDs(int session_id, List<int> NodeIds, DbConnector dbHelper)
        {
            string query = "select RIGHT (resource_description, CHARINDEX (N'=', REVERSE (resource_description)) - 1) as nodeId " +
                "from sys.dm_os_waiting_tasks " +
                "where wait_type = N'CXPacket' " +
                "and session_id = @session_id";// +session_id.ToString();

            dbHelper.AddParameter("@session_id", session_id);
            DbDataReader reader = dbHelper.ExecuteReader(query, System.Data.CommandType.Text, ConnectionState.KeepOpen);

            while (reader.Read())
            {
                int nodeId = (int.Parse)(reader["nodeId"].ToString());
                //Add new node only if the value does not already exist
                if (!(NodeIds.Contains(nodeId)))
                {
                    NodeIds.Add(nodeId);
                }

            }
        }


        private static string getExecutionPlanXML(int session_id, DbConnector dbHelper)
        {
            string query = "select query_plan " +
                                "from sys.dm_exec_requests " +
                                "cross apply sys.dm_exec_text_query_plan " +
                                "(plan_handle, statement_start_offset, statement_end_offset) " +
                                "where session_id = @session_id";// +session_id.ToString();

            string executionPlanXML = null;
            try
            {
                dbHelper.AddParameter("@session_id", session_id);
                executionPlanXML = dbHelper.ExecuteScalar(query, System.Data.CommandType.Text, ConnectionState.KeepOpen).ToString();
            }
            catch
            {
                isThereAPlan = false;
            }

            return executionPlanXML;
        }

        private static int findQPNodePositionByNodeId(List<int> QPNodePositions, int nodeIdPos)
        {
            int posToReturn = -1;
            //find the maximum position which is smaller than the nodeId position
            foreach (int pos in QPNodePositions)
            {
                if (pos < nodeIdPos)
                {
                    posToReturn = pos;
                }
            }
            return posToReturn;
        }

        private static void findPositions(string text, List<int> positions, string stringToFind)
        {
            int index = 0;
            do
            {
                index = text.IndexOf(stringToFind, index);
                if (index != -1)
                {
                    positions.Add(index);
                    index++;
                }
            } while (index != -1);
        }  

        private void buttonGetPlan_Click(object sender, EventArgs e)
        {
            //<meta http-equiv="X-UA-Compatible" content="IE=9" /> 
           
            if (DateTime.Now.Add(new TimeSpan(0,0,-3)) > LastClickTime) //don't start working if less than 3 seconds have passed from the last button click. This is due to collisions between threads
            {                
                LastClickTime = DateTime.Now;
                new System.Threading.Thread(doWork).Start();
            }            
        }
        
        private void doWork(object obj)
        {
            List<int> QPNodePositions = new List<int>();
            List<int> NodeIds = new List<int>();
            //SqlConnection connection = new SqlConnection("Server=localhost;Database=AdventureWorks2012;Trusted_Connection=True;");
            dbHelper.Connection.Open();
            
            //Get ExecutionPlanXML and write it to disk for processing with qp.bat            
            string ExecutionPlanXML = getExecutionPlanXML(int.Parse(textBoxSessionId.Text), dbHelper);
            if (ExecutionPlanXML != null)
            {
                System.IO.File.WriteAllText(@"C:\temp\HTMLQueryPlan\ExecutionPlan.sqlplan", ExecutionPlanXML.ToString());
                getCXPacketNodeIDs(int.Parse(textBoxSessionId.Text), NodeIds, dbHelper);
                //Process execution plan in external application
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\temp\HTMLQueryPlan\qp.bat";
                startInfo.Arguments = @"C:\temp\HTMLQueryPlan\ExecutionPlan.sqlplan C:\temp\HTMLQueryPlan\PlanFromExternalApp";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo);
                //System.Diagnostics.Process.Start(@"C:\temp\HTMLQueryPlan\qp.bat", @"C:\temp\HTMLQueryPlan\ExecutionPlan.sqlplan C:\temp\HTMLQueryPlan\PlanFromExternalApp");


                //Read the file
                string text = System.IO.File.ReadAllText(@"C:\temp\HTMLQueryPlan\PlanFromExternalApp.html");
                //find positions
                findPositions(text, QPNodePositions, "qp-node");


                foreach (int nodeId in NodeIds)
                {
                    int nodeIdPos = text.IndexOf("Node ID</th><td>" + nodeId);
                    int QPPos = findQPNodePositionByNodeId(QPNodePositions, nodeIdPos);
                    var stringBuilder = new StringBuilder(text);
                    stringBuilder.Remove(QPPos, 7);
                    stringBuilder.Insert(QPPos, "qp-node Highlight");
                    text = stringBuilder.ToString();
                    //recalculate postitions 
                    QPNodePositions = new List<int>();
                    findPositions(text, QPNodePositions, "qp-node");
                }


                ////replacement code from http://stackoverflow.com/questions/5015593/how-to-replace-part-of-string-by-position
                //add IE9 property to the header
                text = text.Replace("<html><head>", "<html><head><meta http-equiv='X-UA-Compatible' content='IE=9'>");

                //Write output file
                System.IO.File.WriteAllText(@"C:\temp\HTMLQueryPlan\output.html", text.ToString());
                dbHelper.Dispose();
                //Show revised execution plan
                //webBrowser1.Navigate(new Uri(@"C:\temp\HTMLQueryPlan\output.html"));  
                showPlanWebBrowser.Invoke(new UpdatePlanOnUICallback(this.updatePlanOnUI), new object[] { @"C:\temp\HTMLQueryPlan\output.html" });
            }
            else
            {
                MessageBox.Show("Query Finished");
            }
        }

        public delegate void UpdatePlanOnUICallback(string path);

        private void updatePlanOnUI(string path)
        {
            showPlanWebBrowser.Navigate(new Uri(@"C:\temp\HTMLQueryPlan\output.html"));
        }

        private void textBoxSessionId_MouseClick(Object sender, MouseEventArgs e)
        {
            textBoxSessionId.Text = null;
        }

        private void textBoxSessionId_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
