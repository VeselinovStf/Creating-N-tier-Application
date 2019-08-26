using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.Core.Identity.Helpers;
using PluralSightBook.Core.Interfaces;
using PluralSightBook.Web.ViewModels.Profile;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task<IActionResult> Profile()
        {
            var profileServiceModel = await this.profileService.Profile(this.User.GetUserId());

            var viewModel = new ProfileViewModel()
            {
                Name = profileServiceModel.Name,
                FavoriteAuthor = profileServiceModel.FavoriteAuthor
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                var serviceModel = await this.profileService.GetProfile(this.User.GetUserId());

                var viewModel = new ProfileViewModel()
                {
                    Name = serviceModel.Name
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                //TODO: Log this exception, and manage view display
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.profileService.EditProfile(model.FavoriteAuthor, this.User.GetUserId());

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