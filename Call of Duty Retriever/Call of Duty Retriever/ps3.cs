using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PS3Util
{
    class PS3
    {
        #region UTILITY
        public static bool connected = false;
        public static int target = 0;
        public static uint processID;
        private static uint[] processIDs = new uint[64];
        private static int unit = 0;

        public static bool Connect(bool pickTarget = true, bool askExit = true)
        {
            if (SUCCEEDED(InitTargetComms()))
            {
                if (pickTarget)
                    PickTarget((IntPtr)null, out target);
                if (SUCCEEDED(Connect(target, null)))
                {
                    connected = true;
                    GetProcessList(target, out processIDs);
                    processID = processIDs[0];
                    if (SUCCEEDED(ProcessAttach(target, unit, processID)))
                    {
                        ProcessContinue(target, processID);
                        MessageBox.Show("Successfully connected!");
                        return true;
                    }
                }
                else
                    MessageBox.Show("Failed to connect to target", "Error!");
            }
            else
                MessageBox.Show("Failed to initiate target communications", "Error!");

            // failure
            if (askExit)
                if (MessageBox.Show("Exit Target Manager?", "Failed to connect", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Exit();

            CloseTargetComms();
            return false;
        }

        public static void Disconnect(bool askExit = true)
        {
            if (askExit)
                if (MessageBox.Show("Exit Target Manager?", "Goodbye :(", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Exit();

            CloseTargetComms();
        }
        public static byte[] ReadBytes(uint addr, int lenght)
        {
            byte[] Buffer = new byte[lenght];
            PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, processID, 0, addr, ref Buffer);
            return Buffer;
        }
        public static uint FollowPointer(uint addr)
        {
            return (uint)PS3.GetInt32(addr);
        }

        public static string ReplaceString(string PSNString)
        {
            PSNString = PSNString.Replace("00", string.Empty);
            PSNString = PSNString.Replace("-", string.Empty);
            PSNString = PSNString.Replace("^0", string.Empty);
            PSNString = PSNString.Replace("^1", string.Empty);
            PSNString = PSNString.Replace("^2", string.Empty);
            PSNString = PSNString.Replace("^3", string.Empty);
            PSNString = PSNString.Replace("^4", string.Empty);
            PSNString = PSNString.Replace("^5", string.Empty);
            PSNString = PSNString.Replace("^6", string.Empty);
            PSNString = PSNString.Replace("^7", string.Empty);
            PSNString = PSNString.Replace("^8", string.Empty);
            PSNString = PSNString.Replace("^9", string.Empty);
            return PSNString;
        }
        public static string ReadString(uint addr)
        {
            byte[] Reader = new byte[1];
            uint i = 0;
            List<byte> StringBuilder = new List<byte>();
            while (true)
            {
                PS3.GetMemory(addr + i, ref Reader);
                if (Reader[0] == 0x00)
                    break;
                else
                    StringBuilder.Add(Reader[0]);
                i++;
            }
            System.Text.UTF8Encoding Encode = new System.Text.UTF8Encoding();
            return Encode.GetString(StringBuilder.ToArray());
        }

        public static void GetMemory(ulong address, ref byte[] buffer)
        {
            ProcessGetMemory(target, unit, processID, 0, address, buffer.Length, buffer);
        }

        public static byte[] GetBytes(ulong address, int count)
        {
            byte[] buffer = new byte[count];
            GetMemory(address, ref buffer);
            return buffer;
        }

        public static Int16 GetInt8(ulong address)
        {
            byte[] buffer = new byte[1];
            GetMemory( address, ref buffer );
            return buffer[0];
        }

        public static Int16 GetInt16(ulong address)
        {
            byte[] buffer = new byte[2];
            GetMemory(address, ref buffer);
            Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        public static Int32 GetInt32(ulong address)
        {
            byte[] buffer = new byte[4];
            GetMemory(address, ref buffer);
            Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static Int64 GetInt64(ulong address)
        {
            byte[] buffer = new byte[4];
            GetMemory(address, ref buffer);
            Array.Reverse(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static string GetString(ulong address)
        {
            byte[] buffer = new byte[1000];
            GetMemory(address, ref buffer);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        // get string through pointer
        public static string GetPString(ulong address)
        {
            UInt32 ptr = (UInt32)GetInt32(address);
            return GetString(ptr);
        }

        public static float GetFloat(ulong address)
        {
            byte[] buffer = new byte[4];
            GetMemory(address, ref buffer);
            Array.Reverse(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static double GetDouble(ulong address)
        {
            byte[] buffer = new byte[8];
            GetMemory(address, ref buffer);
            Array.Reverse(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        public static void SetMemory(ulong address, byte[] buffer)
        {
            ProcessSetMemory(target, unit, processID, 0, address, buffer.Length, buffer);
        }

        public static void SetInt16(ulong address, Int16 value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            SetMemory(address, buffer);
        }

        public static void SetInt32(ulong address, Int32 value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            SetMemory(address, buffer);
        }

        public static void SetInt64(ulong address, Int64 value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            SetMemory(address, buffer);
        }

        public static void SetFloat(ulong address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            SetMemory(address, buffer);
        }
        #endregion
        #region EXTERNALS
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3InitTargetComms", CallingConvention = CallingConvention.Cdecl)]
        private static extern int InitTargetComms();

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessGetMemory", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ProcessGetMemory(int target, int unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessSetMemory", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ProcessSetMemory(int target, int unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PickTarget", CallingConvention = CallingConvention.Cdecl)]
        private static extern int PickTarget(IntPtr hWndOwner, out int target);

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Connect", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Connect(int target, string application);

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessList", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetProcessListExt(int target, ref uint count, IntPtr processIdArray);

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessAttach", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ProcessAttach(int target, int unitId, uint processId);

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessContinue", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ProcessContinue(int target, uint processId);

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3CloseTargetComms", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseTargetComms();

        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Exit", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Exit();

        public static bool SUCCEEDED(int res)
        {
            return res >= 0;
        }

        public static bool FAILED(int res)
        {
            return !SUCCEEDED(res);
        }

        private static IntPtr ReadDataFromUnmanagedIncPtr<T>(IntPtr unmanagedBuf, ref T storage)
        {
            storage = (T)Marshal.PtrToStructure(unmanagedBuf, typeof(T));
            return new IntPtr(unmanagedBuf.ToInt64() + (long)Marshal.SizeOf((object)storage));
        }

        private static int GetProcessList(int target, out uint[] processIDs)
        {
            processIDs = (uint[])null;
            IntPtr processIdArray = IntPtr.Zero;
            uint count = 0U;
            int res;

            if (FAILED(res = GetProcessListExt(target, ref count, processIdArray)))
                return res;

            IntPtr num = Marshal.AllocHGlobal(4 * (int)count);
            if (FAILED(res = GetProcessListExt(target, ref count, num)))
            {
                Marshal.FreeHGlobal(num);
                return res;
            }
            else
            {
                IntPtr unmanagedBuf = num;
                processIDs = new uint[count];
                for (uint index = 0U; index < count; ++index)
                    unmanagedBuf = ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processIDs[index]);
                Marshal.FreeHGlobal(num);
                return res;
            }
        }
        #endregion
    }
}