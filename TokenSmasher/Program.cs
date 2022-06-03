using System;
using System.IO;
using System.Threading;
using System.Drawing;

using Console = Colorful.Console;

using TokenSmasher.Core;
using TokenSmasher.Helpers;
using TokenSmasher.Checker.Models;

namespace TokenSmasher {
    class Program {

        static void Main(string[] args) {
            ConsoleHelpers.PrintWatermark();
            Console.ForegroundColor = Color.FromArgb(180, 180, 180);
            ConsoleHelpers.PrintWithColor(@"version 2.0", @"by @notdippe | edited by @astroso", Color.FromArgb(138, 142, 255), true);


            if (!File.Exists("tokens.txt")) {
                File.Create("tokens.txt");
                ConsoleHelpers.PrintWithColor(@"info", @"please put tokens into 'tokens.txt' file.", Color.FromArgb(232, 44, 79), true);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            foreach (string token in File.ReadAllLines("tokens.txt")) 
                TokenModel.Tokens.Add(token);

            if (TokenModel.Tokens.Count <= 0) {
                ConsoleHelpers.PrintWithColor(@"info", @"tokens.txt is empty", Color.FromArgb(232, 44, 79), true);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            ConsoleHelpers.PrintWithColor(@"info", $@"loaded {TokenModel.Tokens.Count} tokens.", Color.FromArgb(138, 142, 255), true);

            Thread tChecker = new Thread(new ThreadStart(Core.Checker.Init));
            tChecker.Start();
            Console.Read();
        }
    }
}
