using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3Lib;

namespace Modern_Warfare_2_All_Clients_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void siteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var message =
                MessageBox.Show(
                    "Modern Warfare 2 All Clients Tool by primetime43\nThanks to SC58 for RPC!\nThanks to xSonoro!\nThanks to aerosoul94 for his image injector\nGo to NextGenUpdate.com?",
                    "Visit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (message == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("http://www.nextgenupdate.com/forums/members/435321-primetime43.html");
            }
            else if (message == DialogResult.No)
            {

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal selected = (decimal) numericUpDown1.Value;

            if (selected == numericUpDown1.Value)
            {
                byte[] client = new byte[0x20];
                PS3Util.PS3.GetMemory(0x014dda40 + (uint) (selected*0x3700) + 0x7A50, ref client);
                textBox1.Text = Encoding.ASCII.GetString(client);
            }
        }

        public uint Sv_GameSendSeverCommand = 0x0021A0A0;

        private void button3_Click(object sender, EventArgs e)
        {
            int client = (int) numericUpDown1.Value;
            var test = textBox1.Text;
            RPC.Call(Sv_GameSendSeverCommand, -1, 1, "v loc_warnings 0");
            RPC.Call(Sv_GameSendSeverCommand, -1, 1, "v loc_warningsAsErrors 0");

            RPC.Call(Sv_GameSendSeverCommand, client, 1, textBox2.Text);
            MessageBox.Show("Command Sent To " + test);
        }

        private void quickMapRestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPC.Call(0x002131A8, 1); // Map restart 1 quick 0 not quick
        }

        private void accountManagementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Account().Show();
        }

        private void nonAllClientsAccountManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path.GetTempPath() + "\\MW2 All Clients Tool"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\MW2 All Clients Tool");
            }
            if (!File.Exists(Path.GetTempPath() + "\\MW2 All Clients Tool\\Modern Warfare 2 RTM Tool.exe"))
            {
                File.WriteAllBytes(Path.GetTempPath() + "\\MW2 All Clients Tool\\Modern Warfare 2 RTM Tool.exe", (byte[])Properties.Resources.Modern_Warfare_2_RTM_Tool);
            }
            if (!File.Exists(Path.GetTempPath() + "\\MW2 All Clients Tool\\Devcomponents.DotNetBar2.dll"))
            {
                File.WriteAllBytes(Path.GetTempPath() + "\\MW2 All Clients Tool\\DevComponents.DotNetBar2.dll", (byte[])Properties.Resources.DevComponents_DotNetBar2);
            }
            if (!File.Exists(Path.GetTempPath() + "\\MW2 All Clients Tool\\ps3tmapi_net.dll"))
            {
                File.WriteAllBytes(Path.GetTempPath() + "\\MW2 All Clients Tool\\ps3tmapi_net.dll", (byte[])Properties.Resources.ps3tmapi_net);
            }
            Process.Start(Path.GetTempPath() + "\\MW2 All Clients Tool\\Modern Warfare 2 RTM Tool.exe");
        }

        private void dEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PS3Util.PS3.Connect() == true)
            {
                RPC.Enable_RPC();
                RPC.Init();
                connectionToolStripMenuItem.ForeColor = Color.Green;
            }
            else
            {
                MessageBox.Show("Couldn't Connect!\nTry to delete your target in target manager and reconnect!");
                Process.Start("ps3tm.exe");
            }
        }

        private PS3API PS3 = new PS3API();

        private void CEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PS3.ConnectTarget() != true)
            {
                MessageBox.Show("Connected!");
                if (PS3.AttachProcess() == true)
                {
                    MessageBox.Show("Process Attached!");
                    RPC.Enable_RPC();
                    RPC.Init();
                    connectionToolStripMenuItem.ForeColor = Color.Green;
                }
                else
                {
                    MessageBox.Show("Couldn't attach!");
                }
            }
            else
            {
                MessageBox.Show("Couldn't Connect!");
            }
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int client = (int)numericUpDown1.Value;

            RPC.Call(Sv_GameSendSeverCommand, client, 1, "v party_connectTimeout 1; party_host 1; party_hostmigration 0; onlinegame 1; onlinegameandhost 1; onlineunrankedgameandhost 0; migration_msgtimeout 0; migration_timeBetween 999999; migration_verboseBroadcastTime 0; migrationPingTime 0; bandwidthtest_duration 0; bandwidthtest_enable 0; bandwidthtest_ingame_enable 0; bandwidthtest_timeout 0; cl_migrationTimeout 0; lobby_partySearchWaitTime 0; bandwidthtest_announceinterval 0; partymigrate_broadcast_interval 99999; partymigrate_pingtest_timeout 0; partymigrate_timeout 0; partymigrate_timeoutmax 0; partymigrate_pingtest_retry 0; partymigrate_pingtest_timeout 0; g_kickHostIfIdle 0; sv_cheats 1; scr_dom_scorelimit 0; xblive_playEvenIfDown 1; party_hostmigration 0; badhost_endGameIfISuck 0; badhost_maxDoISuckFrames 0; badhost_maxHappyPingTime 99999; badhost_minTotalClientsForHappyTest 99999; bandwidthtest_enable 0");
            MessageBox.Show("Force Host Enabled!");
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int client = (int)numericUpDown1.Value;

            RPC.Call(Sv_GameSendSeverCommand, client, 1, "v reset party_connectTimeout; reset party_host; reset party_hostmigration; reset onlinegame; reset onlinegameandhost; reset onlineunrankedgameandhost; reset migration_msgtimeout; reset migration_timeBetween; reset migration_verboseBroadcastTime; reset migrationPingTime; reset bandwidthtest_duration; reset bandwidthtest_enable; reset bandwidthtest_ingame_enable; reset bandwidthtest_timeout; reset cl_migrationTimeout; reset lobby_partySearchWaitTime; reset bandwidthtest_announceinterval; reset partymigrate_broadcast_interval; reset partymigrate_pingtest_timeout; reset partymigrate_timeout; reset partymigrate_timeoutmax; reset partymigrate_pingtest_retry;reset partymigrate_pingtest_timeout; reset g_kickHostIfIdle; reset sv_cheats; reset scr_dom_scorelimit; reset xblive_playEvenIfDown; reset party_hostmigration; reset badhost_endGameIfISuck ; reset badhost_maxDoISuckFrames; reset badhost_maxHappyPingTime ; reset badhost_minTotalClientsForHappyTest ; reset bandwidthtest_enable");
            MessageBox.Show("Force Host Disabled!");
        }

        private void imageInjectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path.GetTempPath() + "\\MW2 All Clients Tool"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\MW2 All Clients Tool");
            }
            if (!File.Exists(Path.GetTempPath() + "\\MW2 All Clients Tool\\Image Injector.exe"))
            {
                File.WriteAllBytes(Path.GetTempPath() + "\\MW2 All Clients Tool\\Image Injector.exe", (byte[])Properties.Resources.Image_Injector);
            }
            Process.Start(Path.GetTempPath() + "\\MW2 All Clients Tool\\Image Injector.exe");
        }

        private void dEXToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PS3Util.PS3.Disconnect();
            connectionToolStripMenuItem.ForeColor = Color.Black;
        }

        private void cEXToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PS3.DisconnectTarget();
            connectionToolStripMenuItem.ForeColor = Color.Black;
            MessageBox.Show("Disconnected!");
        }
    }
}
