using System;
using System.Collections.Generic;
using System.IO;

namespace btd5crypt
{
    internal class Program
    {
        enum CryptOperation
        {
            Encrypt, Decrypt
        }

        static void Main(string[] args)
        {

            if (args.Length < 3)
            {
                Console.WriteLine("Usage: {0} [-d | -e] <input file> <output file>", Environment.GetCommandLineArgs()[0]);
                Environment.Exit(1);
            }

            CryptOperation operation = CryptOperation.Decrypt;
            string inputFile = args[1];
            string outputFile = args[2];


            switch (args[0])
            {
                case "-d":
                    operation = CryptOperation.Decrypt;
                    break;

                case "-e":
                    operation = CryptOperation.Encrypt;
                    break;
            }


            if (operation == CryptOperation.Encrypt)
            {
                using (var stream = File.OpenWrite(outputFile))
                {
                    var save = new SaveFile(stream);
                    save.Encrypt(File.ReadAllBytes(inputFile));

                    Console.WriteLine("Wrote encrypted file to: {0}", inputFile);
                }
            }
            else if (operation == CryptOperation.Decrypt)
            {
                var save = new SaveFile(File.OpenRead(inputFile));
                save.Decrypt(File.OpenWrite(outputFile));

                Console.WriteLine("Wrote decrypted file to: {0}", outputFile);
            }
        }
    }
}
