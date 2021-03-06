﻿using System;
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
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        /*#region Variables

        public static bool[] GodMode = new bool[18];
        public static bool[] UfoMode = new bool[18];
        public static bool[] Blackbird = new bool[18];
        public static bool[] NoClip = new bool[18];
        public static bool[] SuperSpeed = new bool[18];
        public static bool[] LagSwitch = new bool[18];
        public static bool[] InfAmmo = new bool[18];

        #endregion*/

        private void GetClientNames(out string[] names)
        {
            names = new string[18];
            uint client = 0x0119124c;
            for (int index = 0; index < 18; ++index)
            {
                names[index] = PS3Util.PS3.ReplaceString(PS3Util.PS3.ReadString(client + (uint)(index * 0x3C6C) + 0x2F9C8));
            }
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
                    }
                }
                catch
                {

                }
            }
        }

        public void doRefresh()
        {
            button8.PerformClick();
        }

        public static bool[] Controller = new bool[18];
        public uint Sv_GameSendSeverCommand = 0x361410;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int Index = (int)dataGridView1.CurrentRow.Cells[0].Value;
                if (!Controller[(int) dataGridView1.CurrentRow.Cells[0].Value])
                {
                    playermanageplayer players = new playermanageplayer(PS3Util.PS3.ReadString(0x0119124c +(uint)((uint)Index * 0x3C6C) +0x2F9C8), Index, this);
                    byte[] RPCON = new byte[] { 0x38, 0x60, 0xFF, 0xFF, 0x38, 0x80, 0x00, 0x00, 0x3C, 0xA0, 0x02, 0x00, 0x30, 0xA5, 0x50, 0x00, 0x4B, 0xFB, 0x85, 0x2D, 0x4B, 0xFF, 0xFA, 0x38, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00 };
                    byte[] DFT = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    byte[] RPCOFF = new byte[] { 0x80, 0xD7, 0x00, 0x00, 0x38, 0x80, 0x00, 0x00, 0x83, 0x1E, 0x80, 0x5C, 0x3B, 0x40, 0x00, 0x00, 0x54, 0xCA, 0x30, 0x32, 0xC0, 0x3E, 0x80, 0x78, 0x54, 0xDD, 0x18, 0x38, 0x7C, 0x7D, 0x52, 0x14, 0x7D, 0x03, 0xC2, 0x14, 0x79, 0x03, 0x00, 0x20, 0x4B, 0xFD, 0x7E, 0xAD, 0x60, 0x00, 0x00, 0x00, 0xC0, 0x17, 0x00, 0x28, 0xFC, 0x20, 0x00, 0x90, 0x80, 0xF7, 0x00, 0x00, 0xD8, 0x01, 0x04, 0xA0, 0x7C, 0x79, 0x1B, 0x78, 0x54, 0xFC, 0x30, 0x32, 0xE8, 0x81, 0x04, 0xA0, 0x54, 0xEC, 0x18, 0x38, 0x80, 0x7E, 0x83, 0x28, 0x7D, 0x6C, 0xE2, 0x14, 0x7F, 0x6B, 0xC2, 0x14, 0x48, 0x01, 0xED, 0x81, 0x60, 0x00, 0x00, 0x00, 0x38, 0x00, 0xFF, 0xFF, 0x82, 0xDE, 0x80, 0x64, 0x78, 0x64, 0x00, 0x20, 0xC0, 0x3E, 0x82, 0xF8, 0x7B, 0x63, 0x00, 0x20, 0xC0, 0x5E, 0x82, 0x88, 0x78, 0x05, 0x00, 0x60, 0xC0, 0x7E, 0x80, 0x78, 0x7B, 0x26, 0x00, 0x20, 0x39, 0x20, 0x00, 0x00, 0x39, 0x40, 0x00, 0x00, 0xFA, 0xC1, 0x00, 0x78, 0xFB, 0x41, 0x00, 0x80, 0x4B, 0xFD, 0x82, 0x25, 0x60, 0x00, 0x00, 0x00, 0x4B, 0xFF, 0xF9, 0x34, 0x81, 0x3E, 0x80, 0x58, 0x39, 0x61, 0x00, 0xA0 };
                    byte[] ON = new byte[] { 0x41 };
                    byte[] OFF = new byte[] { 0x40 };
                    byte[] sv = new byte[] { };
                    WaWStats.RPC.Call(Sv_GameSendSeverCommand, Index, 1, "w " + textBox1.Text + "");
                    PS3Util.PS3.SetMemory(0x2005000, sv);
                    PS3Util.PS3.SetMemory(0x3A88A4, ON);
                    PS3Util.PS3.SetMemory(0x3A8ED4, RPCON);
                    System.Threading.Thread.Sleep(15);
                    PS3Util.PS3.SetMemory(0x3A88A4, OFF);
                    PS3Util.PS3.SetMemory(0x3A8ED4, RPCOFF);
                    PS3Util.PS3.SetMemory(0x2005000, DFT);
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Form1 i = (Form1)e.Argument;
            while (true)
            {
                this.Invoke(new Action(() => button8.PerformClick()));
                System.Threading.Thread.Sleep(2000);
                Application.DoEvents();
            }
        }
    }
}