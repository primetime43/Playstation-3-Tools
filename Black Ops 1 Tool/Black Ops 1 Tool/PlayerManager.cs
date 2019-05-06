using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3Util;
namespace Black_Ops_1_Tool
{
    public partial class PlayerManager : Form
    {
        public uint clientState = 0x13950C8;
        public uint ID;
        public Form1 f;
        public PlayerManager(string name,int clientID, Form1 i)
        {
            InitializeComponent();
            f = i;
            Text = "Currently Managing " + name + "(Client " + clientID.ToString() + ")";
            txtName.Text = PS3.ReadString(clientState + (uint)((uint)clientID * 0x2A38) + 0x2808);
            ID = (uint) clientID;
            label2.Text = Form1.GodMode[ID].ToString();
            if (label2.Text == "True") label2.ForeColor = Color.Green;
            else label2.ForeColor = Color.Red;
            //
            label4.Text = Form1.Blackbird[ID].ToString();
            if (label4.Text == "True") label4.ForeColor = Color.Green;
            else label4.ForeColor = Color.Red;
            //
            label6.Text = Form1.NoClip[ID].ToString();
            if (label6.Text == "True") label6.ForeColor = Color.Green;
            else label6.ForeColor = Color.Red;
            //
            label8.Text = Form1.SuperSpeed[ID].ToString();
            if (label8.Text == "True") label8.ForeColor = Color.Green;
            else label8.ForeColor = Color.Red;
            //
            label10.Text = Form1.LagSwitch[ID].ToString();
            if (label10.Text == "True") label10.ForeColor = Color.Green;
            else label10.ForeColor = Color.Red;
            //
            label12.Text = Form1.InfAmmo[ID].ToString();
            if (label12.Text == "True") label12.ForeColor = Color.Green;
            else label12.ForeColor = Color.Red;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            PS3.SetMemory(clientState + (uint)((uint)ID * 0x2A38) + 0x2808, Encoding.ASCII.GetBytes(txtName.Text + "\0"));
        }

        private void PlayerManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.Controller[ID] = false;
        }

        private void godmode(object sender, EventArgs e)
        {
            if (!Form1.GodMode[ID])
            {

                PS3.SetMemory(0x0139786F + (ID * 0x2A38), BitConverter.GetBytes(int.MaxValue));
                Form1.GodMode[ID] = true;
                label2.Text = Form1.GodMode[ID].ToString();
                label2.ForeColor = Color.Green;
                f.doRefresh();
            }
            else if (Form1.GodMode[ID])
            {
                PS3.SetMemory(0x0139786F + (ID * 0x2A38), new byte[] { 0x64, 0x00, 0x00, 0x00 });
                Form1.GodMode[ID] = false;
                label2.Text = Form1.GodMode[ID].ToString();
                label2.ForeColor = Color.Red;
                f.doRefresh();
            }
        }

        private System.Windows.Forms.Timer b = new System.Windows.Forms.Timer();

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Form1.Blackbird[ID])
            {
                label4.Text = Form1.Blackbird[ID].ToString();
                label4.ForeColor = Color.Red;
                Form1.Blackbird[ID] = true;
                b.Tick += b_Tick;
                b.Interval = 100;
                b.Start();
            }
            else if (Form1.Blackbird[ID])
            {
                label4.Text = Form1.Blackbird[ID].ToString();
                label4.ForeColor = Color.Green;
                Form1.Blackbird[ID] = false;
                b.Tick += b_Tick;
                b.Interval = 100;
                b.Start();
            }
        }

        private void b_Tick(object sender, EventArgs e)
        {
            if (Form1.Blackbird[ID])
            {
                PS3.SetMemory(0x01397AEB + (ID*0x2A38), new byte[] {0x00});
                f.doRefresh();
            }
            else if (!Form1.Blackbird[ID])
            {
                PS3.SetMemory(0x01397AEB + (ID*0x2A38), new byte[] {0x03});
                f.doRefresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Form1.NoClip[ID])
            {

                PS3.SetMemory(0x013979BF + (ID * 0x2A38), new byte[] { 0x02 });
                Form1.NoClip[ID] = true;
                label6.Text = Form1.NoClip[ID].ToString();
                label6.ForeColor = Color.Green;
                f.doRefresh();
            }
            else if (Form1.NoClip[ID])
            {
                PS3.SetMemory(0x013979BF + (ID * 0x2A38), new byte[] { 0x00 });
                Form1.NoClip[ID] = false;
                label6.Text = Form1.NoClip[ID].ToString();
                label6.ForeColor = Color.Red;
                f.doRefresh();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Form1.SuperSpeed[ID])
            {

                PS3.SetMemory(0x01397881 + (ID * 0x2A38), new byte[] { 0xff, 0xff, 0xff, 0xff });
                Form1.SuperSpeed[ID] = true;
                label8.Text = Form1.SuperSpeed[ID].ToString();
                label8.ForeColor = Color.Green;
                f.doRefresh();
            }
            else if (Form1.SuperSpeed[ID])
            {
                PS3.SetMemory(0x01397881 + (ID * 0x2A38), new byte[] { 0x80, 0x00, 0x00, 0x00 });
                Form1.SuperSpeed[ID] = false;
                label8.Text = Form1.SuperSpeed[ID].ToString();
                label8.ForeColor = Color.Red;
                f.doRefresh();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!Form1.LagSwitch[ID])
            {

                PS3.SetMemory(0x013977DB + (ID * 0x2A38), new byte[] { 0x00 });//Lag
                Form1.LagSwitch[ID] = true;
                label10.Text = Form1.LagSwitch[ID].ToString();
                label10.ForeColor = Color.Green;
                f.doRefresh();
            }
            else if (Form1.LagSwitch[ID])
            {
                PS3.SetMemory(0x013977DB + (ID * 0x2A38), new byte[] { 0x02 });//No lag
                Form1.LagSwitch[ID] = false;
                label10.Text = Form1.LagSwitch[ID].ToString();
                label10.ForeColor = Color.Red;
                f.doRefresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!Form1.InfAmmo[ID])
            {
                PS3.SetMemory(0x013954CA + (ID * 0x2A38), new byte[] { 0xff, 0xff, 0xff, 0x7f });
                PS3.SetMemory(0x01395452 + (ID * 0x2A38), new byte[] { 0xff, 0xff, 0xff, 0x7f });
                PS3.SetMemory(0x013954C1 + (ID * 0x2A38), new byte[] { 0xff, 0xff, 0x7f});
                PS3.SetMemory(0x0139544A + (ID * 0x2A38), new byte[] { 0xff, 0xff, 0x7f });
                Form1.InfAmmo[ID] = true;
                label12.Text = Form1.InfAmmo[ID].ToString();
                label12.ForeColor = Color.Green;
                f.doRefresh();
            }
            else if (Form1.InfAmmo[ID])
            {
                PS3.SetMemory(0x013954CA + (ID * 0x2A38), new byte[] { 0x00, 0x00, 0x00, 0x00 });
                PS3.SetMemory(0x01395452 + (ID * 0x2A38), new byte[] { 0x00, 0x00, 0x00, 0x00 });
                PS3.SetMemory(0x013954C1 + (ID * 0x2A38), new byte[] { 0x00, 0x00, 0x00});
                PS3.SetMemory(0x0139544A + (ID * 0x2A38), new byte[] { 0x00, 0x00, 0x00 });
                Form1.InfAmmo[ID] = false;
                label12.Text = Form1.InfAmmo[ID].ToString();
                label12.ForeColor = Color.Red;
                f.doRefresh();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = (string)comboBox1.SelectedItem;
            int camo = 0x00;
            if (selected == "Gold Camo")
            {
                camo = 0x3C;
            }
            else if (selected == "Ice Camo")
            {
                camo = 0x0A;
            }
            else if (selected == "Red Camo")
            {
                camo = 0x0D;
            }
            else if (selected == "Olive Camo")
            {
                camo = 0x10;
            }
            else if (selected == "Nevada Camo")
            {
                camo = 0x14;
            }
            else if (selected == "Sahara Camo")
            {
                camo = 0x18;
            }
            else if (selected == "Flora Camo")
            {
                camo = 0x38;
            }
            else if (selected == "Woodland Camo")
            {
                camo = 0x34;
            }
            else if (selected == "Yukon Camo")
            {
                camo = 0x30;
            }
            else if (selected == "Siberia Camo")
            {
                camo = 0x2c;
            }
            else if (selected == "Warsaw Camo")
            {
                camo = 0x28;
            }
            else if (selected == "Berlin Camo")
            {
                camo = 0x24;
            }
            else if (selected == "Tiger Camo")
            {
                camo = 0x20;
            }
            else if (selected == "ERDL Camo")
            {
                camo = 0x1c;
            }
            else if (selected == "Dusty Camo")
            {
                camo = 0x04;
            }
            else if (selected == "No Camo")
            {
                camo = 0x00;
            }
            PS3Util.PS3.SetMemory(0x013952E0 + (ID*0x2A38), new byte[] {(byte) camo});
        }
    }
}
