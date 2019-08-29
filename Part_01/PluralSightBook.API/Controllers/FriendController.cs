using Microsoft.AspNetCore.Mvc;
using PluralSightBook.API.ViewModels;
using PluralSightBook.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralSightBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService friendService;

        public FriendController(IFriendService friendService)
        {
            this.friendService = friendService;
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<FriendViewModel>> List(Guid userId)
        {
            try
            {
                var serviceCall = this.friendService.ListFriendsOf(userId);

                var model = serviceCall.Result.Select(f => new FriendViewModel()
                {
                    Id = f.Id,
                    Email = f.Email
                });

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}