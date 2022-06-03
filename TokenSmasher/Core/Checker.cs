using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using TokenSmasher.Helpers;
using TokenSmasher.Checker.Models;

namespace TokenSmasher.Core {
    internal class Checker {

        private static WebClient _webClient;

        public static void Init() {
            _webClient = new WebClient();
            _webClient.Headers.Add(@"Content-Type", @"application/json");

            Checker check = new Checker();
            Task.Run(() => check.CheckTokens());
            Console.ReadLine();
        }

        /*  
         * For each token in our list we send a request to Discord's API and then check for alive, dead or locked tokens.
         * Then we can check if we're currently rate-limited; API V9 is currently the fastest version they made, and hardly gets you rate-limited even while proxyless.
         */

        public void CheckTokens() {
            ConsoleHelpers.PrintWithColor(@"info", @"starting checker...", Color.FromArgb(138, 142, 255), true);
            Thread.Sleep(2000);
            Console.Clear();

            foreach (var token in TokenModel.Tokens) {
                try {
                    _webClient.Headers.Add(HttpRequestHeader.Authorization, token);
                    _webClient.DownloadString("https://discord.com/api/v9/users/@me/guilds");

                    TokenModel.WorkingTokens.Add(token);
                    ConsoleHelpers.PrintWithColor(@"alive", $@"> {token}", Color.FromArgb(138, 142, 255), true);
                } catch (WebException e) {
                    HttpWebResponse response = (HttpWebResponse) e.Response;

                    switch (response.StatusCode) {
                        case HttpStatusCode.Unauthorized:
                            ConsoleHelpers.PrintWithColor(@"dead", $@"> {token}", Color.FromArgb(232, 44, 79), true);
                            TokenModel.DeadTokens.Add(token);
                            break;

                        case HttpStatusCode.Forbidden:
                            ConsoleHelpers.PrintWithColor(@"locked", $@"> {token}", Color.FromArgb(232, 44, 79), true);
                            TokenModel.LockedTokens.Add(token);
                            break;

                        case (HttpStatusCode) 429:
                            ConsoleHelpers.PrintWithColor(@"info", $@"rate limited", Color.FromArgb(232, 44, 79), true);
                            break;
                    }
                }
            }

            var date = DateTime.Now.ToString(@"dd MMM HH-mm-ss");

            if (!Directory.Exists(@"TokenSmasher")) 
                Directory.CreateDirectory(@"TokenSmasher");

            if (!Directory.Exists($"TokenSmasher\\{date}")) 
                Directory.CreateDirectory($"TokenSmasher\\{date}");

            File.WriteAllLines($"TokenSmasher\\{date}\\Working.txt", TokenModel.WorkingTokens);

            ConsoleHelpers.PrintWithColor(@"info", $@"checked each provided token.", Color.FromArgb(138, 142, 255), true);

            ConsoleHelpers.PrintWithColor(@"info", $@"total tokens: {TokenModel.Tokens.Count}", Color.FromArgb(138, 142, 255), true);
            ConsoleHelpers.PrintWithColor(@"info", $@"alive tokens: {TokenModel.WorkingTokens.Count}", Color.FromArgb(138, 142, 255), true);
            ConsoleHelpers.PrintWithColor(@"info", $@"dead tokens: {TokenModel.DeadTokens.Count}", Color.FromArgb(138, 142, 255), true);
            ConsoleHelpers.PrintWithColor(@"info", $@"locked tokens: {TokenModel.LockedTokens.Count}", Color.FromArgb(138, 142, 255), true);
        }
    }
}