using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Black_Ops_1_Zombie_Console
{
    class Menu
    {
        public static void MenuBaseHeader()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to primetime43's Black Ops 1 Zombie Console! :D\n");
            GetInGamePlayers();
            Console.WriteLine("######################################################### \nP.S. [If all the client name's do not load, you are not host!]\nMap: " + ModMethods.GetMapName() + "\n");
        }
        public static void GetInGamePlayers()
        {
            Console.WriteLine("Current in game players: ");
            for (int i = 0; i <= 3; i++)
            {
                byte[] client = new byte[0x20];
                PS3Util.PS3A.GetMemory((ulong)(0x1100820 + (i * 0x1d30)), ref client);
                Console.WriteLine("Client " + i + ": " + Encoding.ASCII.GetString(client));
            }
        }

        public static void MainMenuTop()
        {
            try
            {
                int selectedMenu = -1;
                while (selectedMenu != 0)
                {
                    Console.Clear();
                    MenuBaseHeader();
                    Console.WriteLine("---Main Menu---");
                    Console.WriteLine(
                        "Please select one of the sub menus:\n\n0 - Exit Program\n1 - Zombie Functions\n2 - Zombie Mods\n");
                    selectedMenu = Convert.ToInt32(Console.ReadLine());

                    if (selectedMenu == 0)
                    {
                        Application.Exit();
                    }
                    else if (selectedMenu == 1)
                    {
                        Console.Clear();
                        MenuBaseHeader();
                        FunctionsMenu();
                    }
                    else if (selectedMenu == 2)
                    {
                        Console.Clear();
                        MenuBaseHeader();
                        ModsMenu();
                    }
                    else
                    {
                        Console.WriteLine("The option you have entered is invalid!");
                        Console.Clear();
                        MenuBaseHeader();
                    }
                }
                Application.Exit();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }

        public static void FunctionsMenu()
        {
            try
            {
                int selectedFunction = -1;
                while (selectedFunction != 0)
                {
                    Console.Clear();
                    MenuBaseHeader();
                    Console.WriteLine("---Functions Menu---");
                    Console.WriteLine("\nSelect a function you would like to use:\n0 - Go back to Main Menu\n1 - CBUF_AddText(int localClientNum, const char *text)\n2 - Search for a dvar\n3 - SV_GameSendServerCommand(int clientNum, svscmd_type type, const char *text)\n4 - SetModel(int client, string model)\n");
                    selectedFunction = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    MenuBaseHeader();

                    if (selectedFunction == 1)//Cbuf_AddText
                    {
                        Console.WriteLine("Arguments: CBUF_AddText(const char *text)   (Ex: god)");
                        Console.Write("Enter the command: ");
                        string command = Console.ReadLine();
                        ModMethods.Cbuf_AddText(command);
                        Console.WriteLine("Command sent....");
                        Thread.Sleep(2000);
                    }
                    else if (selectedFunction == 2) //Discover Dvars
                    {
                        Console.Write("Enter the name of the dvar you want to find\n(Enter \"cancel\" to cancel the search): ");
                        string readLine = Console.ReadLine();
                        if (readLine.Equals("cancel"))
                        {
                            Console.WriteLine("Returning to functions.......");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            DiscoverDvarInfo.Dvars(readLine);
                        }
                    }
                    else if (selectedFunction == 3)//GSSC
                    {
                        Console.WriteLine("Select one of the following commands:\n0 - Cancel\n1 - Set Dvars\n2 - Text In The Center\n");
                        int option = Convert.ToInt32(Console.ReadLine());
                        int clientNumber;
                        switch (option)
                        {
                            case 0:
                                break;
                            case 1:
                                Console.Write("Enter the client number: ");
                                clientNumber = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter the dvar: ");
                                string dvar = Console.ReadLine();
                                ModMethods.SetDvars(clientNumber, dvar);
                                break;
                            case 2:
                                Console.Write("Enter the client number: ");
                                clientNumber = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter the text: ");
                                string text = Console.ReadLine();
                                ModMethods.OnScreenCenterText(clientNumber, text);
                                break;
                        }

                        if (option != 0)
                        {
                            Console.WriteLine("Command sent....");
                            Thread.Sleep(2000);
                        }
                    }
                    else if (selectedFunction == 4)//model
                    {
                        Console.Write("Enter the client number: ");
                        int clientNumber = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the model: ");
                        string model = Console.ReadLine();
                        ModMethods.SetModel(clientNumber, model);
                        Console.WriteLine("Command sent....");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("The option you have entered is invalid!");
                    }
                }
                Console.Clear();
                MenuBaseHeader();
                MainMenuTop();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }


        public static void ModsMenu()
        {
            try
            {
                int selectedModMenu = -1;
                while (selectedModMenu != 0)
                {
                    Console.Clear();
                    MenuBaseHeader();
                    Console.WriteLine("---Mods Menu---");
                    Console.WriteLine("\n0 - Go back to main menu\n1 - Mods\n2 - Stat Mods\n3 - Lobby String Editor\n4 - Weapons Menu\n5 - Images\n");
                    selectedModMenu = Convert.ToInt32(Console.ReadLine());

                    if (selectedModMenu == 1) //List of mods to use
                    {
                        int selectedMod = -1;
                        while (selectedMod != 0)
                        {
                            Console.Clear();
                            MenuBaseHeader();
                            Console.WriteLine(
                                "Select which mod you would like to activate!\n0 - Return to Mods Menu\n1 - Skip Rounds\n2 - Unlock All Trophies\n3 - God Mode\n4 - No Clip\n5 - UFO\n6 - No Target\n8 - Give All Weapons\n9 - Take Everything\n10 - Drop Current Weapon\n");
                            selectedMod = Convert.ToInt32(Console.ReadLine());
                            switch (selectedMod)
                            {
                                case 0:
                                    break;
                                case 1:
                                    ModMethods.SkipRounds();
                                    Console.Clear();
                                    MenuBaseHeader();
                                    break;
                                case 2:
                                    ModMethods.UnlockAllTrophies();
                                    Console.Clear();
                                    MenuBaseHeader();
                                    break;
                                case 3:
                                    ModMethods.GodMode();
                                    Console.Clear();
                                    MenuBaseHeader();
                                    break;
                                case 4:
                                    ModMethods.NoClip();
                                    Console.Clear();
                                    MenuBaseHeader();
                                    break;
                                case 5:
                                    ModMethods.Ufo();
                                    Console.Clear();
                                    MenuBaseHeader();
                                    break;
                                case 6:
                                    ModMethods.NoTarget();
                                    Console.Clear();
                                    MenuBaseHeader();
                                    break;
                                case 8:
                                    ModMethods.GiveAllWeapons();
                                    break;
                                case 9:
                                    ModMethods.TakeEverything();
                                    break;
                                case 10:
                                    ModMethods.DropWeapon();
                                    break;
                                default:
                                    Console.WriteLine("Unable to find mod! Returning to mods menu.......");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    MenuBaseHeader();
                                    ModsMenu();
                                    break;
                            }
                        }
                    }
                    else if (selectedModMenu == 2) //Stat mods
                    {
                        int selectedStat = -1;
                        while (selectedStat != 0)
                        {
                            Console.Clear();
                            MenuBaseHeader();
                            Console.WriteLine(
                                "Select which stat you wish to modify:\n0 - Return to Mods Menu\n1 - Points\n2 - Kills\n3 - Headshots\n");
                            selectedStat = Convert.ToInt32(Console.ReadLine());
                            switch (selectedStat)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Console.Write("Enter the client number: ");
                                    int clientPoints = Convert.ToInt32(Console.ReadLine());
                                    ModMethods.ModPoints(clientPoints);
                                    break;
                                case 2:
                                    Console.Write("Enter the client number: ");
                                    int clientKills = Convert.ToInt32(Console.ReadLine());
                                    ModMethods.ModKills(clientKills);
                                    break;
                                case 3:
                                    Console.Write("Enter the client number: ");
                                    int clientHeadshots = Convert.ToInt32(Console.ReadLine());
                                    ModMethods.ModHeadshots(clientHeadshots);
                                    break;
                                default:
                                    Console.WriteLine("Unable to find stat! Returning to mods menu.......");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    MenuBaseHeader();
                                    ModsMenu();
                                    break;
                            }
                        }
                    }
                    else if (selectedModMenu == 3)//Lobby String editor
                    {
                        int selectedString = -1;
                        while (selectedString != 0)
                        {
                            Console.Clear();
                            MenuBaseHeader();
                            Console.WriteLine("Select which string you wish to modify:\n0 - Return to Mods Menu\n1 - PSN Name\n2 - MOTD\n");
                            selectedString = Convert.ToInt32(Console.ReadLine());

                            switch (selectedString)
                            {
                                case 0:
                                    break;
                                case 1:
                                    ModMethods.PsnNameEditor();
                                    break;
                                case 2:
                                    ModMethods.MotdEditor();
                                    break;
                                default:
                                    Console.WriteLine("Unable to find string! Returning to mods menu.......");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    MenuBaseHeader();
                                    ModsMenu();
                                    break;
                            }
                        }
                    }
                    else if (selectedModMenu == 4)//Items Menu
                    {
                        int selectedItem = -1;
                        while (selectedItem != 0)
                        {
                            Console.Clear();
                            MenuBaseHeader();
                            Console.WriteLine("Select One Of The Follow Options:\n0 - Return to Mods Menu\n1 - Weapon Selection\n ");
                            selectedItem = Convert.ToInt32(Console.ReadLine());

                            switch (selectedItem)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Console.Clear();
                                    ModMethods.ListWeapons();
                                    Console.WriteLine("Enter the number that corresponds with the item you wish to acquire:");
                                    int itemId = Convert.ToInt32(Console.ReadLine());
                                    ModMethods.GetWeapon(itemId);
                                    break;
                                default:
                                    Console.WriteLine("Unable to find item! Returning to mods menu.......");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    MenuBaseHeader();
                                    ModsMenu();
                                    break;
                            }
                        }
                    }
                    else if (selectedModMenu == 5)//images
                    {
                        int selectedString = -1;
                        while (selectedString != 0)
                        {
                            Console.Clear();
                            MenuBaseHeader();
                            Console.WriteLine("Select an option:\n0 - Return to Mods Menu\n1 - Dump Images");
                            selectedString = Convert.ToInt32(Console.ReadLine());

                            switch (selectedString)
                            {
                                case 0:
                                    break;
                                case 1:
                                    ModMethods.loadImages();
                                    break;
                                default:
                                    Console.WriteLine("Unable to find option! Returning to mods menu.......");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    MenuBaseHeader();
                                    ModsMenu();
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The option you have entered is invalid!");
                    }
                }
                Console.Clear();
                MenuBaseHeader();
                MainMenuTop();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }
    }
}
