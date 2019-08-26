using PluralSightBook.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PluralSightBook.Core.Interfaces
{
    public interface IFriendService
    {
        Task AddFriend(Guid currentUserId,
            string currentUserEmail,
            string friendEmail);

        void DeleteFriend(int friendId);

        IEnumerable<FriendsViewModelDTO> ListFriendsOf(Guid userId);
    }
}