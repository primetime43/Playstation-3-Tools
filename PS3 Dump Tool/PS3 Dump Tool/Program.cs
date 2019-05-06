using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PS3Util;

namespace PS3_Dump_Tool
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a name for your dumped file name!");//File
            string name = Console.ReadLine();
            StreamWriter output = new StreamWriter(name + ".txt");

            //Connection
            Console.WriteLine("Do you want to connect to the ps3? (yes/no/connected)");
            string answer = Console.ReadLine();

            while (!answer.Equals("yes", StringComparison.InvariantCultureIgnoreCase) && (!answer.Equals("no", StringComparison.InvariantCultureIgnoreCase) && (!answer.Equals("connected", StringComparison.InvariantCultureIgnoreCase))))
            {
                Console.WriteLine("You must enter yes or no!");
                answer = Console.ReadLine();
            }

            if (answer.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
            {
                PS3.Connect();
            }
            else if (answer.Equals("no", StringComparison.InvariantCultureIgnoreCase))
            {
                PS3.Disconnect();
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            else if (answer.Equals("connected", StringComparison.InvariantCultureIgnoreCase))

            //Select a cod
            Console.WriteLine("\nPlease select one of the games");
            Console.WriteLine("1. Call of Duty 4\n2. World At War\n3. Modern Warfare 2\n4. Black Ops 1\n5. Modern Warfare 3\n6. Black Ops 2\n7. Ghosts\n8. Advanced Warfare\n");
            String selectedCod = Console.ReadLine();
            uint codNum = Convert.ToUInt32(selectedCod);

            if (codNum == 8)
            {

                for (uint i = 0x41e9df2c; i < 0x41eaa2b0; )
                {
                    string dump = PS3.GetString(i);

                    /* //Address
                     uint dvar = PS3.FollowPointer(i);
                     string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                     string s = Convert.ToString(hexOutput); //string
                     //Name
                     uint abc = PS3.FollowPointer(i);
                     string test = PS3.GetPString(abc);

                     output.WriteLine("\n" + "Address: " + "0x" + s + "\n");
                     output.WriteLine("String: " + test + "\n");*/
                    output.WriteLine(dump);
                }
            }
            else if (codNum == 7)
            {
                
            }
            else if (codNum == 6)
            {

            }
            else if (codNum == 5)
            {

            }
            else if (codNum == 4)
            {

            }
            else if (codNum == 3)
            {

            }
            else if (codNum == 2)
            {

            }
            else if (codNum == 1)
            {

            }

        }
    }
}
