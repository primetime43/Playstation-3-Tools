using System;
using System.Threading;
using PS3Lib;

namespace Black_Ops_1_Zombie_Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            int selectedMenu = -1;
            Console.Title = "Black Ops 1 Zombie Console by primetime43";
            Console.WriteLine("Select the option that applies to you:\n1 - DEX\n2 - CEX");
            selectedMenu = Convert.ToInt32(Console.ReadLine());

            while (selectedMenu != 1 & selectedMenu != 2)
            {
                Console.Clear();
                Console.WriteLine("Select the option that applies to you:\n1 - DEX\n2 - CEX");
                selectedMenu = Convert.ToInt32(Console.ReadLine());
            }

            if (selectedMenu == 1) //DEX
            {
                try
                {
                    bool connect = PS3Util.PS3A.Connect(false, true);
                    if (connect == true)
                    {
                        BOIZMRPC.Rpc.Enable_RPC();
                        BOIZMRPC.PS3.Init();
                        //Top Menu Base
                        Menu.MenuBaseHeader();
                        Menu.MainMenuTop(); //Opens the menu for mods etc


                        Console.WriteLine("Press any button to exit the app!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Press any button to exit the app!");
                        Console.ReadKey();
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Error!");
                }
            }
            else if (selectedMenu == 2)//CEX
            {
                String IPAddress = "";
                /*try
                {*/
                    CCAPI CEXPS3 = new CCAPI();
                    Console.Write("Enter your PS3's IP Address: ");
                    IPAddress = Console.ReadLine();
                    CEXPS3.ConnectTarget(IPAddress);

                    if (CEXPS3.SUCCESS(CEXPS3.AttachProcess()))
                    {
                        BOIZMRPC.Rpc.Enable_RPC();
                        BOIZMRPC.PS3.Init();
                        //Top Menu Base
                        Menu.MenuBaseHeader();
                        Menu.MainMenuTop(); //Opens the menu for mods etc


                        Console.WriteLine("Press any button to exit the app!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Press any button to exit the app!");
                        Console.ReadKey();
                    }
                /*}
                catch (Exception)
                {
                    Console.WriteLine("Error!");
                }*/
            }
        }
    }
}
