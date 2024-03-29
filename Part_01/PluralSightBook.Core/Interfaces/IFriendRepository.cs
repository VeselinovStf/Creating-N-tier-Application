﻿using PluralSightBook.Core.Models;
using System;
using System.Collections.Generic;

namespace PluralSightBook.Core.Interfaces
{
    public interface IFriendRepository
    {
        void Create(string emailAddress, Guid currentUserId);

        void Delete(int friendId);

        IEnumerable<Friend> ListFriendsOfUser(Guid userId);
    }
}