using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hosts_File_Editor
{
    public partial class Form1 : Form
    {

        public static String website = "";
        public Form1()
        {
            InitializeComponent();
        }

        Process process = new Process();
        private void btnBlockSite_Click(object sender, EventArgs e)
        {
            website = txtWebsite.Text;
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.

            String hostFileLocation = "C:/Windows/System32/drivers/etc/hosts";

            ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\Notepad.exe", hostFileLocation);
            string s = info.ToString();
            info.UseShellExecute = true;
            info.Verb = "runas";
            try
            {
                process = Process.Start(info);
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ERROR_CANCELLED)
                    MessageBox.Show("Why you no select Yes?");
                else
                    throw;
            }

            using (StreamWriter hostFile = new StreamWriter(hostFileLocation, true))
            {
                if (!website.StartsWith("www.") && !website.EndsWith(".com"))
                {
                    hostFile.WriteLine("\n127.0.0.1 www." + website + ".com");
                }
                else if (!website.EndsWith(".com"))
                {
                    hostFile.WriteLine("\n127.0.0.1 " + website + ".com");
                }
                else if (!website.StartsWith("www."))
                {
                    hostFile.WriteLine("\n127.0.0.1 www." + website);
                }
                else if (website.StartsWith("www.") && website.EndsWith(".com"))
                {
                    hostFile.WriteLine("\n127.0.0.1 " + website);
                }
                else
                {
                    MessageBox.Show("Well... something went terribly wrong!");
                }
                process.Kill();
                MessageBox.Show("Website successfully blocked!");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
