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
        public IActionResult List()
        {
            var dbFriends = this.context.Friends.ToList();

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
                var user = await
                    userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ViewData["Error"] = "No Such User";
                    return View();
                }

                user.Friends.Add(new Data.Models.Friend()
                {
                    Email = model.Email,
                });

                await this.context.SaveChangesAsync();

                return RedirectToAction("List", "Friend");
            }

            return View();
        }
    }
}