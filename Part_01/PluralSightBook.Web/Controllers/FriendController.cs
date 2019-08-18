using Microsoft.AspNetCore.Mvc;
using PluralSightBook.Web.Data;
using PluralSightBook.Web.ViewModels.Friend;
using System.Linq;

namespace PluralSightBook.Web.Controllers
{
    public class FriendController : Controller
    {
        private readonly PluralSightBookDbContext context;

        public FriendController(PluralSightBookDbContext context)
        {
            this.context = context;
        }

        public IActionResult List()
        {
            var dbFriends = this.context.Friends.ToList();

            var model = dbFriends.Select(f => new FriendsViewModel()
            {
                Email = f.Email
            });

            return View(model);
        }
    }
}