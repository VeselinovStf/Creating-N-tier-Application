﻿using PluralSightBook.Core.Interfaces;
using PluralSightBook.Core.Models;
using PluralSightBook.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralSightBook.Infrastructure.Repositories
{
    public class EfFriendRepository : IFriendRepository
    {
        private readonly PluralSightBookDbContext context;

        public EfFriendRepository(PluralSightBookDbContext context)
        {
            this.context = context;
        }

        public void Create(string emailAddress)
        {
            var newFriend = new Friend();

            newFriend.Email = emailAddress;
            context.Friends.Add(newFriend);
            context.SaveChanges();
        }

        public void Delete(int friendId)
        {
            var friendToDelete = context.Friends.FirstOrDefault(f => f.Id == friendId && !f.IsDeleted);
            friendToDelete.IsDeleted = true;
            context.SaveChanges();
        }

        public IEnumerable<Friend> ListFriendsOfUser(Guid userId)
        {
            return context.Friends
                .Where(f => f.Id.ToString() == userId.ToString())
                .Select(f => new Friend()
                {
                    Id = f.Id,
                    Email = f.Email
                });
        }
    }
}