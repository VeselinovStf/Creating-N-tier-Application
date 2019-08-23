using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.DLL.Data;
using PluralSightBook.DLL.Identity;
using PluralSightBook.Web.ViewModels.Profile;
using System.Threading.Tasks;

namespace PluralSightBook.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<PluralSightBookIdentityUser> userManager;
        private readonly PluralSightBookDbContext context;

        public ProfileController(
            UserManager<PluralSightBookIdentityUser> userManager,
            PluralSightBookDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Profile()
        {
            var viewModel = new ProfileViewModel();

            var user = await userManager.GetUserAsync(User);

            viewModel.Name = user.UserName;
            viewModel.FavoriteAuthor = user.FavoriteAuthor;

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

                user.FavoriteAuthor = model.FavoriteAuthor;
                await this.context.SaveChangesAsync();

                return RedirectToAction("Profile", "Profile", model);
            }

            return View();
        }
    }
}