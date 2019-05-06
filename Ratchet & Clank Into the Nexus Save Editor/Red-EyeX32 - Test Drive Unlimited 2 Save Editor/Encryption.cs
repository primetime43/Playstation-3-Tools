using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Red_EyeX32___Test_Drive_Unlimited_2_Save_Editor
{
    public class Encryption
    {
        public static void ps3Decrypt(string path, string region, string file)
        {
            cmdexe("-g " + region + " -d \"" + path + "\"  " + file);
        }

        public static void ps3Encrypt(string path, string region, string file)
        {
            cmdexe("-g " + region + " -e \"" + path + "\"  " + file);
        }

        public static void ps3Update(string path, string region, string file)
        {
            cmdexe("-g " + region + " -p -u " + path);
        }

        public static void ps3Patch(string path)
        {
            cmdpatch("build " + path + " PARAM.SFO");
        }

        public static void cmdexe(string cmd)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = "pfdtool.exe";
            startInfo.Arguments = cmd;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        public static void cmdpatch(string cmd)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = "sfopatcher.exe";
            startInfo.Arguments = cmd;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}