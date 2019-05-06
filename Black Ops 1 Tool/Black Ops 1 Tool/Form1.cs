using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3Util;

namespace Black_Ops_1_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS3Util.PS3.Connect();
            connectionToolStripMenuItem.ForeColor = Color.Green;
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult doEnd = MessageBox.Show("Are You Done Playing?", "Are you sure?", MessageBoxButtons.YesNoCancel);
            if (doEnd == DialogResult.Yes)
            {
                PS3Util.PS3.Disconnect();
                connectionToolStripMenuItem.ForeColor = Color.Red;
            }
            else if (doEnd == DialogResult.No)
            {

            }
            else if (doEnd == DialogResult.Cancel)
            {

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult doCredits = MessageBox.Show("Do you want to see the credits?", "Are you sure?",
                                                     MessageBoxButtons.YesNo);
            if (doCredits == DialogResult.Yes)
            {
                MessageBox.Show("Created by primetime43\nThanks to xSonoro\nJoin NextGenUpdate.com");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //The Stat
            string selected = (string) comboBox1.SelectedItem;

            if (selected == "Unlock All")
            {
                MessageBox.Show("Just Click Set Stat To Execute");
            }
            else if (selected == "Low Stats")
            {
                MessageBox.Show("Just Click Set Stat To Execute");
            }
            else if (selected == "Medium Stats")
            {
                MessageBox.Show("Just Click Set Stat To Execute");
            }
            else if (selected == "Max Stats")
            {
                MessageBox.Show("Just Click Set Stat To Execute");
            }
        }



        public int ReadDaysPlayed(ulong i)
        {
            return (int) (i/86400);
        }

        public int ReadHoursPlayed(ulong i)
        {
            return (int) ((i - (ulong) (ReadDaysPlayed(i)*24*60*60))/3600);
        }

        public int ReadMinutesPlayed(ulong i)
        {
            return (int) ((i - (ulong) (ReadDaysPlayed(i)*24*60*60) - (ulong) (ReadHoursPlayed(i)*60*60))/60);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Stat's value
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selected = (string) comboBox1.SelectedItem;

            if (selected == "Prestige")
            {
                byte[] prestige = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020946dd, prestige);
            }
            else if (selected == "Rank")
            {
                if (numericUpDown1.Value == 1)
                {
                    byte[] level = new byte[] {0x00, 0x00, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x01};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 2)
                {
                    byte[] level = new byte[] {0x2c, 0x01, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x02};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 3)
                {
                    byte[] level = new byte[] {0xb0, 0x04, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x03};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 4)
                {
                    byte[] level = new byte[] {0x8c, 0x0a, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x04};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 5)
                {
                    byte[] level = new byte[] {0xc0, 0x12, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x05};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 6)
                {
                    byte[] level = new byte[] {0x4c, 0x1d, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x06};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 7)
                {
                    byte[] level = new byte[] {0x94, 0x2a, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x07};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 8)
                {
                    byte[] level = new byte[] {0x98, 0x3a, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x08};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 9)
                {
                    byte[] level = new byte[] {0x58, 0x4d, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x09};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 10)
                {
                    byte[] level = new byte[] {0xd4, 0x62, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x0a};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 11)
                {
                    byte[] level = new byte[] {0x0c, 0x7b, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x0b};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 12)
                {
                    byte[] level = new byte[] {0xc8, 0x96, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x0c};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 13)
                {
                    byte[] level = new byte[] {0x08, 0xb6, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x0d};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 14)
                {
                    byte[] level = new byte[] {0xcc, 0xd8, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x0e};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 15)
                {
                    byte[] level = new byte[] {0x14, 0xff, 0x00, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 16)
                {
                    byte[] level = new byte[] {0xe0, 0x28, 0x01, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 17)
                {
                    byte[] level = new byte[] {0xf8, 0x56, 0x01, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 18)
                {
                    byte[] level = new byte[] {0x5c, 0x89, 0x01, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 19)
                {
                    byte[] level = new byte[] {0x0c, 0xc0, 0x01, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 20)
                {
                    byte[] level = new byte[] {0x08, 0xfb, 0x01, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 21)
                {
                    byte[] level = new byte[] {0x50, 0x3a, 0x02, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 22)
                {
                    byte[] level = new byte[] {0x48, 0x7e, 0x02, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 23)
                {
                    byte[] level = new byte[] {0xf0, 0xc6, 0x02, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 24)
                {
                    byte[] level = new byte[] {0x48, 0x14, 0x03, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 25)
                {
                    byte[] level = new byte[] {0x50, 0x66, 0x03, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 26)
                {
                    byte[] level = new byte[] {0x08, 0xbd, 0x03, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 27)
                {
                    byte[] level = new byte[] {0xd4, 0x18, 0x04, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 28)
                {
                    byte[] level = new byte[] {0xb4, 0x79, 0x04, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 29)
                {
                    byte[] level = new byte[] {0xa8, 0xdf, 0x04, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 30)
                {
                    byte[] level = new byte[] {0xb0, 0x4a, 0x05, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 31)
                {
                    byte[] level = new byte[] {0xcc, 0xba, 0x05, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 32)
                {
                    byte[] level = new byte[] {0x60, 0x30, 0x06, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 33)
                {
                    byte[] level = new byte[] {0x6c, 0xab, 0x06, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 34)
                {
                    byte[] level = new byte[] {0xf0, 0x2b, 0x07, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 35)
                {
                    byte[] level = new byte[] {0xec, 0xb1, 0x07, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 36)
                {
                    byte[] level = new byte[] {0x60, 0x3d, 0x08, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 37)
                {
                    byte[] level = new byte[] {0xb0, 0xce, 0x08, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 38)
                {
                    byte[] level = new byte[] {0xdc, 0x65, 0x09, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 39)
                {
                    byte[] level = new byte[] {0xe4, 0x02, 0x0a, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 40)
                {
                    byte[] level = new byte[] {0xc8, 0xa5, 0x0a, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 41)
                {
                    byte[] level = new byte[] {0x88, 0x4e, 0x0b, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 42)
                {
                    byte[] level = new byte[] {0x88, 0xfd, 0x0b, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 43)
                {
                    byte[] level = new byte[] {0xc8, 0xb2, 0x0c, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 44)
                {
                    byte[] level = new byte[] {0x48, 0x6e, 0x0d, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 45)
                {
                    byte[] level = new byte[] {0x08, 0x30, 0x0e, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 46)
                {
                    byte[] level = new byte[] {0x08, 0xf8, 0x0e, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 47)
                {
                    byte[] level = new byte[] {0xac, 0xc6, 0x0f, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 48)
                {
                    byte[] level = new byte[] {0xf4, 0x9b, 0x10, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                }
                else if (numericUpDown1.Value == 49)
                {
                    byte[] level = new byte[] {0xe0, 0x77, 0x11, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x30};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
                else if (numericUpDown1.Value == 50)
                {
                    byte[] level = new byte[] {0x70, 0x5a, 0x12, 0x00};
                    PS3Util.PS3.SetMemory(0x020946e5, level);
                    byte[] name = new byte[] {0x31};
                    PS3Util.PS3.SetMemory(0x020946e1, name);
                }
            }
            else if (selected == "Kills")
            {
                byte[] kills = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x02094429, kills);
            }
            else if (selected == "Deaths")
            {
                byte[] deaths = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020942f5, deaths);
            }
            else if (selected == "Assists")
            {
                byte[] assists = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x02094291, assists);
            }
            else if (selected == "Wins")
            {
                byte[] wins = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x0209475d, wins);
            }
            else if (selected == "Losses")
            {
                byte[] losses = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020944dd, losses);
            }
            else if (selected == "Cod Points")
            {
                byte[] codpoints = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020942d1, codpoints);
            }
            else if (selected == "Headshots")
            {
                byte[] head = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x0209440d, head);
            }
            else if (selected == "Games Played")
            {
                byte[] games = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020938b1, games);
            }
            else if (selected == "Wager Match Earnings")
            {
                byte[] wager = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020944d9, wager);
            }
            else if (selected == "Paid Contracts")
            {
                byte[] paid = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020942d5, paid);
            }
            else if (selected == "Lifetime Earnings")
            {
                byte[] life = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value));
                PS3Util.PS3.SetMemory(0x020942f1, life);
            }
            else if (selected == "Time Played Days")
            {
                int newDays = (int) numericUpDown1.Value*86400;
                PS3Util.PS3.SetMemory(0x10, BitConverter.GetBytes(newDays));
            }
            else if (selected == "Time Played Hours")
            {
                int val = (int) numericUpDown1.Value*60*60;
                PS3Util.PS3.SetMemory(0x10, BitConverter.GetBytes(val));
            }
            else if (selected == "Time Played Minutes")
            {
                int val = (int) numericUpDown1.Value*60;
                PS3Util.PS3.SetMemory(0x10, BitConverter.GetBytes(val));
            }
            else if (selected == "Unlock All")
            {
                PS3Util.PS3.SetMemory(0x0208be10, UnlockAll.Unlock1);
                PS3Util.PS3.SetMemory(0x02094990, UnlockAll.Unlock2);
                byte[] fix = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x0209390a, fix);
                byte[] fix1 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x02093ddd, fix1);
                byte[] fix2 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x02094032, fix2);
                byte[] fix3 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x02093d84, fix3);
                byte[] fix4 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x0209408b, fix4);
                byte[] fix5 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x02093e96, fix5);
                byte[] fix6 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x02093ef0, fix6);
                byte[] fix7 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x0208d894, fix7);
                byte[] fix8 = {0x00, 0x00, 0x00, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020938b1, fix8);
            }
            else if (selected == "Best Win Streak")
            {
                byte[] streak = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value*2));
                PS3Util.PS3.SetMemory(0x0208d894, streak);
            }
            else if (selected == "Best Kill Streak")
            {
                byte[] streak = BitConverter.GetBytes(Convert.ToInt32(numericUpDown1.Value*2));
                PS3Util.PS3.SetMemory(0x0208d890, streak);
            }
            else if (selected == "Low Stats")
            {
                byte[] kills = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x02094429, kills);
                byte[] deaths = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020942f5, deaths);
                byte[] assists = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x02094291, assists);
                byte[] wins = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x0209475d, wins);
                byte[] losses = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020944dd, losses);
                byte[] codpoints = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020942d1, codpoints);
                byte[] head = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x0209440d, head);
                byte[] games = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020938b1, games);
                byte[] wager = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020944d9, wager);
                byte[] paid = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020942d5, paid);
                byte[] life = {0x39, 0x05, 0x00, 0x00};
                PS3Util.PS3.SetMemory(0x020942f1, life);
            }
            else if (selected == "Medium Stats")
            {
                byte[] kills = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x02094429, kills);
                byte[] deaths = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x020942f5, deaths);
                byte[] assists = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x02094291, assists);
                byte[] wins = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x0209475d, wins);
                byte[] losses = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x020944dd, losses);
                byte[] codpoints = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x020942d1, codpoints);
                byte[] head = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x0209440d, head);
                byte[] games = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x020938b1, games);
                byte[] wager = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x020944d9, wager);
                byte[] paid = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x020942d5, paid);
                byte[] life = {0x90, 0x02, 0xcc, 0x00};
                PS3Util.PS3.SetMemory(0x020942f1, life);
            }
            else if (selected == "Max Stats")
            {
                byte[] kills = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x02094429, kills);
                byte[] deaths = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x020942f5, deaths);
                byte[] assists = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x02094291, assists);
                byte[] wins = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x0209475d, wins);
                byte[] losses = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x020944dd, losses);
                byte[] codpoints = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x020942d1, codpoints);
                byte[] head = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x0209440d, head);
                byte[] games = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x020938b1, games);
                byte[] wager = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x020944d9, wager);
                byte[] paid = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x020942d5, paid);
                byte[] life = {0xff, 0xff, 0xff, 0x7f};
                PS3Util.PS3.SetMemory(0x020942f1, life);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] CheatProtect = new byte[] {0x60, 0x00, 0x00, 0x00};
            PS3Util.PS3.SetMemory(0x4B8EC4, CheatProtect);
            byte[] CheatProtec1 = new byte[] {0x60, 0x00, 0x00, 0x00};
            PS3Util.PS3.SetMemory(0x4B8ED0, CheatProtec1);
            byte[] CheatProtect2 = new byte[] {0x3B, 0x20, 0x00, 0x00};
            PS3Util.PS3.SetMemory(0x4B8ED4, CheatProtect2);
            MessageBox.Show("Cheat Protection Removed!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] RSA_PATCHED = new byte[]
                {
                    0x60, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x60
                    ,
                    0x00, 0x00, 0x00
                };
            PS3Util.PS3.SetMemory(0x1718C0, RSA_PATCHED);
            MessageBox.Show("RSA Signature Patched!");
        }

        public static bool[] Controller = new bool[18];

        #region Variables

        public static bool[] GodMode = new bool[18];
        public static bool[] UfoMode = new bool[18];
        public static bool[] Blackbird = new bool[18];
        public static bool[] NoClip = new bool[18];
        public static bool[] SuperSpeed = new bool[18];
        public static bool[] LagSwitch = new bool[18];
        public static bool[] InfAmmo = new bool[18];

        #endregion

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int Index = (int) dataGridView1.CurrentRow.Cells[0].Value;
                if (!Controller[(int) dataGridView1.CurrentRow.Cells[0].Value])
                {
                    PlayerManager playerManager =
                        new PlayerManager(
                            PS3Util.PS3.ReadString(0x13950C8 +
                                                   (uint) ((uint) Index*0x2A38) +
                                                   0x2808
                                ), Index, this);
                    playerManager.Show();
                }
                else
                {
                    MessageBox.Show(
                        "A player manager is already opened for the client " +
                        dataGridView1.CurrentRow.Cells[1].Value.ToString(), "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show(
                    "Unable to perform this action", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
            }
        }

        private void GetClientNames(out string[] names)
        {
            names = new string[18];
            uint client = 0x13950C8;
            for (int index = 0; index < 18; ++index)
            {
                names[index] = PS3Util.PS3.ReplaceString(PS3Util.PS3.ReadString(client + (uint) (index*0x2A38) + 0x2808));
            }
        }

        public void doRefresh()
        {
            button8.PerformClick();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] names;
            GetClientNames(out names);
            dataGridView1.Rows.Clear();
            for (int i = 0; i <= 18; i++)
            {
                try
                {
                    if (Encoding.ASCII.GetBytes(names[i])[0] != 0x00)
                    {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = i;
                            dataGridView1.Rows[i].Cells[1].Value = names[i];
                            //
                            byte[] Health = PS3Util.PS3.GetBytes(0x0139786F + ((uint) i*0x2A38), 4);
                            if (Health[2] == 0xFF) dataGridView1.Rows[i].Cells[2].Value = "True";
                            else dataGridView1.Rows[i].Cells[2].Value = "False";
                            //
                            byte[] black = PS3Util.PS3.GetBytes(0x01397AEB + ((uint) i*0x2A38), 4);
                            if (black[0] == 0x03) dataGridView1.Rows[i].Cells[5].Value = "True";
                            else dataGridView1.Rows[i].Cells[5].Value = "False";
                            // 
                            byte[] noClip = PS3Util.PS3.GetBytes(0x013979BF + ((uint) i*0x2A38), 4);
                            if (noClip[0] == 0x02) dataGridView1.Rows[i].Cells[4].Value = "True";
                            else dataGridView1.Rows[i].Cells[4].Value = "False";
                            //
                            byte[] PrimaryClip = PS3Util.PS3.GetBytes(0x013954CB + ((uint) i*0x2A38), 2);
                            int Clip = BitConverter.ToInt16(PrimaryClip, 0);
                            byte[] Primaryright = PS3Util.PS3.GetBytes(0x01395453 + ((uint)i * 0x2A38), 2);
                            int Left = BitConverter.ToInt16(Primaryright, 0);
                            dataGridView1.Rows[i].Cells[7].Value = Clip.ToString() + @"/" + Left.ToString();
                            //
                            byte[] SecondaryClip = PS3Util.PS3.GetBytes(0x013954C3 + ((uint) i*0x2A38), 2);
                            int clip = BitConverter.ToInt16(SecondaryClip, 0);
                            byte[] SecondaryRight = PS3Util.PS3.GetBytes(0x0139544B + ((uint)i * 0x2A38), 2);
                            int left = BitConverter.ToInt16(SecondaryRight, 0);
                            dataGridView1.Rows[i].Cells[6].Value = clip.ToString() + @"/" + left.ToString();
                            //
                            byte[] Super = PS3Util.PS3.GetBytes(0x01397881 + ((uint) i*0x2A38), 4);
                            if (Super[0] == 0xFF) dataGridView1.Rows[i].Cells[3].Value = "True";
                            else dataGridView1.Rows[i].Cells[3].Value = "False";
                            //
                            byte[] Lag = PS3Util.PS3.GetBytes(0x013977DB + ((uint) i*0x2A38), 4);
                            if (Lag[0] == 0x02) dataGridView1.Rows[i].Cells[8].Value = "Is Not Lagging";
                            else dataGridView1.Rows[i].Cells[8].Value = "Is Lagging";
                            //
                            byte[] Status = PS3Util.PS3.GetBytes(0x0139793B + ((uint) i*0x2A38), 4);
                            if (Status[0] == 0x01) dataGridView1.Rows[i].Cells[9].Value = "Dead";
                            else dataGridView1.Rows[i].Cells[9].Value = "Alive";
                    }
                }
                catch
                {

                }
            }
        }
        private byte[] see = new byte[0x400];
        private void button9_Click(object sender, EventArgs e)
        {
            byte[] server = Encoding.ASCII.GetBytes(textBox2.Text + '\0'); //Game Server Command
            PS3Util.PS3.SetMemory(0x009149E0, server);
            PS3Util.PS3.GetMemory(0x009149E0, ref see);
            richTextBox1.Text = Encoding.ASCII.GetString(see);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PS3Util.PS3.SetMemory(0x009149E0, ResetGameServerCommand.reseter); //Game Server Command Reset
            richTextBox1.Clear();
            textBox2.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here are some of the commands to use in the Game Server Command:\nv - Set Dvars (v cg_drawFPS 1)\ne - Killfeed Text\ni - Say CMD Text\nw - Server Disconnect w/ text (Kick)\nc - Center Text");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult test = MessageBox.Show("Do you need help?", "Are you sure?", MessageBoxButtons.YesNo);
            DialogResult help = MessageBox.Show("Do you want to go to NextGenUpdate.com?", "For help", MessageBoxButtons.YesNo);
            if (help == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("http://www.nextgenupdate.com/forums/members/435321-primetime43.html");
                }
                catch { }
            }
            else if (help == DialogResult.No)
            {
                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync(this);
                }
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Form1 i = (Form1)e.Argument;
            while (true)
            {
                this.Invoke(new Action(() => { button8.PerformClick(); }));
                System.Threading.Thread.Sleep(2000);
                Application.DoEvents();
            }
        }

        private void doesntConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Well if you are getting an error when trying to connect, do this:\n- Go into target manager and delete the current target, then find a new one afterwards, and then connect with your new target!\n Enjoy, primetime43! :D");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            uint Class = (uint)numericUpDown2.Value;
            uint addr = 0xD24C88 + 4 * Class;
            uint ptr = GetPointer(GetPointer(addr) + 0x18);
            string add = "";
            if (checkBox2.CheckState == CheckState.Checked)
            {
                add = "^F";
            }
            PS3Util.PS3.SetMemory(ptr, Encoding.ASCII.GetBytes(add + textBox3.Text + '\0'));
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            uint Class = (uint) numericUpDown2.Value;
            uint addr = 0xD24C88 + 4*Class;
            uint ptr = GetPointer(GetPointer(addr) + 0x18);
            textBox3.Text = PS3.ReadString(ptr);
        }
        private uint GetPointer(uint addr)
        {
            return  (uint)PS3.GetInt32(addr);

        }
        private int doo;

        private byte[] name = new byte[0x20];
        private void button2_Click_1(object sender, EventArgs e)
        {
            doo = 1;
            PS3Util.PS3.GetMemory(0x02000934, ref name);
            textBox1.Text = Encoding.ASCII.GetString(name);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (doo == 1)
            {
                byte[] name = Encoding.ASCII.GetBytes(textBox1.Text + '\0'); //PSN Name
                PS3Util.PS3.SetMemory(0x02000934, name);
            }
            else if (doo == 2)
            {
                byte[] clan = Encoding.ASCII.GetBytes(textBox1.Text + '\0'); //Clantag
                PS3Util.PS3.SetMemory(0x01B137EC, clan);
            }
        }

        private byte[] tag = new byte[0x4];
        private void button3_Click_1(object sender, EventArgs e)
        {
            doo = 2;
            PS3Util.PS3.GetMemory(0x01B137EC, ref tag);
            textBox1.Text = Encoding.ASCII.GetString(tag);
        }
    }
}
