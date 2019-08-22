using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.Web.Data;
using PluralSightBook.Web.Identity;
using PluralSightBook.Web.ViewModels.Friend;
using System.Linq;
using System.Threading.Tasks;

namespace PluralSightBook.Web.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly PluralSightBookDbContext context;
        private readonly UserManager<PluralSightBookIdentityUser> userManager;

        public FriendController(PluralSightBookDbContext context, UserManager<PluralSightBookIdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var currentUser = await
                  userManager.GetUserAsync(User);

            var dbFriends = this.context
                .Friends
                .Where(f => f.PluralSightBookIdentityUser == currentUser.Id);

            if (dbFriends == null)
            {
                return View();
            }

            var model = dbFriends.Select(f => new FriendsViewModel()
            {
                Email = f.Email
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FriendsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var friendToAdd = await
                    userManager.FindByEmailAsync(model.Email);

                if (friendToAdd == null)
                {
                    ViewData["Error"] = "No Such User";
                    return View();
                }

                // TODO: STOP DUPLICATES
                if (friendToAdd.Friends.FirstOrDefault(f => f.Email == model.Email) != null)
                {
                    ViewData["Error"] = "You are already friend with this user";
                    return View();
                }

                friendToAdd.Friends.Add(new Data.Models.Friend()
                {
                    Email = model.Email,
                });

                await this.context.SaveChangesAsync();

                return RedirectToAction("List", "Friend");
            }

            return View();
        }

        public async Task<IActionResult> Remove(string friendEmail)
        {
            return RedirectToAction("List", "Friend");
        }
    }
}