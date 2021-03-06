﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3Util;

namespace Call_of_Duty_World_at_War_Tool
{
    public partial class GameSendServerCommand : Form
    {
        public GameSendServerCommand()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here are some commands you can try:\nv cl_wadefps 1\nv - set dvar\nc - send middle text\nf - send killfeed text & there are many more!\nThanks to SC58 for these");
        }

        public uint Sv_GameSendSeverCommand = 0x361410;

        private void button3_Click(object sender, EventArgs e)  //0x361410 - SV_GameSendServerCommand
        {
            int client = (int)numericUpDown1.Value;

            byte[] RPCON = new byte[] { 0x38, 0x60, 0xFF, 0xFF, 0x38, 0x80, 0x00, 0x00, 0x3C, 0xA0, 0x02, 0x00, 0x30, 0xA5, 0x50, 0x00, 0x4B, 0xFB, 0x85, 0x2D, 0x4B, 0xFF, 0xFA, 0x38, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00 };
            byte[] DFT = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] RPCOFF = new byte[] { 0x80, 0xD7, 0x00, 0x00, 0x38, 0x80, 0x00, 0x00, 0x83, 0x1E, 0x80, 0x5C, 0x3B, 0x40, 0x00, 0x00, 0x54, 0xCA, 0x30, 0x32, 0xC0, 0x3E, 0x80, 0x78, 0x54, 0xDD, 0x18, 0x38, 0x7C, 0x7D, 0x52, 0x14, 0x7D, 0x03, 0xC2, 0x14, 0x79, 0x03, 0x00, 0x20, 0x4B, 0xFD, 0x7E, 0xAD, 0x60, 0x00, 0x00, 0x00, 0xC0, 0x17, 0x00, 0x28, 0xFC, 0x20, 0x00, 0x90, 0x80, 0xF7, 0x00, 0x00, 0xD8, 0x01, 0x04, 0xA0, 0x7C, 0x79, 0x1B, 0x78, 0x54, 0xFC, 0x30, 0x32, 0xE8, 0x81, 0x04, 0xA0, 0x54, 0xEC, 0x18, 0x38, 0x80, 0x7E, 0x83, 0x28, 0x7D, 0x6C, 0xE2, 0x14, 0x7F, 0x6B, 0xC2, 0x14, 0x48, 0x01, 0xED, 0x81, 0x60, 0x00, 0x00, 0x00, 0x38, 0x00, 0xFF, 0xFF, 0x82, 0xDE, 0x80, 0x64, 0x78, 0x64, 0x00, 0x20, 0xC0, 0x3E, 0x82, 0xF8, 0x7B, 0x63, 0x00, 0x20, 0xC0, 0x5E, 0x82, 0x88, 0x78, 0x05, 0x00, 0x60, 0xC0, 0x7E, 0x80, 0x78, 0x7B, 0x26, 0x00, 0x20, 0x39, 0x20, 0x00, 0x00, 0x39, 0x40, 0x00, 0x00, 0xFA, 0xC1, 0x00, 0x78, 0xFB, 0x41, 0x00, 0x80, 0x4B, 0xFD, 0x82, 0x25, 0x60, 0x00, 0x00, 0x00, 0x4B, 0xFF, 0xF9, 0x34, 0x81, 0x3E, 0x80, 0x58, 0x39, 0x61, 0x00, 0xA0 };
            byte[] ON = new byte[] { 0x41 };
            byte[] OFF = new byte[] { 0x40 };
            byte[] sv = new byte[] { };
            WaWStats.RPC.Call(Sv_GameSendSeverCommand, client, 1, textBox2.Text);
            PS3Util.PS3.SetMemory(0x2005000, sv);
            PS3Util.PS3.SetMemory(0x3A88A4, ON);
            PS3Util.PS3.SetMemory(0x3A8ED4, RPCON);
            System.Threading.Thread.Sleep(15);
            PS3Util.PS3.SetMemory(0x3A88A4, OFF);
            PS3Util.PS3.SetMemory(0x3A8ED4, RPCOFF);
            PS3Util.PS3.SetMemory(0x2005000, DFT);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Well the top GameSendServer Command works, but isn't the real professional one, meaning that it will have the problem such as you not being able to hit select and whatever else it disables. The one above will also keep showing your message if you are showing one.\nThe one below, the real one sends your message once, and everything works fine as if you aren't even using this. Also, the real one means you don't have to reset memory! :D\nThanks to SC58 for the real one! ");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal selected = (decimal)numericUpDown1.Value;

            if (selected == 0)
            {
                byte[] client0 = new byte[0x20];
                PS3Util.PS3.GetMemory(0x0119124c + (uint)(0 * 0x3C6C) + 0x2F9C8, ref client0);
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

        private void label1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Here are some commands you can use:\nc - send middle text\nf - send killfeed text\nh - Displays say Text\nt - bring options menu UI\nv - sends commands/any dvar\nw - kick player with message\nX - disable sound system");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
