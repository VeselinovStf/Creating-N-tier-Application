using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.Core.Exceptions;
using PluralSightBook.Core.Identity.Helpers;
using PluralSightBook.Core.Interfaces;
using PluralSightBook.Web.ViewModels.Friend;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PluralSightBook.Web.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendService friendService;
        private readonly IProfileService profileService;

        public FriendController(IFriendService friendService, IProfileService profileService)
        {
            this.friendService = friendService;
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var friendsServiceList = this.friendService.ListFriendsOf(this.User.GetUserId());

                var model = friendsServiceList.Select(f => new FriendsViewModel()
                {
                    Id = f.Id,
                    Email = f.Email
                });

                return View(model);
            }
            catch (EmptyFriendsListException)
            {
                return View();
            }
            catch (Exception)
            {
                //TODO: Log this exception
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
                try
                {
                    var currentUser = await this.profileService.GetProfile(this.User.GetUserId());

                    await this.friendService.AddFriend(this.User.GetUserId(), currentUser.Name, model.Email);
                }
                catch (AddYourSelfAsFriendException ex)
                {
                    ViewData["Error"] = ex.Message;
                    return View();
                }
                catch (AlreadyFriendWithThisUserException ex)
                {
                    ViewData["Error"] = ex.Message;
                    return View();
                }
                catch (NoSuchUserException ex)
                {
                    ViewData["Error"] = ex.Message;
                    return View();
                }
                catch (Exception)
                {
                    //TODO: Log this exception
                    return View();
                }

                return RedirectToAction("List", "Friend");
            }

            return View();
        }

        public async Task<IActionResult> Remove(int friendId)
        {
            try
            {
                this.friendService.DeleteFriend(friendId);
            }
            catch (StringParameterException)
            {
                return RedirectToAction("List", "Friend");
            }
            catch (NoSuchUserToRemoveException)
            {
                return RedirectToAction("List", "Friend");
            }
            catch (Exception)
            {
                //TODO: Log this exception
                return View();
            }

            return RedirectToAction("List", "Friend");
        }
    }
}