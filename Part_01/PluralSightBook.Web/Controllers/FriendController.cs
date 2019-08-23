using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.BLL;
using PluralSightBook.DLL.Data;
using PluralSightBook.DLL.Data.Models;
using PluralSightBook.DLL.Identity;
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
        private readonly FriendService friendService;

        public FriendController(
            PluralSightBookDbContext context,
            UserManager<PluralSightBookIdentityUser> userManager,
            FriendService friendService)
        {
            this.context = context;
            this.userManager = userManager;
            this.friendService = friendService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var friendsServiceList = await this.friendService.List(User);

                var model = friendsServiceList.Select(f => new FriendsViewModel()
                {
                    Email = f.Email
                });

                return View(model);
            }
            catch
            {
                return View();
            }
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

                currentUser.Friends.Add(new Friend()
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