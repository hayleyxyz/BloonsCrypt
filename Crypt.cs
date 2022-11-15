using System;

namespace btd5crypt
{
    internal static class Crypt
    {
        public static void Decrypt(ref byte[] data, int start, int length)
        {
            for (int i = start; i < length; i++)
            {
                data[start + i] -= (byte)((i % 6) + 0x15);
            }
        }

        public static void Encrypt(ref byte[] data, int start, int length)
        {
            for (int i = start; i < length; i++)
            {
                data[start + i] += (byte)((i % 6) + 0x15);
            }
        }

        public static uint Checksum(byte[] data, int start, int length)
        {
            uint crc = 0;

            for (int i = 0; i < data.Length; i++)
            {
                crc ^= data[i];
                uint edx = (crc >> 8);
                uint eax = crc & 0xff;

                for (var x = 0; x < 8; x++)
                {
                    if ((eax & 1) == 1)
                    {
                        eax = (uint)(((int)eax >> 1));
                        eax = (uint)((int)eax ^ 0xEDB88320);
                    }
                    else
                    {
                        eax = (uint)((int)eax >> 1);
                    }
                }

                crc = eax ^ edx;
            }

            return crc;
        }
    }
}
