using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using PS3Util;
using System.Threading;
using PS3Lib;

namespace Call_of_Duty_Retriever
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static PS3API iMCXs = new PS3API();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PS3.Connect();
                //richTextBox1.Text = "Successfully Connect & Attached!\nPlease Select A Call Of Duty Game!";
            }
            catch
            {
                MessageBox.Show("Could not successfully connect! Retry again!");
            }
        }

        public class COD
        {
            public static string game;
        }

        //public uint start_address = 0x19611BC;
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("Dumped by primetime43\n");
            richTextBox1.ScrollToCaret();

            if (COD.game == "Black Ops 1 MP")
            {
                for (uint i = 0x19611BC; i < 0x01963e04; i += 0x4)
                {
                    //Address
                    uint dvar = PS3.FollowPointer(i);
                    string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint name = PS3.FollowPointer(dvar);
                    string nameOutput = PS3.ReadString(name);
                    //Type
                    uint u = PS3.FollowPointer(i) + 0x10;
                    uint gettype = (uint)PS3.GetInt32(u);
                    string s1 = String.Format("{0:X}", gettype);
                    //Next Dvar
                    uint next = PS3.FollowPointer(i) + 0x68;
                    uint getnext = (uint)PS3.GetInt32(next);

                    if (getnext > 0)
                    {
                        uint followeddvar = PS3.FollowPointer(next); //Follows pointer from next
                        string hexOutput2 = String.Format("{0:X}", getnext); //Gets address
                        string getnextstring = PS3.GetPString(followeddvar); //Gets name
                        //Gets Type
                        uint def = followeddvar + 0x10;
                        uint tested = (uint)PS3.GetInt32(def);
                        string s12 = String.Format("{0:X}", tested);
                        //
                        //
                        richTextBox1.AppendText("\n" + "Address: " + "0x" + hexOutput2 + "\n");
                        richTextBox1.AppendText("Name: " + getnextstring + "\n");
                        richTextBox1.AppendText("\n");
                        if (s12 == "0")
                        {
                            const string typestring = "BOOL";
                            uint a = followeddvar + 0x18;
                            var test = PS3.GetInt32(a);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            if (test == 0)
                            {
                                richTextBox1.AppendText("Value: " + "False" + "\n");
                            }
                            else if (test == 1)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                            else if (test == 16777216)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                        }
                        if (s12 == "8")
                        {
                            const string typestring = "COLOR";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s12 == "11")
                        {
                            const string typestring = "COLOR XYZ";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s12 == "6")
                        {
                            const string typestring = "ENUM";
                            /////////
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string enumvalue = String.Format("{0:X}", getvalue);

                            uint a = followeddvar + 0x5C;
                            uint Base_addr = PS3.FollowPointer(a);
                            uint Ptr = Base_addr;

                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + enumvalue + "\n");

                            if (Ptr != 0)
                            {
                                for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                                {
                                    uint temp_addr = PS3.FollowPointer(curPtr);

                                    if (temp_addr == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        string stringtext = PS3.GetString(temp_addr);
                                        richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                        richTextBox1.AppendText("\"");
                                        richTextBox1.AppendText("\n");
                                    }


                                }
                            }
                        }
                        if (s12 == "1")
                        {
                            const string typestring = "FLOAT";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "2")
                        {
                            const string typestring = "FLOAT 2";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "3")
                        {
                            const string typestring = "FLOAT 3";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "4")
                        {
                            const string typestring = "FLOAT 4";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "5")
                        {
                            const string typestring = "INT";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue +
                                                    " " + "(hex)" + "\n");
                        }
                        if (s12 == "9")
                        {
                            const string typestring = "INT 64";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue +
                                                    " " + "(hex)" + "\n");
                        }
                        if (s12 == "10")
                        {
                            const string typestring = "LINEAR COLOR RGB";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s12 == "7")
                        {
                            const string typestring = "STRING";
                            uint ss = followeddvar + 0x18;
                            uint getstring = PS3.FollowPointer(ss);
                            string stringtext = PS3.GetString(getstring);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("String: \"" + stringtext + "\n");
                            richTextBox1.AppendText("\"");
                            richTextBox1.AppendText("\n");
                        }
                        //////

                        richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                        //Displays whats in textbox
                        richTextBox1.AppendText("Name: " + nameOutput + "\n");
                        if (s1 == "0")
                        {
                            const string typestring = "BOOL";
                            uint a = PS3.FollowPointer(i) + 0x18;
                            var test = PS3.GetInt32(a);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            if (test == 0)
                            {
                                richTextBox1.AppendText("Value: " + "False" + "\n");
                            }
                            else if (test == 1)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                            else if (test == 16777216)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                        }
                        if (s1 == "8")
                        {
                            const string typestring = "COLOR";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s1 == "11")
                        {
                            const string typestring = "COLOR XYZ";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s1 == "6")
                        {
                            const string typestring = "ENUM";
                            /////////
                            uint poop = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(poop);
                            string enumvalue = String.Format("{0:X}", getvalue);

                            uint a = PS3.FollowPointer(i) + 0x5C;
                            uint Base_addr = PS3.FollowPointer(a);
                            uint Ptr = Base_addr;

                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + enumvalue + "\n");

                            if (Ptr != 0)
                            {
                                for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                                {
                                    uint temp_addr = PS3.FollowPointer(curPtr);

                                    if (temp_addr == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        string stringtext = PS3.GetString(temp_addr);
                                        richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                        richTextBox1.AppendText("\"");
                                        richTextBox1.AppendText("\n");
                                    }


                                }
                            }
                        }
                        if (s1 == "1")
                        {
                            const string typestring = "FLOAT";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "2")
                        {
                            const string typestring = "FLOAT 2";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "3")
                        {
                            const string typestring = "FLOAT 3";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "4")
                        {
                            const string typestring = "FLOAT 4";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "5")
                        {
                            const string typestring = "INT";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue + " " + "(hex)" + "\n");
                        }
                        if (s1 == "9")
                        {
                            const string typestring = "INT 64";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue + " " + "(hex)" + "\n");
                        }
                        if (s1 == "10")
                        {
                            const string typestring = "LINEAR COLOR RGB";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s1 == "7")
                        {
                            const string typestring = "STRING";
                            uint ss = PS3.FollowPointer(i) + 0x18;
                            uint getstring = PS3.FollowPointer(ss);
                            string stringtext = PS3.GetString(getstring);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("String: \"" + stringtext + "\n");
                            richTextBox1.AppendText("\"");
                            richTextBox1.AppendText("\n");
                        }
                        richTextBox1.ScrollToCaret();
                    }
                }
            }
            else if (COD.game == "Black Ops 1 ZM")
            {
                for (uint i = 0x1450864; i < 0x014532e8; i += 0x4)
                {
                    //Address
                    uint dvar = PS3.FollowPointer(i);
                    string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint name = PS3.FollowPointer(dvar);
                    string nameOutput = PS3.ReadString(name);
                    //Type
                    uint u = PS3.FollowPointer(i) + 0x10;
                    uint gettype = (uint)PS3.GetInt32(u);
                    string s1 = String.Format("{0:X}", gettype);
                    //Next Dvar
                    uint next = PS3.FollowPointer(i) + 0x68;
                    uint getnext = (uint)PS3.GetInt32(next);

                    if (getnext > 0)
                    {
                        uint followeddvar = PS3.FollowPointer(next); //Follows pointer from next
                        string hexOutput2 = String.Format("{0:X}", getnext); //Gets address
                        string getnextstring = PS3.GetPString(followeddvar); //Gets name
                        //Gets Type
                        uint def = followeddvar + 0x10;
                        uint tested = (uint)PS3.GetInt32(def);
                        string s12 = String.Format("{0:X}", tested);
                        //
                        //
                        richTextBox1.AppendText("\n" + "Address: " + "0x" + hexOutput2 + "\n");
                        richTextBox1.AppendText("Name: " + getnextstring + "\n");
                        richTextBox1.AppendText("\n");
                        if (s12 == "0")
                        {
                            const string typestring = "BOOL";
                            uint a = followeddvar + 0x18;
                            var test = PS3.GetInt32(a);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            if (test == 0)
                            {
                                richTextBox1.AppendText("Value: " + "False" + "\n");
                            }
                            else if (test == 1)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                            else if (test == 16777216)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                        }
                        if (s12 == "8")
                        {
                            const string typestring = "COLOR";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s12 == "11")
                        {
                            const string typestring = "COLOR XYZ";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s12 == "6")
                        {
                            const string typestring = "ENUM";
                            /////////
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string enumvalue = String.Format("{0:X}", getvalue);

                            uint a = followeddvar + 0x5C;
                            uint Base_addr = PS3.FollowPointer(a);
                            uint Ptr = Base_addr;

                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + enumvalue + "\n");

                            if (Ptr != 0)
                            {
                                for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                                {
                                    uint temp_addr = PS3.FollowPointer(curPtr);

                                    if (temp_addr == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        string stringtext = PS3.GetString(temp_addr);
                                        richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                        richTextBox1.AppendText("\"");
                                        richTextBox1.AppendText("\n");
                                    }


                                }
                            }
                        }
                        if (s12 == "1")
                        {
                            const string typestring = "FLOAT";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "2")
                        {
                            const string typestring = "FLOAT 2";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "3")
                        {
                            const string typestring = "FLOAT 3";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "4")
                        {
                            const string typestring = "FLOAT 4";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s12 == "5")
                        {
                            const string typestring = "INT";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue +
                                                    " " + "(hex)" + "\n");
                        }
                        if (s12 == "9")
                        {
                            const string typestring = "INT 64";
                            uint abc = followeddvar + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue +
                                                    " " + "(hex)" + "\n");
                        }
                        if (s12 == "10")
                        {
                            const string typestring = "LINEAR COLOR RGB";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s12 == "7")
                        {
                            const string typestring = "STRING";
                            uint ss = followeddvar + 0x18;
                            uint getstring = PS3.FollowPointer(ss);
                            string stringtext = PS3.GetString(getstring);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("String: \"" + stringtext + "\n");
                            richTextBox1.AppendText("\"");
                            richTextBox1.AppendText("\n");
                        }
                        //////

                        richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                        //Displays whats in textbox
                        richTextBox1.AppendText("Name: " + nameOutput + "\n");
                        if (s1 == "0")
                        {
                            const string typestring = "BOOL";
                            uint a = PS3.FollowPointer(i) + 0x18;
                            var test = PS3.GetInt32(a);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            if (test == 0)
                            {
                                richTextBox1.AppendText("Value: " + "False" + "\n");
                            }
                            else if (test == 1)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                            else if (test == 16777216)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                        }
                        if (s1 == "8")
                        {
                            const string typestring = "COLOR";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s1 == "11")
                        {
                            const string typestring = "COLOR XYZ";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s1 == "6")
                        {
                            const string typestring = "ENUM";
                            /////////
                            uint poop = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(poop);
                            string enumvalue = String.Format("{0:X}", getvalue);

                            uint a = PS3.FollowPointer(i) + 0x5C;
                            uint Base_addr = PS3.FollowPointer(a);
                            uint Ptr = Base_addr;

                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + enumvalue + "\n");

                            if (Ptr != 0)
                            {
                                for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                                {
                                    uint temp_addr = PS3.FollowPointer(curPtr);

                                    if (temp_addr == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        string stringtext = PS3.GetString(temp_addr);
                                        richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                        richTextBox1.AppendText("\"");
                                        richTextBox1.AppendText("\n");
                                    }


                                }
                            }
                        }
                        if (s1 == "1")
                        {
                            const string typestring = "FLOAT";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "2")
                        {
                            const string typestring = "FLOAT 2";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "3")
                        {
                            const string typestring = "FLOAT 3";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "4")
                        {
                            const string typestring = "FLOAT 4";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string floatvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s1 == "5")
                        {
                            const string typestring = "INT";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue + " " + "(hex)" + "\n");
                        }
                        if (s1 == "9")
                        {
                            const string typestring = "INT 64";
                            uint abc = PS3.FollowPointer(i) + 0x18;
                            uint getvalue = (uint)PS3.GetInt32(abc);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue + " " + "(hex)" + "\n");
                        }
                        if (s1 == "10")
                        {
                            const string typestring = "LINEAR COLOR RGB";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s1 == "7")
                        {
                            const string typestring = "STRING";
                            uint ss = PS3.FollowPointer(i) + 0x18;
                            uint getstring = PS3.FollowPointer(ss);
                            string stringtext = PS3.GetString(getstring);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("String: \"" + stringtext + "\n");
                            richTextBox1.AppendText("\"");
                            richTextBox1.AppendText("\n");
                        }
                        richTextBox1.ScrollToCaret();
                    }
                }
            }
            else if (COD.game == "Modern Warfare 2")
            {
                for (uint i = 0x1d0ae98; i < 0x01d27950; i += 0x48)
                {
                    //Address
                    string hexOutput = String.Format("{0:X}", i); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint dvar = PS3.FollowPointer(i);
                    string nameOutput = PS3.ReadString(dvar);
                    //Description
                    uint todesc = i + 0x4;
                    uint descpoint = PS3.FollowPointer(todesc);
                    string desc = PS3.GetString(descpoint);
                    //Type
                    uint u = i + 0xA;
                    var ok = (uint)PS3.GetInt8(u);
                    var idk = Convert.ToString(ok);
                    var s1 = String.Format("{0:X}", idk);
                    //Next Dvar
                    uint next = i + 0x44;
                    uint getnext = (uint)PS3.GetInt32(next);
                    if (getnext > 0)
                    {
                        //Address
                        string hexOutput1 = String.Format("{0:X}", getnext); //Converts from hex to string
                        string ss = Convert.ToString(hexOutput1); //string
                        //Name
                        uint dvar1 = PS3.FollowPointer(next);
                        uint gogo = PS3.FollowPointer(dvar1);
                        string nameOutput1 = PS3.ReadString(gogo);
                        //Description
                        uint todesc1 = dvar + 0x4;
                        uint descpoint1 = PS3.FollowPointer(todesc1);
                        string desc1 = PS3.GetString(descpoint1);
                        //Type
                        uint u1 = dvar + 0xA;
                        var ok1 = (uint)PS3.GetInt8(u1);
                        var idk1 = Convert.ToString(ok1);
                        var s2 = String.Format("{0:X}", idk1);

                        richTextBox1.AppendText("\n" + "Address: " + "0x" + ss + "\n");
                        richTextBox1.AppendText("Name: " + nameOutput1 + "\n");
                        richTextBox1.AppendText("Description: " + desc1 + "\n");
                        richTextBox1.AppendText("\n");
                        if (s2 == "0")
                        {
                            const string typestring = "BOOL";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            if (test == 0)
                            {
                                richTextBox1.AppendText("Value: " + "False" + "\n");
                            }
                            else if (test == 1)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                            else if (test == 16777216)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                        }
                        if (s2 == "1")
                        {
                            const string typestring = "FLOAT";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "2")
                        {
                            const string typestring = "FLOAT 2";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "3")
                        {
                            const string typestring = "FLOAT 3";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "4")
                        {
                            const string typestring = "FLOAT 4";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "5")
                        {
                            const string typestring = "INT";
                            uint a = dvar + 0xC; //Gets value
                            var getvalue = PS3.GetInt32(a);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue + " " + "(hex)" + "\n");
                        }
                        if (s2 == "6")
                        {
                            const string typestring = "ENUM";
                            uint a = dvar + 0x40;
                            uint Base_addr = PS3.FollowPointer(a);
                            uint Ptr = Base_addr;

                            richTextBox1.AppendText("Type: " + typestring + "\n");

                            if (Ptr != 0)
                            {
                                for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                                {
                                    uint temp_addr = PS3.FollowPointer(curPtr);

                                    if (temp_addr == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        string stringtext = PS3.GetString(temp_addr);
                                        richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                        richTextBox1.AppendText("\"");
                                        richTextBox1.AppendText("\n");
                                    }


                                }
                            }
                        }
                        if (s2 == "7")
                        {
                            const string typestring = "STRING";
                            uint a = dvar + 0xC; //Gets value
                            uint gg = PS3.FollowPointer(a);
                            string stringtext = PS3.GetString(gg);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("String: \"" + stringtext + "\n");
                            richTextBox1.AppendText("\"");
                            richTextBox1.AppendText("\n");
                        }
                        if (s2 == "8")
                        {
                            const string typestring = "COLOR";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s2 == "9")
                        {
                            const string typestring = "DEV TWEAK";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s2 == "10")
                        {
                            const string typestring = "COUNT";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                    }

                    richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                    richTextBox1.AppendText("Name: " + nameOutput + "\n");
                    richTextBox1.AppendText("Description: " + desc + "\n");
                    richTextBox1.AppendText("\n");

                    /*switch (s1)
                    {
                        case 0:
                            //your poop for 0;
                            break;
                        case 1:
                            //...
                            break;
                            //this will make your code a LOT faster and sometimes will avoid some crash !
                    }*/

                    if (s1 == "0")
                    {
                        const string typestring = "BOOL";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        if (test == 0)
                        {
                            richTextBox1.AppendText("Value: " + "False" + "\n");
                        }
                        else if (test == 1)
                        {
                            richTextBox1.AppendText("Value: " + "True" + "\n");
                        }
                        else if (test == 16777216)
                        {
                            richTextBox1.AppendText("Value: " + "True" + "\n");
                        }
                    }
                    if (s1 == "1")
                    {
                        const string typestring = "FLOAT";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "2")
                    {
                        const string typestring = "FLOAT 2";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "3")
                    {
                        const string typestring = "FLOAT 3";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "4")
                    {
                        const string typestring = "FLOAT 4";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "5")
                    {
                        const string typestring = "INT";
                        uint a = i + 0xC; //Gets value
                        var getvalue = PS3.GetInt32(a);
                        string intvalue = String.Format("{0:X}", getvalue);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                intvalue + " " + "(hex)" + "\n");
                    }
                    if (s1 == "6")
                    {
                        const string typestring = "ENUM";
                        uint a = i + 0x40;
                        uint Base_addr = PS3.FollowPointer(a);
                        uint Ptr = Base_addr;

                        richTextBox1.AppendText("Type: " + typestring + "\n");

                        if (Ptr != 0)
                        {
                            for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                            {
                                uint temp_addr = PS3.FollowPointer(curPtr);

                                if (temp_addr == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    string stringtext = PS3.GetString(temp_addr);
                                    richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                    richTextBox1.AppendText("\"");
                                    richTextBox1.AppendText("\n");
                                }


                            }
                        }
                    }
                    if (s1 == "7")
                    {
                        const string typestring = "STRING";
                        uint a = i + 0xC; //Gets value
                        uint gg = PS3.FollowPointer(a);
                        string stringtext = PS3.GetString(gg);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("String: \"" + stringtext + "\n");
                        richTextBox1.AppendText("\"");
                        richTextBox1.AppendText("\n");
                    }
                    if (s1 == "8")
                    {
                        const string typestring = "COLOR";
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                    }
                    if (s1 == "9")
                    {
                        const string typestring = "DEV TWEAK";
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                    }
                    if (s1 == "10")
                    {
                        const string typestring = "COUNT";
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                    }

                    richTextBox1.ScrollToCaret();
                }
            }
            else if (COD.game == "Modern Warfare 3")
            {
                if (!Directory.Exists(Path.GetTempPath() + "\\Call of Duty Retriever"))
                {
                    Directory.CreateDirectory(Path.GetTempPath() + "\\Call of Duty Retriever");
                }
                if (!File.Exists(Path.GetTempPath() + "\\Call of Duty Retriever\\mw3.txt"))
                {
                    File.WriteAllText(Path.GetTempPath() + "\\Call of Duty Retriever\\mw3.txt",
                                      Properties.Resources.mw3);
                }

                using (
                    StreamReader sr =
                        new StreamReader(Path.GetTempPath() + "\\Call of Duty Retriever\\mw3.txt"))
                {
                    String line = sr.ReadToEnd();
                    richTextBox1.AppendText("\n" + line);
                    richTextBox1.ScrollToCaret();
                }
            }
            else if (COD.game == "Black Ops 2")
            {
                for (uint i = 0x01ca3ea0; i < 0x01bdaad0; i += 0x4)
                {
                    //Address
                    uint dvar = PS3.FollowPointer(i);
                    string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint name = PS3.FollowPointer(dvar);
                    string nameOutput = PS3.ReadString(name);

                    richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                    //Displays whats in textbox
                    richTextBox1.AppendText("Name: " + nameOutput + "\n");
                    richTextBox1.ScrollToCaret();
                }
            }
            else if (COD.game == "Modern Warfare 2")
            {
                for (uint i = 0x1d0ae98; i < 0x01d27950; i += 0x48)
                {
                    //Address
                    string hexOutput = String.Format("{0:X}", i); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint dvar = PS3.FollowPointer(i);
                    string nameOutput = PS3.ReadString(dvar);
                    //Description
                    uint todesc = i + 0x4;
                    uint descpoint = PS3.FollowPointer(todesc);
                    string desc = PS3.GetString(descpoint);
                    //Type
                    uint u = i + 0xA;
                    var ok = (uint)PS3.GetInt8(u);
                    var idk = Convert.ToString(ok);
                    var s1 = String.Format("{0:X}", idk);
                    //Next Dvar
                    uint next = i + 0x44;
                    uint getnext = (uint)PS3.GetInt32(next);
                    if (getnext > 0)
                    {
                        //Address
                        string hexOutput1 = String.Format("{0:X}", getnext); //Converts from hex to string
                        string ss = Convert.ToString(hexOutput1); //string
                        //Name
                        uint dvar1 = PS3.FollowPointer(next);
                        uint gogo = PS3.FollowPointer(dvar1);
                        string nameOutput1 = PS3.ReadString(gogo);
                        //Description
                        uint todesc1 = dvar + 0x4;
                        uint descpoint1 = PS3.FollowPointer(todesc1);
                        string desc1 = PS3.GetString(descpoint1);
                        //Type
                        uint u1 = dvar + 0xA;
                        var ok1 = (uint)PS3.GetInt8(u1);
                        var idk1 = Convert.ToString(ok1);
                        var s2 = String.Format("{0:X}", idk1);

                        richTextBox1.AppendText("\n" + "Address: " + "0x" + ss + "\n");
                        richTextBox1.AppendText("Name: " + nameOutput1 + "\n");
                        richTextBox1.AppendText("Description: " + desc1 + "\n");
                        richTextBox1.AppendText("\n");
                        if (s2 == "0")
                        {
                            const string typestring = "BOOL";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            if (test == 0)
                            {
                                richTextBox1.AppendText("Value: " + "False" + "\n");
                            }
                            else if (test == 1)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                            else if (test == 16777216)
                            {
                                richTextBox1.AppendText("Value: " + "True" + "\n");
                            }
                        }
                        if (s2 == "1")
                        {
                            const string typestring = "FLOAT";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "2")
                        {
                            const string typestring = "FLOAT 2";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "3")
                        {
                            const string typestring = "FLOAT 3";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "4")
                        {
                            const string typestring = "FLOAT 4";
                            uint a = dvar + 0xC; //Gets value
                            var test = PS3.GetInt32(a);
                            string floatvalue = String.Format("{0:X}", test);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + floatvalue + "\n");
                        }
                        if (s2 == "5")
                        {
                            const string typestring = "INT";
                            uint a = dvar + 0xC; //Gets value
                            var getvalue = PS3.GetInt32(a);
                            string intvalue = String.Format("{0:X}", getvalue);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                    intvalue + " " + "(hex)" + "\n");
                        }
                        if (s2 == "6")
                        {
                            const string typestring = "ENUM";
                            uint a = dvar + 0x40;
                            uint Base_addr = PS3.FollowPointer(a);
                            uint Ptr = Base_addr;

                            richTextBox1.AppendText("Type: " + typestring + "\n");

                            if (Ptr != 0)
                            {
                                for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                                {
                                    uint temp_addr = PS3.FollowPointer(curPtr);

                                    if (temp_addr == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        string stringtext = PS3.GetString(temp_addr);
                                        richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                        richTextBox1.AppendText("\"");
                                        richTextBox1.AppendText("\n");
                                    }


                                }
                            }
                        }
                        if (s2 == "7")
                        {
                            const string typestring = "STRING";
                            uint a = dvar + 0xC; //Gets value
                            uint gg = PS3.FollowPointer(a);
                            string stringtext = PS3.GetString(gg);
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                            richTextBox1.AppendText("String: \"" + stringtext + "\n");
                            richTextBox1.AppendText("\"");
                            richTextBox1.AppendText("\n");
                        }
                        if (s2 == "8")
                        {
                            const string typestring = "COLOR";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s2 == "9")
                        {
                            const string typestring = "DEV TWEAK";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                        if (s2 == "10")
                        {
                            const string typestring = "COUNT";
                            richTextBox1.AppendText("Type: " + typestring + "\n");
                        }
                    }

                    richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                    richTextBox1.AppendText("Name: " + nameOutput + "\n");
                    richTextBox1.AppendText("Description: " + desc + "\n");
                    richTextBox1.AppendText("\n");

                    /*switch (s1)
                    {
                        case 0:
                            //your poop for 0;
                            break;
                        case 1:
                            //...
                            break;
                            //this will make your code a LOT faster and sometimes will avoid some crash !
                    }*/

                    if (s1 == "0")
                    {
                        const string typestring = "BOOL";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        if (test == 0)
                        {
                            richTextBox1.AppendText("Value: " + "False" + "\n");
                        }
                        else if (test == 1)
                        {
                            richTextBox1.AppendText("Value: " + "True" + "\n");
                        }
                        else if (test == 16777216)
                        {
                            richTextBox1.AppendText("Value: " + "True" + "\n");
                        }
                    }
                    if (s1 == "1")
                    {
                        const string typestring = "FLOAT";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "2")
                    {
                        const string typestring = "FLOAT 2";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "3")
                    {
                        const string typestring = "FLOAT 3";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "4")
                    {
                        const string typestring = "FLOAT 4";
                        uint a = i + 0xC; //Gets value
                        var test = PS3.GetInt32(a);
                        string floatvalue = String.Format("{0:X}", test);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + floatvalue + "\n");
                    }
                    if (s1 == "5")
                    {
                        const string typestring = "INT";
                        uint a = i + 0xC; //Gets value
                        var getvalue = PS3.GetInt32(a);
                        string intvalue = String.Format("{0:X}", getvalue);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("Value: " + getvalue.ToString("D") + " " + "(dec)" + " " +
                                                intvalue + " " + "(hex)" + "\n");
                    }
                    if (s1 == "6")
                    {
                        const string typestring = "ENUM";
                        uint a = i + 0x40;
                        uint Base_addr = PS3.FollowPointer(a);
                        uint Ptr = Base_addr;

                        richTextBox1.AppendText("Type: " + typestring + "\n");

                        if (Ptr != 0)
                        {
                            for (uint curPtr = Ptr; curPtr < 0xFFFFFFFF; curPtr += 0x4)
                            {
                                uint temp_addr = PS3.FollowPointer(curPtr);

                                if (temp_addr == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    string stringtext = PS3.GetString(temp_addr);
                                    richTextBox1.AppendText("Enum Values: \"" + stringtext + "\n");
                                    richTextBox1.AppendText("\"");
                                    richTextBox1.AppendText("\n");
                                }


                            }
                        }
                    }
                    if (s1 == "7")
                    {
                        const string typestring = "STRING";
                        uint a = i + 0xC; //Gets value
                        uint gg = PS3.FollowPointer(a);
                        string stringtext = PS3.GetString(gg);
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                        richTextBox1.AppendText("String: \"" + stringtext + "\n");
                        richTextBox1.AppendText("\"");
                        richTextBox1.AppendText("\n");
                    }
                    if (s1 == "8")
                    {
                        const string typestring = "COLOR";
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                    }
                    if (s1 == "9")
                    {
                        const string typestring = "DEV TWEAK";
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                    }
                    if (s1 == "10")
                    {
                        const string typestring = "COUNT";
                        richTextBox1.AppendText("Type: " + typestring + "\n");
                    }

                    richTextBox1.ScrollToCaret();
                }
            }
            else if (COD.game == "Ghosts")
            {
                for (uint i = 0x0266c9c0; i < 0x0266c9f4; i += 0x4)
                {
                    //Address
                    uint dvar = PS3.FollowPointer(i);
                    string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint name = PS3.FollowPointer(dvar);
                    string nameOutput = PS3.ReadString(name);

                    richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                    //Displays whats in textbox
                    richTextBox1.AppendText("Name: " + nameOutput + "\n");
                    richTextBox1.ScrollToCaret();
                }
            }
            else if (COD.game == "World At War MP")
            {
                for (uint i = 0x279DCFC; i < 0x0279f8cc; i += 0x4)
                {
                    //Address
                    uint dvar = PS3.FollowPointer(i);
                    string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint name = PS3.FollowPointer(dvar);
                    string nameOutput = PS3.ReadString(name);
                    //Description
                    uint des = PS3.FollowPointer(dvar + 0x4);
                    string desOutput = PS3.ReadString(des);
                    /*//Type
                    uint u = PS3.FollowPointer(dvar + 0x10);
                    uint gettype = (uint) PS3.GetInt32(u);
                    string s1 = String.Format("{0:X}", gettype);*/


                    richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                    richTextBox1.AppendText("Name: " + nameOutput + "\n");
                    richTextBox1.AppendText("Description: " + desOutput + "\n");



                    //Next Dvar
                    uint next = PS3.FollowPointer(i) + 0x58;
                    uint getnext = (uint)PS3.GetInt32(next);

                    if (getnext > 0)
                    {
                        //Address
                        uint dvar1 = PS3.FollowPointer(i);
                        string hexOutput1 = String.Format("{0:X}", dvar1); //Converts from hex to string
                        string ss = Convert.ToString(hexOutput1); //string
                        //Name
                        uint name1 = PS3.FollowPointer(dvar1);
                        string nameOutput1 = PS3.ReadString(name1);
                        //Description
                        uint des1 = PS3.FollowPointer(dvar1 + 0x4);
                        string desOutput1 = PS3.ReadString(des1);

                        richTextBox1.AppendText("\n" + "Address: " + "0x" + ss + "\n");
                        richTextBox1.AppendText("Name: " + nameOutput1 + "\n");
                        richTextBox1.AppendText("Description: " + desOutput1 + "\n");
                    }

                    richTextBox1.ScrollToCaret();
                }
            }
            else if (COD.game == "Dump")
            {
                /*   using (StreamWriter file = new StreamWriter(@"C:\Users\Mike\Desktop\Dump.txt", true))
                   {
                       file.Write(getstring);
                   }*/
                /*for (uint i = 0x30243d5c; i < 0x30244518; i += 0x4)
                {
                    uint stringdmp = PS3.FollowPointer(i); //Dumps strings
                    uint followagain = PS3.FollowPointer(stringdmp);
                    string stringfinal = PS3.GetString(followagain);


                    richTextBox1.AppendText("\n" + "- " + stringfinal + "\n");
                    richTextBox1.ScrollToCaret();
                }*/
                string test = "";
                for (uint i = 0x339c14ba; i < 0x339e44fb; i += (uint)test.Length) //0x36E23890
                {
                    test = PS3.GetString(i);
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Mike\Desktop\Dump.txt", true))
                    {
                        file.Write(test);
                    }
                }
            }





            else if (COD.game == "Advanced Warfare")
            {
                //StreamWriter output = new StreamWriter("dump.txt");

                for (uint i = 0x41ce3004; i < 0x41cf980c; i += 0x8)
                {
                    //Address
                    uint dvar = PS3.FollowPointer(i);
                    string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                    string s = Convert.ToString(hexOutput); //string
                    //Name
                    uint abc = PS3.FollowPointer(i);
                    string test = PS3.GetPString(abc);
                    //Description
                    /*uint des = PS3.FollowPointer(dvar + 0x4);
                    string desOutput = PS3.ReadString(des);*/
                    /*//Type
                    uint u = PS3.FollowPointer(dvar + 0x10);
                    uint gettype = (uint) PS3.GetInt32(u);
                    string s1 = String.Format("{0:X}", gettype);*/

                    /*output.WriteLine("\n" + "Address: " + "0x" + s + "\n");
                    output.WriteLine("Name: " + test + "\n");*/
                    richTextBox1.AppendText("\n" + "Address: " + "0x" + s + "\n");
                    richTextBox1.AppendText("String: " + test + "\n");
                    //richTextBox1.AppendText("Description: " + desOutput + "\n");
                }
            }
        }

        public static uint FollowPointer(uint addr)
        {
            return (uint)iMCXs.Extension.ReadInt32(addr);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "Modern Warfare 2")
            {
                COD.game = "Modern Warfare 2";
            }
            if ((string)comboBox1.SelectedItem == "Modern Warfare 3")
            {
                COD.game = "Modern Warfare 3";
            }
            if ((string)comboBox1.SelectedItem == "Black Ops 1 MP")
            {
                COD.game = "Black Ops 1 MP";
            }
            if ((string)comboBox1.SelectedItem == "Black Ops 1 ZM")
            {
                COD.game = "Black Ops 1 ZM";
            }
            if ((string)comboBox1.SelectedItem == "Black Ops 2")
            {
                COD.game = "Black Ops 2";
            }
            if ((string)comboBox1.SelectedItem == "Ghosts")
            {
                COD.game = "Ghosts";
            }
            if ((string)comboBox1.SelectedItem == "Dump")
            {
                COD.game = "Dump";
            }
            if ((string)comboBox1.SelectedItem == "World At War MP")
            {
                COD.game = "World At War MP";
            }
            if ((string)comboBox1.SelectedItem == "Advanced Warfare")
            {
                COD.game = "Advanced Warfare";
            }
        }

        public void SaveMyFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "*.txt";
            saveFileDialog.Filter = @"Text File|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveMyFile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            iMCXs.CCAPI.ConnectTarget();
            iMCXs.CCAPI.AttachProcess();
            MessageBox.Show("Connected");
        }
    }
}
