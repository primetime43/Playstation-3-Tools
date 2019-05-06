using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOIZMRPC
{
    public class PS3
    {
        static uint processID;

        private static uint GetProcessID()
        {
            uint[] ProcessIDs;
            PS3TMAPI.GetProcessList(0, out ProcessIDs);
            return ProcessIDs[0];
        }

        public static void SetMemory(uint address, byte[] memory)
        {
            PS3TMAPI.ProcessSetMemory(0, PS3TMAPI.UnitType.PPU, processID, 0, address, memory);
        }

        public static byte[] GetMemory(uint address, int length)
        {
            byte[] bytes = new byte[length];
            PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, processID, 0, address, ref bytes);
            return bytes;
        }

        public static int Init()
        {
            processID = GetProcessID();
            return Rpc.Init();
        }
    }
    public class Rpc
    {
        public static UInt32 function_address = 0x6E34E0; //MP = 0x7A21E0  ||  ZM = 0x6E34E0

        public static int Init()
        {
            if (function_address == 0) return -1;
            Enable_RPC();
            return 0;
        }
        public static void Enable_RPC()
        {
            PS3.SetMemory(function_address, new byte[] { 0x4e, 0x80, 0, 0x20 });
            System.Threading.Thread.Sleep(20);
            byte[] memory = new byte[] {
            0x7c, 8, 2, 0xa6, 0xf8, 1, 0, 0x80, 60, 0x60, 0x10, 5, 0x81, 0x83, 0, 0x4c,
            0x2c, 12, 0, 0, 0x41, 130, 0, 100, 0x80, 0x83, 0, 4, 0x80, 0xa3, 0, 8,
            0x80, 0xc3, 0, 12, 0x80, 0xe3, 0, 0x10, 0x81, 3, 0, 20, 0x81, 0x23, 0, 0x18,
            0x81, 0x43, 0, 0x1c, 0x81, 0x63, 0, 0x20, 0xc0, 0x23, 0, 0x24, 0xc0, 0x43, 0, 40,
            0xc0, 0x63, 0, 0x2c, 0xc0, 0x83, 0, 0x30, 0xc0, 0xa3, 0, 0x34, 0xc0, 0xc3, 0, 0x38,
            0xc0, 0xe3, 0, 60, 0xc1, 3, 0, 0x40, 0xc1, 0x23, 0, 0x48, 0x80, 0x63, 0, 0,
            0x7d, 0x89, 3, 0xa6, 0x4e, 0x80, 4, 0x21, 60, 0x80, 0x10, 5, 0x38, 160, 0, 0,
            0x90, 0xa4, 0, 0x4c, 0x90, 100, 0, 80, 0xe8, 1, 0, 0x80, 0x7c, 8, 3, 0xa6,
            0x38, 0x21, 0, 0x70, 0x4e, 0x80, 0, 0x20};
            PS3.SetMemory(function_address + 4, memory);
            PS3.SetMemory(0x10050000, new byte[0x2854]);
            PS3.SetMemory(function_address, new byte[] { 0xf8, 0x21, 0xff, 0x91 });
        }

        public static uint Call(uint func_address, params object[] parameters)
        {
            int length = parameters.Length;
            uint num2 = 0;
            for (uint i = 0; i < length; i++)
            {
                if (parameters[i] is int)
                {
                    byte[] array = BitConverter.GetBytes((int)parameters[i]);
                    Array.Reverse(array);
                    PS3.SetMemory(0x10050000 + ((i + num2) * 4), array);
                }
                else if (parameters[i] is uint)
                {
                    byte[] buffer2 = BitConverter.GetBytes((uint)parameters[i]);
                    Array.Reverse(buffer2);
                    PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer2);
                }
                else if (parameters[i] is string)
                {
                    byte[] buffer3 = Encoding.UTF8.GetBytes(Convert.ToString(parameters[i]) + "\0");
                    PS3.SetMemory(0x10050054 + (i * 0x400), buffer3);
                    uint num4 = 0x10050054 + (i * 0x400);
                    byte[] buffer4 = BitConverter.GetBytes(num4);
                    Array.Reverse(buffer4);
                    PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer4);
                }
                else if (parameters[i] is float)
                {
                    num2++;
                    byte[] buffer5 = BitConverter.GetBytes((float)parameters[i]);
                    Array.Reverse(buffer5);
                    PS3.SetMemory(0x10050024 + ((num2 - 1) * 4), buffer5);
                }
            }
            byte[] bytes = BitConverter.GetBytes(func_address);
            Array.Reverse(bytes);
            PS3.SetMemory(0x1005004c, bytes);
            System.Threading.Thread.Sleep(20);
            byte[] memory = new byte[4];
            PS3Util.PS3A.GetMemory(0x10050050, ref memory);
            Array.Reverse(memory);
            return BitConverter.ToUInt32(memory, 0);
        }
    }
}