using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace btd5crypt
{
    internal class Program
    {
        enum CryptOperation
        {
            Encrypt, Decrypt
        }

        static string  ProgramName { get { return Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]); } }

        static void Usage()
        {
            Console.WriteLine("Usage: {0} [-d --decrypt | -e --encrypt] <input file> <output file>", ProgramName);
            Console.WriteLine("\t-d, --decrypt\tDecrypt the input file and write to output file");
            Console.WriteLine("\t-e, --encrypt\tEncrypt the input file and write to output file");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Usage();

                Environment.Exit(1);
            }


            CryptOperation operation = CryptOperation.Decrypt;
            string inputFile = args[1];
            string outputFile = args[2];


            switch (args[0])
            {
                case "--decrypt":
                case "-d":
                    operation = CryptOperation.Decrypt;
                    break;

                case "--encrypt":
                case "-e":
                    operation = CryptOperation.Encrypt;
                    break;

                case "--help":
                case "-h":
                    Usage();
                    Environment.Exit(0);
                    return;

                default:
                    Console.WriteLine("{0}: Unrecognised operation: {1}", ProgramName, args[0]);
                    Environment.Exit(1);
                    return;
            }


            if (operation == CryptOperation.Encrypt)
            {
                var saveBuffer = File.ReadAllBytes(inputFile);

                using (var stream = File.OpenWrite(outputFile))
                {
                    var save = new SaveFile(stream);
                    save.Encrypt(saveBuffer);

                    Console.WriteLine("Wrote encrypted file to: {0}", inputFile);
                }
            }
            else if (operation == CryptOperation.Decrypt)
            {
                bool crcValid = false;

                var save = new SaveFile(File.Open(inputFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite));
                save.Decrypt(File.Open(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), ref crcValid);

                if (!crcValid)
                {
                    Console.WriteLine("WARNING: CRC mismatch, save may be corrupted");
                }

                Console.WriteLine("Wrote decrypted file to: {0}", outputFile);
            }
        }
    }
}
