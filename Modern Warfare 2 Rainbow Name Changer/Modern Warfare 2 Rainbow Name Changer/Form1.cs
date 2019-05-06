using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Warfare_2_Rainbow_Name_Changer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> colorList = new List<string>();
        private Dictionary<string, Color> lblColors = new Dictionary<string, Color>();

        private void Form1_Load(object sender, EventArgs e)
        {
            PS3Util.PS3.Connect();

            byte[] name = new byte[0x20];
            PS3Util.PS3.GetMemory(0x01f9f11c, ref name);
            rainTextBox.Text = Encoding.ASCII.GetString(name);
            label1.Text = Encoding.ASCII.GetString(name);

            rainLabel.Visible = false;
            label2.Visible = false;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    colorList.Add("^" + j.ToString());
                }
            }

            lblColors.Add("^1", Color.Red);
            lblColors.Add("^2", Color.GreenYellow);
            lblColors.Add("^3", Color.Yellow);
            lblColors.Add("^4", Color.Blue);
            lblColors.Add("^5", Color.Cyan);
            lblColors.Add("^6", Color.DeepPink);
        }

        private List<Label> Labels = new List<Label>();

        private void rainButton_Click(object sender, EventArgs e)
        {
            string text = rainTextBox.Text + '\0';

            rainLabel.Visible = true;

            int charcount = text.Length;
            string[] chars = new string[charcount];

            for (int i = 0; i < charcount; i++)
            {
                chars[i] = text.Substring(i, 1);
                Label label = new Label();
                label.AutoSize = true;
                label.Font = new Font("Consolas", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
                label.Location = new Point((i * 20) + 15, 75);
                label.Name = "lbl1" + i.ToString() + '\0';
                label.Size = new Size(23, 24);
                label.TabIndex = 34;
                label.Text = chars[i];
                Controls.Add(label);
                Labels.Add(label);
            }

            List<string> test = new List<string>();

            for (int i = 0; i < chars.Count(); i++)
            {
                test.Add(colorList[i]);
                test.Add(chars[i]);
            }

            StringBuilder sb = new StringBuilder();

            foreach (var VARIABLE in test)
            {
                sb.Append(VARIABLE);
            }

            rainLabel.Text = sb.ToString() + '\0';
            rainTimer.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            rainTimer.Stop();
            moveTimer.Stop();
            rainLabel.Visible = false;
        }

        private void rainTimer_Tick(object sender, EventArgs e)
        {
            string temp = rainLabel.Text + '\0';


            int count = 0;
            foreach (Match match in Regex.Matches(temp, @"\^([0-9])(.)"))
            {
                string digit = match.Value.Substring(1, 1);
                string lbltext = match.Value.Substring(2, 1);

                Labels[count].Text = lbltext;
                Labels[count].ForeColor = lblColors[match.Value.Substring(0, 2)];

                var i = int.Parse(digit);
                if (i < 7)
                {
                    i++;
                    digit = Convert.ToString(i);

                    if (digit == "7")
                        digit = "1";

                    temp = temp.Remove(match.Index + 1, 1);
                    temp = temp.Insert(match.Index + 1, digit);
                }
                count++;
            }

            rainLabel.Text = temp + '\0';

            byte[] name = Encoding.ASCII.GetBytes(rainLabel.Text + '\0');
            PS3Util.PS3.SetMemory(0x01f9f11c, name);
        }

        private int charcount;
        private string text;
        private bool state;

        private void MoveName()
        {
            label2.Text = rainTextBox.Text + '\0';
            text = label2.Text + '\0';
            charcount = text.Length;
            moveTimer.Start();
        }

        private void moveTimer_Tick(object sender, EventArgs e)
        {
            if (text.Length <= 16)
            {
                if (text.Length == charcount)
                {
                    state = true;
                }
                if (text.Length == 16)
                {
                    state = false;
                }

                if (state)
                {
                    text = text.Insert(0, " ");
                    label2.Text = text + '\0';

                    byte[] name = Encoding.ASCII.GetBytes(rainLabel.Text + '\0');
                    PS3Util.PS3.SetMemory(0x01f9f11c, name);
                }
                if (!state)
                {
                    text = text.Remove(0, 1);
                    label2.Text = text + '\0';

                    byte[] name = Encoding.ASCII.GetBytes(rainLabel.Text + '\0');
                    PS3Util.PS3.SetMemory(0x01f9f11c, name);
                }
            }
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            MoveName();
            label2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] original = Encoding.ASCII.GetBytes(label1.Text + '\0'); //PSN Name Reset
            PS3Util.PS3.SetMemory(0x01f9f11c, original);

            //

            byte[] name = new byte[0x20];
            PS3Util.PS3.GetMemory(0x01f9f11c, ref name);
            rainTextBox.Text = Encoding.ASCII.GetString(name);
        }
    }
}
