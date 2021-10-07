using System;
using System.IO;
using System.Threading;

using Vars;
using TokensChecker;
using EasyConsoleClass;

namespace TokenSmasher {
    class Program {

        static void Main(string[] args) {

            #region Initialize

            int tokens = 0;
            Console.Title = $"token smasher | tokens: {tokens} | alive: 0 | dead: 0 | locked: 0 ";
            EasyConsole.Watermark ( );

            #endregion

            #region Load Tokens

            if ( !File.Exists ( "tokens.txt" ) ) {
                File.Create ( "tokens.txt" );
                EasyConsole.WriteLine ( " [info] > please put tokens into 'tokens.txt' file!", ConsoleColor.Red, true );
                Environment.Exit ( -1 );
            }

            foreach ( string token in File.ReadAllLines ( "tokens.txt" ) ) {
                tokens++;
                Variables.Tokens.Add ( token );
            }

            if ( tokens <= 0 ) {
                EasyConsole.WriteLine ( $" [info] > tokens.txt is empty!", ConsoleColor.Red, true );
                Environment.Exit ( -1 );
            }

            Console.Title = $"token smasher | tokens: {tokens} | alive: 0 | dead: 0 | locked: 0 ";

            EasyConsole.WriteLine($" [info] > loaded {tokens} tokens.", ConsoleColor.White, false);
            Thread.Sleep(1000);

            #endregion

            #region Start Checker

            Thread tChecker = new Thread(new ThreadStart( Checker.StartChecker) );
            tChecker.Start();
            Console.Read();

            #endregion

        }
    }
}
