using EasyConsoleClass;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TokensChecker;
using Variabili;

namespace TokenSmasher {
    class Program {
        public static void printf(string txt) => Console.WriteLine(txt);

        static void Main(string[] args) {

            #region initialtitle
            Console.Title = "Token Smasher | Tokens: 0 | Live: 0 | Died: 0 | Locked: 0";
            Watermark();
            #endregion

            #region load
            int tokens = 0;
            if (!File.Exists("tokens.txt")) {
                File.Create("tokens.txt");
                EasyConsole.WriteLine("Put Tokens Into 'tokens.txt' file!", ConsoleColor.Red, true);
                Environment.Exit(-1);
            }
            foreach (string token in File.ReadAllLines("tokens.txt")) {
                tokens++;
                Variables.Tokens.Add(token);
            }

            if (tokens <= 0) {
                EasyConsole.WriteLine($"tokens.txt is empty!, fill the txt file", ConsoleColor.Red, true);
                Environment.Exit(-1);
            }
            Console.Title = $"Token Smasher | Tokens: {tokens} | Live: 0 | Died: 0 | Locked: 0 ";

            EasyConsole.WriteLine($"[+] Loaded {tokens} tokens", ConsoleColor.White, false);
            Thread.Sleep(1000);
            #endregion

            #region start
            Thread tChecker = new Thread(new ThreadStart(StartChecker));
            tChecker.Start();
            Console.Read();
            #endregion

        }
        #region Methods

        static void StartChecker() {
            Checker check = new Checker();
            Task.Run(() => check.CheckTokens());
            Console.ReadLine();
        }

        static void Watermark() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            printf(@"


████████╗ ██████╗ ██╗  ██╗███████╗███╗   ██╗    ███████╗███╗   ███╗ █████╗ ███████╗██╗  ██╗███████╗██████╗ 
╚══██╔══╝██╔═══██╗██║ ██╔╝██╔════╝████╗  ██║    ██╔════╝████╗ ████║██╔══██╗██╔════╝██║  ██║██╔════╝██╔══██╗
   ██║   ██║   ██║█████╔╝ █████╗  ██╔██╗ ██║    ███████╗██╔████╔██║███████║███████╗███████║█████╗  ██████╔╝
   ██║   ██║   ██║██╔═██╗ ██╔══╝  ██║╚██╗██║    ╚════██║██║╚██╔╝██║██╔══██║╚════██║██╔══██║██╔══╝  ██╔══██╗
   ██║   ╚██████╔╝██║  ██╗███████╗██║ ╚████║    ███████║██║ ╚═╝ ██║██║  ██║███████║██║  ██║███████╗██║  ██║
   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝    ╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
                                                                                                           ");
            Console.ForegroundColor = ConsoleColor.White;
            string versione = "1.0";
            Console.WriteLine("Version: " + " " + versione);

        }
        public static DateTime UnixTimeToDateTime(long unixtime) {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }
        #endregion
    }
}
