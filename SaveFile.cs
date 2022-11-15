using System;
using System.IO;
using System.Linq;
using System.Text;

namespace btd5crypt
{
    internal class SaveFile
    {
        private static byte[] Magic = new byte[] { (byte)'D', (byte)'G', (byte)'D', (byte)'A', (byte)'T', (byte)'A' };

        public Stream Stream { get; protected set; }

        public SaveFile(Stream input)
        {
            Stream = input;
        }

        public void Decrypt(Stream output)
        {
            var saveMagic = new byte[Magic.Length];

            Stream.Read(saveMagic);

            if(!saveMagic.SequenceEqual(Magic))
            {
                throw new SaveMalformedException("Save file does not contain expected header magic");
            }

            var crcAscii = new byte[8];

            Stream.Read(crcAscii);

            var crc = uint.Parse(ASCIIEncoding.ASCII.GetString(crcAscii), System.Globalization.NumberStyles.HexNumber);
            
            var data = new byte[Stream.Length - (Magic.Length + 8)];

            Stream.Read(data);

            Crypt.Decrypt(ref data, 0, data.Length);

            var calcCrc = Crypt.Checksum(data, 0, data.Length);

            output.Write(data);
        }

        internal void Encrypt(byte[] saveData)
        {
            Stream.Write(Magic);

            var calcCrc = Crypt.Checksum(saveData, 0, saveData.Length);
            var asciiCrc = calcCrc.ToString("x2");
            var crcBytes = ASCIIEncoding.ASCII.GetBytes(asciiCrc);

            Stream.Write(crcBytes);

            Crypt.Encrypt(ref saveData, 0, saveData.Length);

            Stream.Write(saveData);
        }
    }
}
