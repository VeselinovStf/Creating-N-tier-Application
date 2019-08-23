using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PluralSightBook.BLL;
using PluralSightBook.BLL.Exceptions;
using PluralSightBook.Web.ViewModels.Friend;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PluralSightBook.Web.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly FriendService friendService;

        public FriendController(FriendService friendService)
        {
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
                    await this.friendService.Add(model.Email, User);
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

        public async Task<IActionResult> Remove(string friendEmail)
        {
            try
            {
                await this.friendService.Remove(friendEmail, User);
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