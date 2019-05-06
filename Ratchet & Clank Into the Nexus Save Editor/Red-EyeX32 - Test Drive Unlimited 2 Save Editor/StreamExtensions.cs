using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Red_EyeX32___Test_Drive_Unlimited_2_Save_Editor
{
    public static class StreamExtensions
    {
        public static short LittleEndianReadInt16(this Stream stream)
        {
            byte[] buffer = new byte[2];
            stream.Read(buffer, 0, 2);
            Array.Reverse(buffer, 0, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        public static int findNearestNull(this Stream stream)
        {
            bool flag = false;
            long position = stream.Position;
            while (!flag)
            {
                position -= 1L;
                stream.Position = position;
                if (stream.ReadByte() == 0)
                {
                    return (int)stream.Position;
                }
                if (stream.Position < 0L)
                {
                    return -1;
                }
            }
            return -1;
        }

        public static long findNearestPointer(this Stream stream)
        {
            bool flag = false;
            long position = stream.Position;
            while (!flag)
            {
                position -= 1L;
                stream.Position = position;
                if (stream.ReadInt32() == -1)
                {
                    flag = true;
                }
            }
            return position;
        }

        public static string ReadBinShort(this Stream stream)
        {
            byte[] Size = stream.ReadBytes(2);
            string HSize = BitConverter.ToString(Size);
            HSize = HSize.Replace("-", "");
            return HSize;
        }

        public static int PeekChar(this Stream stream)
        {
            int num = stream.ReadByte();
            stream.Position -= 1L;
            return num;
        }

        public static bool ReadBoolean(this Stream stream)
        {
            return Convert.ToBoolean(stream.ReadByte());
        }

        public static byte[] ReadBytes(this Stream stream, int count)
        {
            byte[] buffer = new byte[count];
            stream.Read(buffer, 0, count);
            return buffer;
        }

        public static char ReadChar(this Stream stream)
        {
            return Convert.ToChar(stream.ReadByte());
        }

        public static char[] ReadChars(this Stream stream, int count)
        {
            char[] chArray = new char[count];
            for (int i = 0; i < count; i++)
            {
                chArray[i] = Convert.ToChar(stream.ReadByte());
            }
            return chArray;
        }

        public static string ReadColor(this Stream stream)
        {
            float num = stream.ReadFloat();
            float num2 = stream.ReadFloat();
            float num3 = stream.ReadFloat();
            float num4 = stream.ReadFloat();
            return (num.ToString() + " " + num2.ToString() + " " + num3.ToString() + " " + num4.ToString());
        }

        public static double ReadDouble(this Stream stream)
        {
            byte[] buffer = new byte[8];
            stream.Read(buffer, 0, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        public static float ReadFloat(this Stream stream)
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            Array.Reverse(buffer, 0, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static short ReadInt16(this Stream stream)
        {
            byte[] buffer = new byte[2];
            stream.Read(buffer, 0, 2);
            Array.Reverse(buffer, 0, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        public static int ReadInt32(this Stream stream)
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            Array.Reverse(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static long ReadInt64(this Stream stream)
        {
            byte[] buffer = new byte[8];
            stream.Read(buffer, 0, 8);
            Array.Reverse(buffer, 0, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static string ReadNullTerminatedString(this Stream stream)
        {
            string str = string.Empty;
            int num = -1;
            while ((num = stream.ReadByte()) != 0)
            {
                str = str + Convert.ToChar(num);
            }
            return str;
        }

        public static string ReadRectDef(this Stream stream)
        {
            float num = stream.ReadFloat();
            float num2 = stream.ReadFloat();
            float num3 = stream.ReadFloat();
            float num4 = stream.ReadFloat();
            int num5 = stream.ReadChar();
            int num6 = stream.ReadChar();
            stream.ReadBytes(2);
            return (num.ToString() + " " + num2.ToString() + " " + num3.ToString() + " " + num4.ToString() + " " + num5.ToString() + " " + num6.ToString());
        }

        public static sbyte ReadSByte(this Stream stream)
        {
            return Convert.ToSByte(stream.ReadByte());
        }

        public static float ReadSingle(this Stream stream)
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static ushort ReadUInt16(this Stream stream)
        {
            byte[] buffer = new byte[2];
            stream.Read(buffer, 0, 2);
            Array.Reverse(buffer, 0, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public static uint ReadUInt32(this Stream stream)
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            Array.Reverse(buffer, 0, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static ulong ReadUInt64(this Stream stream)
        {
            byte[] buffer = new byte[8];
            stream.Read(buffer, 0, 8);
            Array.Reverse(buffer, 0, 8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static String ReadEncodedString(this Stream stream, Int32 size, Encoding encoding)
        {
            Byte[] buffer = new Byte[encoding.GetMaxByteCount(size)];
            stream.Read(buffer, 0, buffer.Length);

            return encoding.GetString(buffer, 0, buffer.Length);
        }

        public static void Rewind(this Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0L, SeekOrigin.Begin);
            }
        }

        public static void WriteBytes(this Stream stream, byte[] buffer)
        {
            foreach (byte b in buffer)
                stream.WriteByte(b);
        }

        public static void WriteBoolean(this Stream stream, bool value)
        {
            stream.WriteByte(Convert.ToByte(value));
        }

        public static void WriteChar(this Stream stream, char value)
        {
            stream.WriteByte(Convert.ToByte(value));
        }

        public static void WriteCString(this Stream stream, string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                stream.WriteByte(Convert.ToByte(value[i]));
            }
            stream.WriteByte(0);
        }

        public static void WriteString(this Stream stream, string value)
        {
            for (int i = 0; i < value.Length; i++)
                stream.WriteByte(Convert.ToByte(value[i]));
        }

        public static void WriteDouble(this Stream stream, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, 8);
        }

        public static void WriteFloat(this Stream stream, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            stream.Write(bytes, 0, 4);
        }

        public static void WriteInt16(this Stream stream, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            stream.Write(bytes, 0, 2);
        }

        public static void WriteInt32(this Stream stream, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            stream.Write(bytes, 0, 4);
        }

        public static void WriteInt64(this Stream stream, long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            stream.Write(bytes, 0, 8);
        }

        public static void WriteSByte(this Stream stream, sbyte value)
        {
            stream.WriteByte(Convert.ToByte(value));
        }

        public static void WriteSingle(this Stream stream, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, 4);
        }

        public static void WriteUInt16(this Stream stream, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            stream.Write(bytes, 0, 2);
        }

        public static void WriteUInt32(this Stream stream, uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            stream.Write(bytes, 0, 4);
        }

        public static void WriteUInt64(this Stream stream, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            stream.Write(bytes, 0, 8);
        }

        public static void WriteEncodedString(this Stream stream, String value, Encoding encoding)
        {
            Int32 size = encoding.GetByteCount(value);
            Byte[] buffer = encoding.GetBytes(value);

            stream.Write(buffer, 0, size);
        }
    }
}