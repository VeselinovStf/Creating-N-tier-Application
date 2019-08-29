using PluralSightBook.CLI.WebAPIService;
using PluralSightBook.Core.Interfaces;
using StructureMap;
using System.Net.Http;

namespace PluralSightBook.CLI
{
    public class StructureMapBootStrap
    {
        public static Container Configure()
        {
            var container = new Container(c =>
            {
                c.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                    x.AssemblyContainingType<IFriendService>(); // Core
                });

                // Configure WebAPI
                c.For<HttpClient>().Singleton().Use(() => ApiConfig.GetClient());

                // Configure Implementations
                // c.For<ISendEmail>().Use<DebugEmailSender>();
                // c.For<IUserService>().Use<WebApiUserService>();
                c.For<IFriendService>().Use<WebApiFriendsService>();
            });

            return container;
        }
    }
}