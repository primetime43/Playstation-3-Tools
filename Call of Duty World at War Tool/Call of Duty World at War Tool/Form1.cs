using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Call_of_Duty_World_at_War_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PS3Util.PS3.Connect() == true)
            {
                connectToolStripMenuItem.ForeColor = Color.Green;
                byte[] name = new byte[0x20];
                PS3Util.PS3.GetMemory(0x02952934, ref name);
                string PCname = System.Environment.MachineName;
                label5.Text = (PCname);
                label2.Text = Encoding.ASCII.GetString(name);
                label3.Text = LocalIPAddress();
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                WaWStats.PS3.Init();
                //
                /*string[] lines = { LocalIPAddress() };
                System.IO.File.WriteAllLines(@"IP Dump.txt", lines);*/ //dumps text file to where the program is at. Dumps the ip that is loaded.

                
                MessageBox.Show("Done! Click on your name to edit your psn. :P");
            }
            else
            {
                MessageBox.Show("Couldn't Connect!\nTry to delete your target in target manager and reconnect!");
                Process.Start("ps3tm.exe");
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {

                PS3Util.PS3.Disconnect();
                string name = System.Environment.MachineName;
                MessageBox.Show(name, "Cya Later!");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 show = new AboutBox1();
            show.ShowDialog(); // Shows aboutbox
        }

        private void siteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult site = MessageBox.Show("Do you want to go to PortalCentric to my profile page?", "Are you sure?",MessageBoxButtons.YesNo);
            if (site == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("http://portalcentric.net/forums/members/primetime43.876/");
                }
                catch { }
            }
        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void gameSendServerCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameSendServerCommand f2 = new GameSendServerCommand();
            f2.Show(); // Shows GameSendServerCommand form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clients f2 = new Clients();
            f2.Show(); //Shows clients form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mods f2 = new Mods();
            f2.Show(); //Shows Patches form
        }

        private void label2_Click(object sender, EventArgs e)
        {
            name f2 = new name();
            f2.Show(); //Shows name form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stats f2 = new Stats();
            f2.Show(); //Shows Stats form
        }
    }
}
