using System;
using System.Threading;
using PS3Util;

namespace Black_Ops_1_Zombie_Console
{
    class DiscoverDvarInfo
    {
        public static void Dvars(String dvarName)
        {
            String nameOutput = "";
            String dvarAddress = "";
            String dvarType = "";
            String dvarValue = "";
            uint i = 0x1450864;//dvar pool start address

            Console.Write("Searching for dvar.........");

            while (dvarName != nameOutput && i != 0x014532e8 && dvarName != "cancel")
            {
                //Address
                uint dvar = PS3A.FollowPointer(i);
                string hexOutput = String.Format("{0:X}", dvar); //Converts from hex to string
                dvarAddress = Convert.ToString(hexOutput); //string
                //Name
                uint name = PS3A.FollowPointer(dvar);
                nameOutput = PS3A.ReadString(name);
                //Type
                uint u = PS3A.FollowPointer(i) + 0x10;
                uint dvarTypeValue = (uint)PS3A.GetInt32(u);
                dvarType = DvarType(dvarTypeValue);
                //Value
                uint value = PS3A.FollowPointer(i) + 0x18;//at base of dvar, adds 18 to go to value
                uint getvalue = (uint)PS3A.GetInt32(value);
                dvarValue = DvarValue(dvarType, getvalue);//passes in the dvarType and its value Ex: bool, 0

                i += 0x4;
            }

            if (i == 0x014532e8 || dvarName == "cancel")
            {
                Console.WriteLine("\nUnable to find the dvar!");
                Console.WriteLine("Returning to main menu.......");
                Thread.Sleep(2000);
                Console.Clear();
                Menu.MenuBaseHeader();
                Menu.MainMenuTop();
            }
            else
            {
                Console.WriteLine("\n\nName: " + nameOutput + "\nAddress: 0x" + dvarAddress + "\nType: " + dvarType + "\nValue: " + dvarValue + "\n\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        public static String DvarType(uint dvarTypeValue)
        {
            switch (dvarTypeValue)
            {
                case 0:
                    return "BOOL";
                case 1:
                    return "FLOAT";
                case 2:
                    return "FLOAT 2";
                case 3:
                    return "FLOAT 3";
                case 4:
                    return "FLOAT 4";
                case 5:
                    return "INT";
                case 6:
                    return "ENUM";
                case 7:
                    return "STRING";
                case 8:
                    return "COLOR";
                case 9:
                    return "INT 64";
                case 10:
                    return "LINEAR COLOR RGB";
                case 11:
                    return "COLOR XYZ";
                default:
                    return "Unable to determine type";
            }
        }

        public static String DvarValue(String type, uint dvarValue)
        {
            String returnValue = "";
            switch (type)
            {
                case "BOOL":
                    returnValue = (dvarValue == 0 ? "False" : "True");
                    return returnValue;

                case "FLOAT": case "FLOAT 2": case "FLOAT 3": case "FLOAT 4": case "INT": case "INT 64":
                    string value = String.Format("{0:X}", dvarValue);
                    returnValue = dvarValue.ToString("D") + " (dec) " + value + " (hex) ";
                    return returnValue;

                case "ENUM":
                    returnValue = PS3A.ReadString(dvarValue);
                    return returnValue;

                case "STRING":
                    returnValue = PS3A.ReadString(dvarValue);
                    return "\"" + returnValue + "\"" + "\n";

                default:
                    returnValue = ("Unable to determine value");
                    return returnValue;
            }
        }
    }
}
