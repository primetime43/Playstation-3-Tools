using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Warfare_2_All_Clients_Tool
{
    public partial class TimePlayedCalculator : Form
    {
        public TimePlayedCalculator()
        {
            InitializeComponent();
        }

        public string X = "";

        private void TimePlayedCalculator_Load(object sender, EventArgs e)
        {
        
            
        }

        private void TimePlayedCalculator_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            X = xSonoroExtensions.CalculateTimePlayed((int)numericUpDown1.Value, (int)numericUpDown3.Value,
                                                   (int)numericUpDown2.Value);
            DialogResult = DialogResult.OK;
        }

    }
}
