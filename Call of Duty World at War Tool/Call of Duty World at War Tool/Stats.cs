using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Call_of_Duty_World_at_War_Tool
{
    public partial class Stats : Form
    {
        public Stats()
        {
            InitializeComponent();
        }

        public uint Sv_GameSendSeverCommand = 0x361410;

        //2301 - RANKXP
        //2302 - SCORE
        //2303 - KILLS
        //2304 - KILL_STREAK
        //2305 - DEATHS
        //2306 - DEATH_STREAK
        //2307 - ASSISTS
        //2308 - HEADSHOTS
        //2309 - TEAMKILLS
        //2310 - SUICIDES
        //2311 - TIME_PLAYED_ALLIES
        //2312 - TIME_PLAYED_OPFOR
        //2313 - TIME_PLAYED_OTHER
        //2314 - TIME_PLAYED_TOTAL
        //2315 - KDRATIO
        //2316 - WINS
        //2317 - LOSSES
        //2318 - TIES
        //2319 - WIN_STREAK
        //2320 - CUR_WIN_STREAK
        //2321 - WLRATIO
        //2322 - HITS
        //2323 - MISSES
        //2324 - TOTAL_SHOTS
        //2325 - ACCURACY
        //2326 - PLEVEL
        //2350 - RANK

        public void button1_Click(object sender, EventArgs e)
        {
            /*WaWStats.RPC.Call(Sv_GameSendSeverCommand, -1, 1, "v loc_warningsUI 0");
            WaWStats.RPC.Call(Sv_GameSendSeverCommand, -1, 1, "v loc_warnings 0");

            int client = (int)numericUpDown1.Value;

            if (checkBox1.Checked)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Please Wait.....\"");

                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 3000 4294967296 N 3001 4294967296 N 3002 4294967296 N 3003 4294967296 N 3004 4294967296 N 3005 4294967296 N 3006 4294967296 N 3007 4294967296 N 3008 4294967296 N 3009 4294967296 N 3010 4294967296 N 3011 4294967296 N 3012 4294967296 N 3013 4294967296 N 3014 4294967296 N 3015 4294967296 N 3016 4294967296 N 3017 4294967296 N 3018 4294967296 N 3019 4294967296 N 3020 4294967296 N 3021 4294967296");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 3022 4294967296 N 3023 4294967296 N 3024 4294967296 N 3025 4294967296 N 3026 4294967296 N 3027 4294967296 N 3028 4294967296 N 3029 4294967296 N 3030 4294967296 N 3031 4294967296 N 3032 4294967296 N 3033 4294967296 N 3034 4294967296 N 3035 4294967296 N 3036 4294967296 N 3037 4294967296 N 3038 4294967296 N 3039 4294967296 N 3040 4294967296 N 3041 4294967296 N 3042 4294967296 N 3043 4294967296");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 3044 4294967296 N 3045 4294967296 N 3046 4294967296 N 3047 4294967296 N 3048 4294967296 N 3049 4294967296 N 3050 4294967296 N 3051 4294967296 N 3052 4294967296 N 3053 4294967296 N 3054 4294967296 N 3055 4294967296 N 3056 4294967296 N 3057 4294967296 N 3058 4294967296 N 3059 4294967296 N 3060 4294967296 N 3061 4294967296 N 3062 4294967296 N 3063 4294967296 N 3064 4294967296 N 3065 4294967296");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 3066 4294967296 N 3067 4294967296 N 3068 4294967296 N 3069 4294967296 N 3070 4294967296 N 3071 4294967296 N 3072 4294967296 N 3073 4294967296 N 3074 4294967296 N 3075 4294967296 N 3076 4294967296 N 3077 4294967296 N 3078 4294967296 N 3079 4294967296 N 3080 4294967296 N 3081 4294967296 N 3082 4294967296 N 3083 4294967296 N 3084 4294967296 N 3085 4294967296 N 3086 4294967296 N 3087 4294967296");

                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "c \"^6Everything Has Been Unlocked !\"");
            }
            if (checkBox2.Checked)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2301 148680");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Level 65 Set\"");
            }
            if (checkBox3.Checked)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2301 0");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Level 1 Set\"");
            }
            if (comboBox1.Text == "None")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 0");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 0 Set\"");
            }
            if (comboBox1.Text == "Prestige 1")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 1");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 1 Set\"");
            }
            if (comboBox1.Text == "Prestige 2")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 2");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 2 Set\"");
            }
            if (comboBox1.Text == "Prestige 3")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 3");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 3 Set\"");
            }
            if (comboBox1.Text == "Prestige 4")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 4");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 4 Set\"");
            }
            if (comboBox1.Text == "Prestige 5")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 5");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 5 Set\"");
            }
            if (comboBox1.Text == "Prestige 6")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 6");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 6 Set\"");
            }
            if (comboBox1.Text == "Prestige 7")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 7");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 7 Set\"");
            }
            if (comboBox1.Text == "Prestige 8")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 8");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 8 Set\"");
            }
            if (comboBox1.Text == "Prestige 9")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 9");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 9 Set\"");
            }
            if (comboBox1.Text == "Prestige 10")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 10");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 10 Set\"");
            }
            if (comboBox1.Text == "Prestige 11")
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2326 11");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Prestige 11 Set\"");
            }
            if (numericUpDown2.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2302 " + numericUpDown2.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Score Set\"");
            }
            if (numericUpDown3.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2316 " + numericUpDown3.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Wins Set\"");
            }
            if (numericUpDown4.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2317 " + numericUpDown4.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Losses Set\"");
            }
            if (numericUpDown5.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2319 " + numericUpDown5.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Win Streak Set\"");
            }
            if (numericUpDown6.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2303 " + numericUpDown6.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Kills Set\"");
            }
            if (numericUpDown7.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2305 " + numericUpDown7.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Deaths Set\"");
            }
            if (numericUpDown8.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2304 " + numericUpDown8.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Kill Streak Set\"");
            }
            if (numericUpDown9.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2308 " + numericUpDown9.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Headshots Set\"");
            }
            if (numericUpDown10.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2313 " + numericUpDown10.Value * 86400 + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2314 " + numericUpDown10.Value * 86400 + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom Time Played Set\"");
            }
            if (numericUpDown11.Value != 0)
            {
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "N 2301 " + numericUpDown11.Value + "");
                WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, "f \"Custom XP Set\"");
            }*/
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal selected = (decimal) numericUpDown1.Value;

            if (selected == 0)
            {
                byte[] client0 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint) (0*0x3C6C) + 0x2F9C8, ref client0);
                textBox1.Text = Encoding.ASCII.GetString(client0);
            }
            else if (selected == 1)
            {
                byte[] client1 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(1 * 0x3C6C) + 0x2F9C8, ref client1);
                textBox1.Text = Encoding.ASCII.GetString(client1);
            }
            else if (selected == 2)
            {
                byte[] client2 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(2 * 0x3C6C) + 0x2F9C8, ref client2);
                textBox1.Text = Encoding.ASCII.GetString(client2);
            }
            else if (selected == 3)
            {
                byte[] client3 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(3 * 0x3C6C) + 0x2F9C8, ref client3);
                textBox1.Text = Encoding.ASCII.GetString(client3);
            }
            else if (selected == 4)
            {
                byte[] client4 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(4 * 0x3C6C) + 0x2F9C8, ref client4);
                textBox1.Text = Encoding.ASCII.GetString(client4);
            }
            else if (selected == 5)
            {
                byte[] client5 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(5 * 0x3C6C) + 0x2F9C8, ref client5);
                textBox1.Text = Encoding.ASCII.GetString(client5);
            }
            else if (selected == 6)
            {
                byte[] client6 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(6 * 0x3C6C) + 0x2F9C8, ref client6);
                textBox1.Text = Encoding.ASCII.GetString(client6);
            }
            else if (selected == 7)
            {
                byte[] client7 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(7 * 0x3C6C) + 0x2F9C8, ref client7);
                textBox1.Text = Encoding.ASCII.GetString(client7);
            }
            else if (selected == 8)
            {
                byte[] client8 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(8 * 0x3C6C) + 0x2F9C8, ref client8);
                textBox1.Text = Encoding.ASCII.GetString(client8);
            }
            else if (selected == 9)
            {
                byte[] client9 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(9 * 0x3C6C) + 0x2F9C8, ref client9);
                textBox1.Text = Encoding.ASCII.GetString(client9);
            }
            else if (selected == 10)
            {
                byte[] client10 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(10 * 0x3C6C) + 0x2F9C8, ref client10);
                textBox1.Text = Encoding.ASCII.GetString(client10);
            }
            else if (selected == 11)
            {
                byte[] client11 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(11 * 0x3C6C) + 0x2F9C8, ref client11);
                textBox1.Text = Encoding.ASCII.GetString(client11);
            }
            else if (selected == 12)
            {
                byte[] client12 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(12 * 0x3C6C) + 0x2F9C8, ref client12);
                textBox1.Text = Encoding.ASCII.GetString(client12);
            }
            else if (selected == 13)
            {
                byte[] client13 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(13 * 0x3C6C) + 0x2F9C8, ref client13);
                textBox1.Text = Encoding.ASCII.GetString(client13);
            }
            else if (selected == 14)
            {
                byte[] client14 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(14 * 0x3C6C) + 0x2F9C8, ref client14);
                textBox1.Text = Encoding.ASCII.GetString(client14);
            }
            else if (selected == 15)
            {
                byte[] client15 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(15 * 0x3C6C) + 0x2F9C8, ref client15);
                textBox1.Text = Encoding.ASCII.GetString(client15);
            }
            else if (selected == 16)
            {
                byte[] client16 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(16 * 0x3C6C) + 0x2F9C8, ref client16);
                textBox1.Text = Encoding.ASCII.GetString(client16);
            }
            else if (selected == 17)
            {
                byte[] client17 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(17 * 0x3C6C) + 0x2F9C8, ref client17);
                textBox1.Text = Encoding.ASCII.GetString(client17);
            }
            else if (selected == 18)
            {
                byte[] client18 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(18 * 0x3C6C) + 0x2F9C8, ref client18);
                textBox1.Text = Encoding.ASCII.GetString(client18);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Enabled = false;
                numericUpDown11.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
                numericUpDown11.Enabled = true;
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox3.Enabled = false;
                numericUpDown11.Enabled = false;
            }
            else
            {
                checkBox3.Enabled = true;
                numericUpDown11.Enabled = true;
            }
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown11.Value != 0)
            {
                checkBox3.Enabled = false;
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox3.Enabled = true;
                checkBox2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WaWStats.RPC.Call(0x2468D0, 0x10040000, 0x23, "mp/gametypesTable.csv", 1, -1);
        }
    }
}
