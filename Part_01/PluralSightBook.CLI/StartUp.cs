using System;

namespace PluralSightBook.CLI
{
    internal class StartUp
    {
        private static void Main(string[] args)
        {
            string userEmail = "test@test.com";

            var friendsReport = StructureMapBootStrap.Configure().GetInstance<FriendsReport>();

            Console.Write(friendsReport.ShowFriendsReport(userEmail));
            Console.WriteLine();
            Console.WriteLine("Press any key to close...");
            Console.ReadLine();
        }
    }
}