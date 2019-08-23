using Microsoft.AspNetCore.Identity;
using PluralSightBook.BLL.DTOs;
using PluralSightBook.BLL.Exceptions;
using PluralSightBook.DLL.Data;
using PluralSightBook.DLL.Data.Models;
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

        public async Task Add(string modelEmail, ClaimsPrincipal user)
        {
            var currentUser = await
             userManager.GetUserAsync(user);

            if (currentUser.Email == modelEmail)
            {
                throw new AddYourSelfAsFriendException("Can't add your self ass friend");
            }

            var currentUserFriendsList = this.context.Friends
                .Where(f => f.PluralSightBookIdentityUser == currentUser.Id && !f.IsDeleted)
                .ToList();

            if (currentUserFriendsList.FirstOrDefault(f => f.Email == modelEmail) != null)
            {
                throw new AlreadyFriendWithThisUserException("You are already friend with this user");
            }

            var friendToAdd = await
                userManager.FindByEmailAsync(modelEmail);

            if (friendToAdd == null)
            {
                throw new NoSuchUserException("No Such User");
            }

            if (friendToAdd.Friends.FirstOrDefault(f => f.Email == modelEmail) != null)
            {
                throw new AlreadyFriendWithThisUserException("You are already friend with this user");
            }

            currentUser.Friends.Add(new Friend()
            {
                Email = modelEmail,
            });

            await this.context.SaveChangesAsync();
        }
    }
}