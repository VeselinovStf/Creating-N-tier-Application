using PluralSightBook.Core.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> ShowFriendsReport(string userEmail)
        {
            Console.WriteLine("All Friends of {0}:", userEmail);
            //var user = _userService.GetUserByEmail(userEmail);

            var friends = await _friendsService.ListFriendsOf(new Guid("70630cb0-edff-461f-87f3-409a6619dc16"));

            StringBuilder sb = new StringBuilder();
            foreach (var friend in friends)
            {
                sb.Append("Id: " + friend.Id);
                sb.Append(System.Environment.NewLine);
                sb.Append("Email: " + friend.Email);
                sb.Append(System.Environment.NewLine);
                sb.Append("-----------------------------");
            }
            return sb.ToString();
        }
    }
}