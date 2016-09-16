using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using IdentityModel.Client;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("username:");
            var username = Console.ReadLine();

            Console.WriteLine("password:");
            var password = ReadPassword();

            var token = GetToken(username, password);

            var response = CallApi(token);

            Console.WriteLine("\napi response: ");
            Console.WriteLine(response);

            Console.WriteLine("\n\npress any key to exit...");
            Console.ReadKey();
        }

        private static string CallApi(TokenResponse token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token.AccessToken);
            return client.GetStringAsync("http://localhost:5001/api/users").Result;
        }

        private static TokenResponse GetToken(string username, string password)
        {
            var client = new TokenClient(
                "http://localhost:5000/connect/token",
                "console-app",
                "83AA132C-8BFA-4439-85B5-33A1E2961F6E");

            //return client.RequestClientCredentialsAsync("web-api").Result;            
            return client.RequestResourceOwnerPasswordAsync(username, password, "web-api").Result;
        }

        //http://stackoverflow.com/a/7049688/59849
        private static string ReadPassword(char mask='*')
        {
            const int ENTER = 13, BACKSP = 8, CTRLBACKSP = 127;
            int[] FILTERED = { 0, 27, 9, 10 /*, 32 space, if you care */ }; // const

            var pass = new Stack<char>();
            char chr = (char)0;

            while ((chr = System.Console.ReadKey(true).KeyChar) != ENTER)
            {
                if (chr == BACKSP)
                {
                    if (pass.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        pass.Pop();
                    }
                }
                else if (chr == CTRLBACKSP)
                {
                    while (pass.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        pass.Pop();
                    }
                }
                else if (FILTERED.Count(x => chr == x) > 0) { }
                else
                {
                    pass.Push((char)chr);
                    System.Console.Write(mask);
                }
            }

            Console.WriteLine();

            return new string(pass.Reverse().ToArray());
        }
    }
}
