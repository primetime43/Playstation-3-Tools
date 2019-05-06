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
    public partial class name : Form
    {
        public name()
        {
            InitializeComponent();
            //
            byte[] name = new byte[0x20];
            PS3Util.PS3.GetMemory(0x02952934, ref name);
            textBox1.Text = Encoding.ASCII.GetString(name);
            //
            byte[] clan = new byte[0x05];
            PS3Util.PS3.GetMemory(0x00, ref clan);
            textBox2.Text = Encoding.ASCII.GetString(clan);
            //
            byte[] original = new byte[0x20];
            PS3Util.PS3.GetMemory(0x02952934, ref original);
            textBox3.Text = Encoding.ASCII.GetString(original);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            byte[] name = Encoding.ASCII.GetBytes(textBox1.Text + '\0'); //PSN Name
            PS3Util.PS3.SetMemory(0x02952934, name);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            byte[] clan = Encoding.ASCII.GetBytes(textBox2.Text + '\0'); //Clantag
            PS3Util.PS3.SetMemory(0x00, clan);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] original = Encoding.ASCII.GetBytes(textBox3.Text + '\0'); //PSN Name Reset
            PS3Util.PS3.SetMemory(0x02952934, original);
        }
    }
}
