using Microsoft.AspNetCore.Identity;
using PluralSightBook.BLL.DTOs;
using PluralSightBook.BLL.Exceptions;
using PluralSightBook.DLL.Data;
using PluralSightBook.DLL.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PluralSightBook.BLL
{
    public class FriendService
    {
        private readonly PluralSightBookDbContext context;
        private readonly UserManager<PluralSightBookIdentityUser> userManager;

        public FriendService(
            PluralSightBookDbContext context,
            UserManager<PluralSightBookIdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<FriendsViewModelDTO>> List(ClaimsPrincipal user)
        {
            var currentUser = await
                  userManager.GetUserAsync(user);

            var dbFriends = this.context
                .Friends
                .Where(f => f.PluralSightBookIdentityUser == currentUser.Id && !f.IsDeleted)
                .ToList();

            if (dbFriends == null)
            {
                throw new EmptyFriendsListException("No Friends to list");
            }

            var model = dbFriends.Select(f => new FriendsViewModelDTO()
            {
                Email = f.Email
            }).ToList();

            return model;
        }
    }
}