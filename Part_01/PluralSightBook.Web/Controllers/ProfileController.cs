using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.Web.Identity;
using PluralSightBook.Web.ViewModels.Profile;
using System.Threading.Tasks;

namespace PluralSightBook.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<PluralSightBookIdentityUser> userManager;

        public ProfileController(UserManager<PluralSightBookIdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Profile()
        {
            var viewModel = new ProfileViewModel();

            viewModel.Name = userManager.GetUserName(User);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var viewModel = new ProfileViewModel();

            viewModel.Name = userManager.GetUserName(User);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                //TODO: The process of saving the favorite author is not implemented
                user.FavoriteAuthor = model.FavoriteAuthor;
                user.

                return RedirectToAction("Profile");
            }

            return View();
        }
    }
}