using PluralSightBook.Core.DTOs;
using PluralSightBook.Core.Exceptions;
using PluralSightBook.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluralSightBook.Core.Services
{
    public class FriendsService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IProfileRepository _profileRepository;

        public FriendsService(IFriendRepository friendRepository, IProfileRepository profileRepository
           )
        {
            _friendRepository = friendRepository;
            _profileRepository = profileRepository;
        }

        public async Task AddFriend(Guid currentUserId,
            string currentUserEmail,
            string friendEmail)
        {
            if (currentUserEmail == friendEmail)
            {
                throw new AddYourSelfAsFriendException("Can't add your self ass friend");
            }

            var currentUserFriendsList = _friendRepository.ListFriendsOfUser(currentUserId);

            if (currentUserFriendsList.FirstOrDefault(f => f.Email == friendEmail) != null)
            {
                throw new AlreadyFriendWithThisUserException("You are already friend with this user");
            }

            var friendToAdd = await this._profileRepository.GetUser(currentUserId);

            if (friendToAdd == null)
            {
                throw new NoSuchUserException("No Such User");
            }

            if (friendToAdd.Friends.FirstOrDefault(f => f.Email == currentUserEmail) != null)
            {
                throw new AlreadyFriendWithThisUserException("You are already friend with this user");
            }

            _friendRepository.Create(friendEmail, currentUserId);
        }

        public void DeleteFriend(int friendId)
        {
            _friendRepository.Delete(friendId);
        }

        public IEnumerable<FriendsViewModelDTO> ListFriendsOf(Guid userId)
        {
            var dbFriends = _friendRepository.ListFriendsOfUser(userId);

            if (dbFriends == null)
            {
                throw new EmptyFriendsListException("No Friends to list");
            }

            var model = dbFriends.Select(f => new FriendsViewModelDTO()
            {
                Id = f.Id,
                Email = f.Email
            }).ToList();

            return model;
        }
    }
}