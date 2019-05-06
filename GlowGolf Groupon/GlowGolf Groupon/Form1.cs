using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GlowGolf_Groupon
{
    public partial class Form1 : Form
    {
        public String name, voucherNumber, grouponNumber, date = "";
        public String path = "mobile groupon.txt";
        public String[] lines;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Today.ToString("d");

            if (File.Exists(path) == false) //if the file doesnt exit
            {
                File.Create(path).Dispose();
            }

            StreamReader reader = new StreamReader(path);

            lines = File.ReadAllLines(path);//puts each line into the array
            reader.Dispose();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            name = txtName.Text;
            voucherNumber = txtVoucherNumber.Text;
            grouponNumber = txtGrouponNumber.Text;
            date = txtDate.Text;

            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    writer.WriteLine(lines[i]);
                }
                writer.WriteLine(name + "	" + voucherNumber + "	   " + grouponNumber + "	" + date);
                writer.Dispose();
            }
            MessageBox.Show("Groupon Submitted!");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtGrouponNumber.Clear();
            txtVoucherNumber.Clear();
            txtName.Focus();
        }
    }
}
