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

namespace Black_Ops_1_Unlock_All
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void connectPS3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS3.Connect();
            PS3.SUCCEEDED(0);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS3.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PS3.SetMemory(0x0208be10, All.Unlock1);
            PS3.SetMemory(0x02094990, All.Unlock2);
            MessageBox.Show("Unlock All Completed!\nEnjoy!");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Unlock All Tool By primetime43\nThanks To aerosoul94 For His Connection Class\nJoin NextGenUpdate.com");
        }

    }
}
