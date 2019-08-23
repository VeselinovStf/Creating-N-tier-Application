using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.BLL;
using PluralSightBook.DLL.Data;
using PluralSightBook.DLL.Identity;
using PluralSightBook.Web.ViewModels.Profile;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<PluralSightBookIdentityUser> userManager;
        private readonly PluralSightBookDbContext context;
        private readonly ProfileService profileService;

        public ProfileController(
            UserManager<PluralSightBookIdentityUser> userManager,
            PluralSightBookDbContext context,
            ProfileService profileService)
        {
            this.userManager = userManager;
            this.context = context;
            this.profileService = profileService;
        }

        public async Task<IActionResult> Profile()
        {
            var profileServiceModel = await this.profileService.Profile(User);

            var viewModel = new ProfileViewModel()
            {
                Name = profileServiceModel.Name,
                FavoriteAuthor = profileServiceModel.FavoriteAuthor
            };

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
                try
                {
                    await this.profileService.EditProfile(model.FavoriteAuthor, User);

                    return RedirectToAction("Profile", "Profile", model);
                }
                catch (Exception)
                {
                    //TODO: Log this exception
                    return View();
                }
            }

            return View();
        }
    }
}