using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Vars;
using EasyConsoleClass;

namespace TokensChecker {
    internal class Checker {

        WebClient wc = new WebClient ( );

        public static void StartChecker ( ) {
            Checker check = new Checker ( );
            Task.Run ( ( ) => check.CheckTokens ( ) );
            Console.ReadLine ( );
        }

        /*  
         * For each token in our list we send a request to Discord's API and then check for alive, dead or locked tokens.
         * Then we can check if we're currently rate-limited; API V9 is currently the fastest version they made, and hardly gets you rate-limited even while proxyless.
         */

        public void CheckTokens ( ) {
            EasyConsole.WriteLine ( " [info] > starting tokens checker...", ConsoleColor.DarkYellow, false );
            Thread.Sleep ( 1000 );
            Console.Clear ( );

            foreach ( string token in Variables.Tokens ) {
                
                try {

                    wc.Headers.Add ( "Content-Type", "application/json" );
                    wc.Headers.Add ( HttpRequestHeader.Authorization, token );
                    wc.DownloadString ( "https://discord.com/api/v9/users/@me/guilds" );

                    Variables.WorkingTokens.Add ( token );
                    EasyConsole.WriteLine ( $" [info] > alive token - {token}", ConsoleColor.Green, false );

                } catch ( WebException e ) {

                    HttpWebResponse response = ( HttpWebResponse ) e.Response;

                    if ( response.StatusCode == HttpStatusCode.Unauthorized ) {
                        Variables.NonWorkingTokens.Add ( token );
                        EasyConsole.WriteLine ( $" [info] > dead token - {token}", ConsoleColor.Red, false );
                    } else if ( response.StatusCode == HttpStatusCode.Forbidden ) {
                        Variables.LockedsTokens.Add ( token );
                        EasyConsole.WriteLine ( $" [info] > locked token - {token}", ConsoleColor.Yellow, false );
                    } else if ( response.StatusCode == ( HttpStatusCode ) 429 ) {
                        EasyConsole.WriteLine ( $" [info] > rate limited", ConsoleColor.Blue, false );
                    }

                }
                Console.Title = $"token smasher | tokens: {Variables.Tokens.Count} | alive: {Variables.WorkingTokens.Count} | dead: {Variables.NonWorkingTokens.Count} | locked: {Variables.LockedsTokens.Count}";
            }

            string date = DateTime.Now.ToString ( "dd MMM HH-mm-ss" );

            if ( !Directory.Exists ( "TokenSmasher" ) )  Directory.CreateDirectory ( "TokenSmasher" );

            if ( !Directory.Exists ( $"TokenSmasher\\{date}" ) )  Directory.CreateDirectory ( $"TokenSmasher\\{date}" );

            File.WriteAllLines ( $"TokenSmasher\\{date}\\TokensChecked.txt", Variables.WorkingTokens );

            EasyConsole.WriteLine ( $" [info] checked all {Variables.Tokens.Count} tokens! saved alive tokens @ {Directory.GetCurrentDirectory ( )}\\TokenSmasher\\{date}", ConsoleColor.White, false );
        }
    }
}