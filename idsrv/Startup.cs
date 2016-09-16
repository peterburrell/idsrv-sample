using System.Collections.Generic;
using IdentityServer;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            var options = new IdentityServerOptions
            {
                SiteName = "Peter's Identity Server",
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(GetClients())
                    .UseInMemoryScopes(GetScopes())
                    .UseInMemoryUsers(GetUsers()),

                RequireSsl = false,
            };

            app.UseIdentityServer(options);
        }

        private static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = "peterb",
                    Password = "p@ssword"
                }
            };
        }

        private static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "web-api"
                },
                new Scope
                {
                    Name = "web-app"
                }
            };
        }

        private static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //new Client
                //{
                //    ClientName = "Console Application",
                //    ClientId = "console-app",
                //    Enabled = true,
                //    AccessTokenType = AccessTokenType.Reference,
                //    Flow = Flows.ClientCredentials,
                //    ClientSecrets = new List<Secret>
                //    {
                //        new Secret("83AA132C-8BFA-4439-85B5-33A1E2961F6E".Sha256())
                //    },
                //    AllowedScopes = new List<string>
                //    {
                //        "web-api"
                //    }
                //},
                new Client
                {
                    ClientName = "Console Application",
                    ClientId = "console-app",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Reference,
                    Flow = Flows.ResourceOwner,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("83AA132C-8BFA-4439-85B5-33A1E2961F6E".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "web-api"
                    }
                }
            };
        }
    }
}
