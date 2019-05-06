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

namespace Clash_of_Clans_CSV_Tool
{
    public partial class Form1 : Form
    {
        public static string _path = "";
        public static string _folder = "";
        public static string _region = "";

        public Form1()
        {
            InitializeComponent();
        }
        private byte[] Remove(byte[] x)
        {
            List<byte> output = new List<byte>();
            for (int i = 0; i < 10; i++)
            {
                output.Add(x[i]);
            }
            for (int i = 0x0E; i < x.Length; i++)
            {
                output.Add(x[i]);
            }
            byte[] dowhateveryouwantwiththis = output.ToArray();

            return new byte[] {};
        }
        private OpenFileDialog FileDialog = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = FileDialog.FileName;
                string fileName = FileDialog.FileName;


                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] buffer = new byte[stream.Length - 0x0A];
                    stream.Seek(0x0A, SeekOrigin.Begin);
                    stream.Read(buffer, 0, buffer.Length);
                    stream.Seek(0x0A, SeekOrigin.Begin);
                    stream.Write(new byte[4], 0, 4);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Dispose();
                    stream.Close();
                    MessageBox.Show("CSV Decompressed!");
                    button1.Visible = false;
                    textBox1.Enabled = false;
                    button2.Visible = true;
                }
            }
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            Remove(new byte[] {0x00, 0x00, 0x00, 0x00});
            Application.Exit();
        }
    }
}
