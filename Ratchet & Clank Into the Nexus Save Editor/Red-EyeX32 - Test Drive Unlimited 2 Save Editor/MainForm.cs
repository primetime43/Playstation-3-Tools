using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Red_EyeX32___Test_Drive_Unlimited_2_Save_Editor
{
    public partial class MainForm : Form
    {
        public PlayerData playerData;
        public static string _path = "";
        public static string _folder = "";
        public static string _region = "";
        public string file = "GAME.SAV";

        public MainForm()
        {
            InitializeComponent();
            string currentPath = (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

            byte[] pfdtool = Properties.Resources.pfdtool;
            File.WriteAllBytes(currentPath + "\\pfdtool.exe", pfdtool);

            byte[] games = Properties.Resources.games;
            File.WriteAllBytes(currentPath + "\\games.conf", games);

            byte[] global = Properties.Resources.global;
            File.WriteAllBytes(currentPath + "\\global.conf", global);

            byte[] sfopatcher = Properties.Resources.sfopatcher;
            File.WriteAllBytes(currentPath + "\\sfopatcher.exe", sfopatcher);
        }

        public struct PlayerData
        {
            public int Bolts;
            public int Raritanium;
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderDialog = new FolderBrowserDialog();
            FolderDialog.Tag = "Open A Ratchet & Clank Game Save Folder";
            FolderDialog.Description = "Please Locate Your Ratchet & Clank Game Save Path";
            if (FolderDialog.ShowDialog() == DialogResult.OK)
            {
                _folder = FolderDialog.SelectedPath;
                if (!File.Exists(_folder + "\\PARAM.SFO"))
                {
                    MessageBox.Show("PARAM.SFO Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Stream rs = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                rs.Position = 0x140;
                AccountIDTextBox.Text = new string(rs.ReadChars(0x10));
                rs.Position = 0x968;
                _region = new string(rs.ReadChars(9));
                GameVersionTextBox.Text = _region;
                rs.Close();
                rs.Dispose();

                SaveGameImagePictureBox.Image = Image.FromFile(FolderDialog.SelectedPath + "\\ICON0.PNG");

                //Loads GAME.SAV files
                if ( _region == "NPUA80908" || _region == "NPEA00457" || _region == "BCUS99245" || _region == "BCES01908" || _region == "BCES01949" || _region == "BCJS30092" || _region == "NPJA00101" || _region == "BCUS98187" || _region == "BLES00301" || _region == "NPUA80145" || _region == "NPEA00088" || _region == "BCUS98127" || _region == "BCES00052" || _region == "BCKS10016" || _region == "BCJS30014" || _region == "BCJS70012" || _region == "BCKS10054" || _region == "BCAS20045" || _region == "BCJS70004" || _region == "NPUA98153" || _region == "NPEA90017" || _region == "NPJA90035" || _region == "NPHA20002")
                {
                    GameSaveKeyTextBox.Text = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
                    _path = _folder + "\\GAME.SAV";
                    Encryption.ps3Decrypt(_folder, _region, file);
                    Stream test = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                    test.Position = 0x968;
                    string gameRegion = Encoding.ASCII.GetString((byte[])test.ReadBytes(9)); 
                    Stream stream = new FileStream(_folder + "\\GAME.SAV", FileMode.Open, FileAccess.ReadWrite);

                    if (File.Exists(_folder + "\\PARAM.SFO") && gameRegion == "NPUA80908" || gameRegion == "NPEA00457" || gameRegion == "BCUS99245" || gameRegion == "BCES01908" || gameRegion == "BCES01949" || gameRegion == "BCJS30092" || gameRegion == "NPJA00101")//Ratchet & Clank™: Nexus
                    {
                        stream.Position = 0x105C;//Loads the number of Bolts
                        playerData.Bolts = stream.ReadInt32();

                        stream.Position = 0x1060;//Loads the number of raritanium
                        playerData.Raritanium = stream.ReadInt32();
                        Stream planet = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                        planet.Position = 0x9b0; // Set Nexus position for planet name
                        string planetload = Encoding.ASCII.GetString((byte[])planet.ReadBytes(20));
                        textBox1.Text = planetload;
                        MoneyNumericUpDown.Value = playerData.Bolts;
                        CasinoChipsNumericUpDown.Value = playerData.Raritanium;
                    }
                    else if (File.Exists(_folder + "\\PARAM.SFO") && gameRegion == "BCUS98187" || gameRegion == "BLES00301" || gameRegion == "NPUA80145" || gameRegion == "NPEA00088")//Ratchet & Clank® Future: Quest for Booty™
                    {
                        stream.Position = 0x274;//Loads the number of Bolts
                        playerData.Bolts = stream.ReadInt32();

                        Stream planet = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                        planet.Position = 0x158;
                        string planetload = Encoding.ASCII.GetString((byte[])planet.ReadBytes(20));
                        textBox1.Text = planetload;

                        CasinoChipsLabel.Hide();
                        CasinoChipsNumericUpDown.Hide();
                        MoneyNumericUpDown.Value = playerData.Bolts;
                    }
                    else if (File.Exists(_folder + "\\PARAM.SFO") && gameRegion == "BCUS98127" || gameRegion == "BCES00052" || gameRegion == "BCKS10016" || gameRegion == "BCJS30014" || gameRegion == "BCJS70012" || gameRegion == "BCKS10054" || gameRegion == "BCAS20045" || gameRegion == "BCJS70004" || gameRegion == "NPUA98153" || gameRegion == "NPEA90017" || gameRegion == "NPJA90035" || gameRegion == "NPHA20002")//Ratchet & Clank Tools of Destruction
                    {
                        stream.Position = 0x41c;//Loads the number of Bolts
                        playerData.Bolts = stream.ReadInt32();

                        stream.Position = 0x420;//Loads the number of raritanium
                        playerData.Raritanium = stream.ReadInt32();
                        Stream planet = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                        planet.Position = 0x9b0;
                        string planetload = Encoding.ASCII.GetString((byte[])planet.ReadBytes(20));
                        textBox1.Text = planetload;

                        MoneyNumericUpDown.Value = playerData.Bolts;
                        CasinoChipsNumericUpDown.Value = playerData.Raritanium;
                    }
                    stream.Close();
                    stream.Dispose();
                }

                //Loads USR-DATA files
                    if (File.Exists((_folder + "\\USR-DATA")))
                    {
                        GameSaveKeyTextBox.Text = "01020304050607FACB0A0B0C0D0E0F10";
                        _path = _folder + "\\USR-DATA";
                        Encryption.ps3Decrypt(_folder, _region, file);
                        Stream test = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                        test.Position = 0x968;
                        string gameRegion = Encoding.ASCII.GetString((byte[])test.ReadBytes(9)); 
                        Stream USR = new FileStream(_folder + "\\USR-DATA", FileMode.Open, FileAccess.ReadWrite);

                        if (File.Exists(_folder + "\\PARAM.SFO") && gameRegion == "NPUA80643")
                        {
                            USR.Position = 0x24;//Loads the number of Bolts
                            playerData.Bolts = USR.ReadInt32();

                            Stream planet = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                            planet.Position = 0x168;
                            string planetload = Encoding.ASCII.GetString((byte[])planet.ReadBytes(20));
                            textBox1.Text = planetload;

                            CasinoChipsLabel.Hide();
                            CasinoChipsNumericUpDown.Hide();
                            MoneyNumericUpDown.Value = playerData.Bolts;
                        }
                        USR.Close();
                        USR.Dispose();
                    }

                    TabControl.Enabled = true;
                    MessageBox.Show("All Files Have Been Decrypted And Loaded Succesfully", "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_region == "NPUA80908" || _region == "NPEA00457" || _region == "BCUS99245" || _region == "BCES01908" ||
                _region == "BCES01949" || _region == "BCJS30092" || _region == "NPJA00101" || _region == "BCUS98187" ||
                _region == "BLES00301" || _region == "NPUA80145" || _region == "NPEA00088" || _region == "BCUS98127" ||
                _region == "BCES00052" || _region == "BCKS10016" || _region == "BCJS30014" || _region == "BCJS70012" ||
                _region == "BCKS10054" || _region == "BCAS20045" || _region == "BCJS70004" || _region == "NPUA98153" ||
                _region == "NPEA90017" || _region == "NPJA90035" || _region == "NPHA20002")
            {
                Stream test = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                test.Position = 0x968;
                string gameRegion = Encoding.ASCII.GetString((byte[]) test.ReadBytes(9));

                Stream stream = new FileStream(_path, FileMode.Open, FileAccess.ReadWrite);

                if (File.Exists(_folder + "\\PARAM.SFO") && gameRegion == "NPUA80908" || gameRegion == "NPEA00457" ||
                    gameRegion == "BCUS99245" || gameRegion == "BCES01908" || gameRegion == "BCES01949" ||
                    gameRegion == "BCJS30092" || gameRegion == "NPJA00101") //Ratchet & Clank™: Nexus
                {
                    stream.Position = 0x105C; //Bolts
                    stream.WriteInt32(Convert.ToInt32(MoneyNumericUpDown.Value));

                    stream.Position = 0x1060; //Raritanium
                    stream.WriteInt32(Convert.ToInt32(CasinoChipsNumericUpDown.Value));

                    stream.Close();
                    stream.Dispose();
                }
                else if (File.Exists(_folder + "\\PARAM.SFO") && gameRegion == "BCUS98187" || gameRegion == "BLES00301" ||
                         gameRegion == "NPUA80145" || gameRegion == "NPEA00088")
                    //Ratchet & Clank® Future: Quest for Booty™
                {
                    stream.Position = 0x274; //Bolts
                    stream.WriteInt32(Convert.ToInt32(MoneyNumericUpDown.Value));

                    stream.Close();
                    stream.Dispose();
                }
                else if (File.Exists(_folder + "\\PARAM.SFO") && gameRegion == "BCUS98127" ||
                         gameRegion == "BCES00052" || gameRegion == "BCKS10016" || gameRegion == "BCJS30014" ||
                         gameRegion == "BCJS70012" || gameRegion == "BCKS10054" || gameRegion == "BCAS20045" ||
                         gameRegion == "BCJS70004" || gameRegion == "NPUA98153" || gameRegion == "NPEA90017" ||
                         gameRegion == "NPJA90035" || gameRegion == "NPHA20002")
                    //Ratchet & Clank Tools of Destruction
                {
                    stream.Position = 0x41c; //Bolts
                    stream.WriteInt32(Convert.ToInt32(MoneyNumericUpDown.Value));

                    stream.Position = 0x420; //Raritanium
                    stream.WriteInt32(Convert.ToInt32(CasinoChipsNumericUpDown.Value));
                    stream.Close();
                    stream.Dispose();
                }
            }
            if (File.Exists((_folder + "\\USR-DATA")))
            {
                Stream test = new FileStream(_folder + "\\PARAM.SFO", FileMode.Open, FileAccess.Read);
                test.Position = 0x968;
                string gameRegion = Encoding.ASCII.GetString((byte[]) test.ReadBytes(9));

                Stream USR = new FileStream(_path, FileMode.Open, FileAccess.ReadWrite);
                {
                    USR.Position = 0x24;//Bolts
                    USR.WriteInt32(Convert.ToInt32(MoneyNumericUpDown.Value));
                }
            }

            Encryption.ps3Encrypt(_folder, _region, file);
                MessageBox.Show("All Files Saved Successfully!", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
                base.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string currentPath = (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            File.Delete(currentPath + "\\pfdtool.exe");
            File.Delete(currentPath + "\\games.conf");
            File.Delete(currentPath + "\\global.conf");
            File.Delete(currentPath + "\\sfopatcher.exe");
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDialog = new SaveFileDialog();
            SaveDialog.FileName = "ICON0";
            SaveDialog.Filter = "PNG Image (*.PNG)|*.PNG";
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(_folder + "\\ICON0.PNG", SaveDialog.FileName);
            }
        }

        private void ViewImageButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(_folder + "\\ICON0.PNG");
        }

        private void VerifyVersionButton_Click(object sender, EventArgs e)
        {
            GameVersionTextBox.Text = _region;
            MessageBox.Show("Game Version Verified Succesfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void UpdateAccountIDButton_Click(object sender, EventArgs e)
        {
            Encryption.ps3Update(_folder, _region, _folder);
            MessageBox.Show("Account ID Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void PatchPARAMButton_Click(object sender, EventArgs e)
        {
            Encryption.ps3Patch(_folder);
            MessageBox.Show("PARAM.SFO Has Been Patched Successfully", "Patched", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void BackUpDATAButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("Game Back Ups"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Game Back Ups");
            }
            if (File.Exists(_folder + "\\GAME.SAV"))
            {
                File.Copy(_folder + "\\GAME.SAV", Application.StartupPath + "\\Game Back Ups\\GAME.SAV");
            }
            if (File.Exists((_folder + "\\USR-DATA")))
            {
                File.Copy(_folder + "\\USR-DATA", Application.StartupPath + "\\Game Back Ups\\USR-DATA");
            }
            MessageBox.Show("DATA Game File Have Been Backed Up To " + Application.StartupPath + "\\Game Back Ups", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void BackUpSFOButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Application.StartupPath + "\\Game Back Ups"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Game Back Ups");
            }
            File.Copy(_folder + "\\PARAM.SFO", Application.StartupPath + "\\Game Back Ups\\PARAM.SFO");
            MessageBox.Show("PARAM.SFO Has Been Backed Up To " + Application.StartupPath + "\\Game Back Ups", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Ratchet & Clank save editor created by primetime43, with the help of Red_EyeX32!\nThanks to him for allowing me to use his source to convert a different game to use for this game!");
        }
    }
}