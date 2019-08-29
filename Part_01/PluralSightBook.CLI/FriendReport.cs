using PluralSightBook.Core.Interfaces;
using System;
using System.Text;

namespace PluralSightBook.CLI
{
    public class FriendsReport
    {
        private readonly IFriendService _friendsService;

        public FriendsReport(IFriendService friendsService
          )
        {
            this._friendsService = friendsService;
        }

        public string ShowFriendsReport(string userEmail)
        {
            Console.WriteLine("All Friends of {0}:", userEmail);
            //var user = _userService.GetUserByEmail(userEmail);

            var friends = _friendsService.ListFriendsOf(new Guid("70630cb0-edff-461f-87f3-409a6619dc16")).Result;
            StringBuilder sb = new StringBuilder();
            foreach (var friend in friends)
            {
                sb.Append(friend);
                sb.Append(System.Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}