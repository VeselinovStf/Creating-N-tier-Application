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
                .Where(f => f.PluralSightBookIdentityUser == currentUser.Id && !f.IsDeleted);

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
                var currentUser = await
                 userManager.GetUserAsync(User);

                if (currentUser.Email == model.Email)
                {
                    ViewData["Error"] = "Can't add your self ass friend";
                    return View();
                }

                var currentUserFriendsList = this.context.Friends
                    .Where(f => f.PluralSightBookIdentityUser == currentUser.Id && !f.IsDeleted)
                    .ToList();

                if (currentUserFriendsList.FirstOrDefault(f => f.Email == model.Email) != null)
                {
                    ViewData["Error"] = "You are already friend with this user";
                    return View();
                }

                var friendToAdd = await
                    userManager.FindByEmailAsync(model.Email);

                if (friendToAdd == null)
                {
                    ViewData["Error"] = "No Such User";
                    return View();
                }

                if (friendToAdd.Friends.FirstOrDefault(f => f.Email == model.Email) != null)
                {
                    ViewData["Error"] = "You are already friend with this user";
                    return View();
                }

                currentUser.Friends.Add(new Data.Models.Friend()
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
            if (string.IsNullOrWhiteSpace(friendEmail))
            {
                return RedirectToAction("List", "Friend");
            }

            var currentUser = await
                userManager.GetUserAsync(User);

            var currentUserFriendsList = this.context.Friends
                   .Where(f => f.PluralSightBookIdentityUser == currentUser.Id && !f.IsDeleted)
                   .ToList();

            var userToRemove = currentUserFriendsList.FirstOrDefault(f => f.Email == friendEmail);

            if (userToRemove == null)
            {
                return RedirectToAction("List", "Friend");
            }

            userToRemove.IsDeleted = true;

            await this.context.SaveChangesAsync();

            return RedirectToAction("List", "Friend");
        }
    }
}