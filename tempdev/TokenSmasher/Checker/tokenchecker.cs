using EasyConsoleClass;
using System;
using System.IO;
using System.Net;
using Variabili;
using System.Threading;
using System.Text.RegularExpressions;

namespace TokensChecker {
    class Checker {
        public void CheckTokens() {
            EasyConsole.WriteLine("Starting Tokens Checker.....", ConsoleColor.DarkYellow, false);
            Thread.Sleep(1000);
            Console.Clear();
            foreach (string token in Variables.Tokens) {
                try {
                    using (var wc = new WebClient()) {

                        wc.Headers.Add("Content-Type", "application/json");
                        wc.Headers.Add(HttpRequestHeader.Authorization, token);
                        wc.DownloadString("https://discord.com/api/v9/users/@me/guilds");
                        Variables.WorkingTokens.Add(token);
                        Console.Title = $"Token Smasher | Tokens: {Variables.Tokens.Count} | Live: {Variables.WorkingTokens.Count} | Died: {Variables.NonWorkingTokens.Count} | Locked: {Variables.LockedsTokens.Count}";
                        EasyConsole.WriteLine($"[+] Live Token - {token}", ConsoleColor.Green, false);
                    }
                }
                catch (WebException e) {
                    HttpWebResponse response = (HttpWebResponse)e.Response;

                    if (response.StatusCode == HttpStatusCode.Unauthorized) // 401 
                    {
                        Variables.NonWorkingTokens.Add(token);
                        Console.Title = $"Token Smasher | Tokens: {Variables.Tokens.Count} | Live: {Variables.WorkingTokens.Count} | Died: {Variables.NonWorkingTokens.Count} | Locked: {Variables.LockedsTokens.Count}";
                        EasyConsole.WriteLine($"[-] Died Token - {token}", ConsoleColor.Red, false);
                    }
                    else if (response.StatusCode == HttpStatusCode.Forbidden) //403
                    {
                        Variables.LockedsTokens.Add(token);
                        Console.Title = $"Token Smasher | Tokens: {Variables.Tokens.Count} | Live: {Variables.WorkingTokens.Count} | Died: {Variables.NonWorkingTokens.Count} | Locked: {Variables.LockedsTokens.Count}";
                        EasyConsole.WriteLine($"[-] Locked Token - {token}", ConsoleColor.Yellow, false);
                    }
                    else if (response.StatusCode == (HttpStatusCode)429) {
                        Console.Title = $"Token Smasher | Tokens: {Variables.Tokens.Count} | Live: {Variables.WorkingTokens.Count} | Died: {Variables.NonWorkingTokens.Count} | Locked: {Variables.LockedsTokens.Count}";
                        EasyConsole.WriteLine($"[-] RATE LIMITED", ConsoleColor.Blue, false);
                    }
                }
            }

            string date = DateTime.Now.ToString("dd MMM HH-mm-ss");

            if (!Directory.Exists("TokenSmasher")) {
                Directory.CreateDirectory("TokenSmasher");
            }

            if (!Directory.Exists($"TokenSmasher\\{date}")) {
                Directory.CreateDirectory($"TokenSmasher\\{date}");
            }

            File.WriteAllLines($"TokenSmasher\\{date}\\TokensChecked.txt", Variables.WorkingTokens);


            EasyConsole.WriteLine($"Checked all {Variables.Tokens.Count} Tokens! Saved Working to {Directory.GetCurrentDirectory()}\\TokenSmasher\\{date}", ConsoleColor.White, false);
        }
    }
}
