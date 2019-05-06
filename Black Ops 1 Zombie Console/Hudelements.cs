using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOIZMRPC;
using PS3Lib;

namespace Black_Ops_1_Zombie_Console
{
    class Hudelements
    {
        public class Huds
        {
            private static PS3API PS3lib = new PS3API();
            public class HudStruct
            {
                public static uint
                    G_HudElems = 0x00EE7B84,
                IndexSize = 0x84,
                x = 0x0,
                y = 0x4,
                z = 0x8,
                fontScaleStartTime = 0x14,
                fontScale = 0xC,
                entNum = 0x18,
                color = 0x18,
                teamNum = 0x1C,
                fromColor = 0x1C,
                icon = 0x20,
                fadeStartTime = 0x20,
                scaleStartTime = 0x24,
                fromX = 0x28,
                fromY = 0x2C,
                moveStartTime = 0x30,
                time = 0x34,
                duration = 0x38,
                value = 0x3C,
                sort = 0x40,
                glowColor = 0x44,
                fxBirthTime = 0x48,
                targetEntNum = 0x4C,
                fontScaleTime = 0x4E,
                fadeTime = 0x50,
                label = 0x52,
                width = 0x54,
                height = 0x56,
                fromWidth = 0x58,
                fromHeight = 0x5A,
                scaleTime = 0x5C,
                moveTime = 0x5E,
                text = 0x60,
                fxDecayStartTime = 0x64,
                fxDecayDuration = 0x66,
                fxRedactDecayStartTime = 0x68,
                fxRedactDecayDuration = 0x6A,
                flags = 0x6C,
                type = 0x6E,
                font = 0x6F,
                alignOrg = 0x70,
                alignScreen = 0x71,
                materialIndex = 0x72,
                fxLetterTime = 0x62,
                offscreenMaterialIdx = 0x73,
                fromAlignOrg = 0x74,
                fromAlignScreen = 0x75,
                soundID = 0x76,
                ui3dWindow = 0x77,
                clientNum = 0x78,
                team = 0x7C,
                archived = 0x80;
            }
            public static short G_LocalizedStringIndex(string Text)
            {
                return (short)Rpc.Call(0x00304BA0, Text);
            }
            public static void ChangeText(UInt32 elemIndex, String Text)
            {
                PS3lib.Extension.WriteInt16(elemIndex + HudStruct.text, G_LocalizedStringIndex(Text));
            }

            public static void WriteSingle(UInt32 address, float input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                PS3.SetMemory(address, array);
            }

            public static uint SetText(int clientIndex, string TextString, int Font, Single FontSize, Single X, Single Y, uint align = 0, int r = 255, int g = 255, int b = 255, int a = 255, int glowr = 255, int glowg = 255, int glowb = 255, int glowa = 0)
            {
                uint text = HudElem_Alloc();
                PS3lib.Extension.WriteByte(text + HudStruct.type, 1);
                WriteSingle(text + HudStruct.fontScale, FontSize);
                PS3lib.Extension.WriteByte(text + HudStruct.font, Convert.ToByte(Font));
                if (align != 0)
                { PS3lib.Extension.WriteByte(text + HudStruct.alignOrg, 5); PS3lib.Extension.WriteByte(text + HudStruct.alignScreen, Convert.ToByte(align)); }
                else
                { WriteSingle(text + HudStruct.x, X); WriteSingle(text + HudStruct.y, Y); }
                PS3lib.Extension.WriteInt32(text + HudStruct.clientNum, clientIndex);
                PS3lib.Extension.WriteInt16(text + HudStruct.text, G_LocalizedStringIndex(TextString));
                PS3lib.Extension.WriteBytes(text + HudStruct.color, new Byte[] { (Byte)r, (Byte)g, (Byte)b, (Byte)a });
                PS3lib.Extension.WriteBytes(text + HudStruct.glowColor, new Byte[] { (Byte)glowr, (Byte)glowg, (Byte)glowb, (Byte)glowa });
                PS3lib.Extension.WriteByte(text + HudStruct.ui3dWindow, 0xFF);
                return text;
            }
            public static uint SetShader(int clientIndex, int Material, short Width, short Height, Single X, Single Y, uint align = 0, int r = 255, int g = 255, int b = 255, int a = 255)
            {
                uint Shader = HudElem_Alloc();
                PS3lib.Extension.WriteByte(Shader + HudStruct.type, 4);
                PS3lib.Extension.WriteByte(Shader + HudStruct.materialIndex, Convert.ToByte(Material));
                PS3lib.Extension.WriteInt16(Shader + HudStruct.height, Height);
                PS3lib.Extension.WriteInt16(Shader + HudStruct.width, Width);
                if (align != 0)
                { PS3lib.Extension.WriteByte(Shader + HudStruct.alignOrg, 5); PS3lib.Extension.WriteByte(Shader + HudStruct.alignScreen, Convert.ToByte(align)); }
                else
                { WriteSingle(Shader + HudStruct.x, X); WriteSingle(Shader + HudStruct.y, Y); }
                PS3lib.Extension.WriteInt32(Shader + HudStruct.clientNum, clientIndex);
                PS3lib.SetMemory(Shader + HudStruct.ui3dWindow, new Byte[] { 0xFF });
                PS3lib.Extension.WriteBytes(Shader + HudStruct.color, new Byte[] { (Byte)r, (Byte)g, (Byte)b, (Byte)a });
                return Shader;
            }
            public static uint HudElem_Alloc()
            {
                for (uint i = 40; i < 1024; i++)
                {
                    uint Index = HudStruct.G_HudElems + (i * HudStruct.IndexSize);
                    if (PS3lib.Extension.ReadByte(Index + HudStruct.type) == 0)
                    {
                        PS3lib.Extension.WriteBytes(Index, new Byte[0x88]);
                        return Index;
                    }
                }
                return 0;
            }
            public static void DestroyElement(uint Element)
            {
                PS3.SetMemory(Element, new byte[HudStruct.IndexSize]);
            }
        }

        //Buttons Monitoring
        //==================

        public static class Buttons
        {
            public static Int32
                L1 = 1048704,
                L2 = 72704,
                L3 = 1074003968,
                R1 = -2147483648,
                R2 = 131072,
                R3 = 536870912,
                Square = 67108864,
                Cross = 2104320,
                Crouch = 4194304,
                Prone = 8388608,
                Triangle = 8;
        }
        public static bool ButtonPressed(int client, int Button)
        {
            if (Convert.ToInt32((0x11007E8 + ((uint)client * 0x1D30))) == Button)
                return true;
            else return false;
        }

        //Addresses
        //=========

        class Addresses
        {
            public static uint
            G_entityZM = 0xFA805C,
            G_entitySizeZM = 0x34C,
            G_clientZM = 0x010fed78,
            G_clientSizeZM = 0x1D30;
        }

        //Functions
        //=========
        private static PS3API PS3lib = new PS3API();
        public static string GetPlayerNameForMenu(Int32 clientIndex)
        {
            String Name = PS3lib.Extension.ReadString(0x011008B8 + ((uint)clientIndex * 0x1d30));
            if (Name == "")
                return "Not Connected";
            else
                return Name;
        }

        public string GetMap()
        {
            return PS3lib.Extension.ReadString(0x0138E1B8);
        }

        public int getMapMaterialWhiteShader()
        {
            string map = GetMap();
            if (map == "zombie_theater")
            {
                return 18;
            }
            if (map == "zombie_pentagon")
            {
                return 17;
            }
            if (map == "zombie_cod5_prototype")
            {
                return 16;
            }
            if (map == "zombie_asylum")
            {
                return 16;
            }
            if (map == "zombie_swamp")
            {
                return 16;
            }
            if (map == "zombie_cod5_factory")
            {
                return 16;
            }
            if (map == "zombie_cosmodrome")
            {
                return 16;
            }
            if (map == "zombie_coast")
            {
                return 19;
            }
            if (map == "zombie_temple")
            {
                return 17;
            }
            if (map == "zombie_paris")
            {
                return 16;
            }
            else
            {
                return 0;
            }
        }

        public int getMapMaterialBlackShader()
        {
            string map = GetMap();
            if (map == "zombie_theater")
            {
                return 11;
            }
            if (map == "zombie_pentagon")
            {
                return 10;
            }
            if (map == "zombie_cod5_prototype")
            {
                return 9;
            }
            if (map == "zombie_asylum")
            {
                return 9;
            }
            if (map == "zombie_swamp")
            {
                return 9;
            }
            if (map == "zombie_cod5_factory")
            {
                return 9;
            }
            if (map == "zombie_cosmodrome")
            {
                return 9;
            }
            if (map == "zombie_coast")
            {
                return 12;
            }
            if (map == "zombie_temple")
            {
                return 10;
            }
            if (map == "zombie_paris")
            {
                return 9;
            }
            else
            {
                return 0;
            }
        }
        public static int GetHost()
        {
            string Host = PS3lib.Extension.ReadString(0x01C33DB0);
            int i = 0;
            if (Host == "")
            {
                i = 0;
            }
            else if (Host == PS3lib.Extension.ReadString(0x011008B8 + (0 * 0x1d30)))
            {
                i = 0;
            }
            else if (Host == PS3lib.Extension.ReadString(0x011008B8 + (1 * 0x1d30)))
            {
                i = 1;
            }
            else if (Host == PS3lib.Extension.ReadString(0x011008B8 + (2 * 0x1d30)))
            {
                i = 2;
            }
            else if (Host == PS3lib.Extension.ReadString(0x011008B8 + (3 * 0x1d30)))
            {
                i = 3;
            }
            return i;
        }
    }
}
