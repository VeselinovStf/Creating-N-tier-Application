using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PluralSightBook.Core.Exceptions;
using PluralSightBook.Core.Identity;
using PluralSightBook.Core.Interfaces;
using PluralSightBook.Core.Models;
using PluralSightBook.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluralSightBook.Test.Core
{
    [TestClass]
    public class FriendsService_Should
    {
        [TestMethod]
        public async Task AddFriend_When_CorrectParamettersArePassed()
        {
            Guid currentUserId = Guid.NewGuid();
            string currentUserEmail = "currentUser@mail.com";
            string friendEmail = "friend@mail.com";

            var userName = "TestUserName";
            var userFavoriteAuthor = "TestUserFavoriteAuthor";

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            friendsRepositoryMock.Setup(f => f.ListFriendsOfUser(currentUserId)).Returns(new List<Friend>());

            PluralSightBookIdentityUser user = new PluralSightBookIdentityUser()
            {
                Id = currentUserId.ToString(),
                UserName = userName,
                FavoriteAuthor = userFavoriteAuthor
            };

            var profileRepositoryMock = new Mock<IProfileRepository>();
            profileRepositoryMock.Setup(f => f.GetUser(currentUserId)).ReturnsAsync(user);

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            await friendService.AddFriend(currentUserId, currentUserEmail, friendEmail);

            friendsRepositoryMock.Verify(f => f.Create(friendEmail, currentUserId), Times.Once);
        }

        [TestMethod]
        public async Task Throw_AddYourSelfAsFriendException_WhenAddYourSelfAsFriend()
        {
            Guid currentUserId = Guid.NewGuid();
            string currentUserEmail = "currentUser@mail.com";
            string friendEmail = "currentUser@mail.com";

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            var profileRepositoryMock = new Mock<IProfileRepository>();

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            await Assert.ThrowsExceptionAsync<AddYourSelfAsFriendException>(() => friendService.AddFriend(currentUserId, currentUserEmail, friendEmail));
        }

        [TestMethod]
        public async Task Throw_AlreadyFriendWithThisUserException_WhenAddExistingFriend()
        {
            Guid currentUserId = Guid.NewGuid();
            string currentUserEmail = "currentUser@mail.com";
            string friendEmail = "friend@mail.com";

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            friendsRepositoryMock.Setup(f => f.ListFriendsOfUser(currentUserId)).Returns(new List<Friend>()
            {
                 new Friend()
                 {
                      Email = friendEmail
                 }
            });

            var profileRepositoryMock = new Mock<IProfileRepository>();

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            await Assert.ThrowsExceptionAsync<AlreadyFriendWithThisUserException>(() => friendService.AddFriend(currentUserId, currentUserEmail, friendEmail));
        }

        [TestMethod]
        public async Task Throw_NoSuchUserException_WhenAddFriendCantFindUserToAddFriend()
        {
            Guid currentUserId = Guid.NewGuid();
            string currentUserEmail = "currentUser@mail.com";
            string friendEmail = "friend@mail.com";

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            friendsRepositoryMock.Setup(f => f.ListFriendsOfUser(currentUserId)).Returns(new List<Friend>());

            var profileRepositoryMock = new Mock<IProfileRepository>();
            profileRepositoryMock.Setup(f => f.GetUser(currentUserId)).ReturnsAsync(default(PluralSightBookIdentityUser));

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            await Assert.ThrowsExceptionAsync<NoSuchUserException>(() => friendService.AddFriend(currentUserId, currentUserEmail, friendEmail));
        }

        [TestMethod]
        public async Task Throw_AlreadyFriendWithThisUserException_WhenAddFriendTryToAddAlreadyFriend()
        {
            Guid currentUserId = Guid.NewGuid();
            string currentUserEmail = "currentUser@mail.com";
            string friendEmail = "friend@mail.com";

            var userName = "TestUserName";
            var userFavoriteAuthor = "TestUserFavoriteAuthor";

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            friendsRepositoryMock.Setup(f => f.ListFriendsOfUser(currentUserId)).Returns(new List<Friend>());

            PluralSightBookIdentityUser user = new PluralSightBookIdentityUser()
            {
                Id = currentUserId.ToString(),
                UserName = userName,
                FavoriteAuthor = userFavoriteAuthor
            };

            user.Friends.Add(new Friend()
            {
                Email = currentUserEmail
            });

            var profileRepositoryMock = new Mock<IProfileRepository>();
            profileRepositoryMock.Setup(f => f.GetUser(currentUserId)).ReturnsAsync(user);

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            await Assert.ThrowsExceptionAsync<AlreadyFriendWithThisUserException>(() => friendService.AddFriend(currentUserId, currentUserEmail, friendEmail));
        }

        [TestMethod]
        public void DeleteFriend_ShouldDelete()
        {
            int userIdToDelete = 12345;

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            var profileRepositoryMock = new Mock<IProfileRepository>();

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            friendService.DeleteFriend(userIdToDelete);

            friendsRepositoryMock.Verify(f => f.Delete(userIdToDelete), Times.Once);
        }

        [TestMethod]
        public void ListFriendsOf_WhenCorrectParametterIsPassed()
        {
            Guid currentUserId = Guid.NewGuid();
            int friendId = 12345;
            string friendEmail = "friend@Mail.com";

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            friendsRepositoryMock.Setup(f => f.ListFriendsOfUser(currentUserId)).Returns(new List<Friend>()
            {
                 new Friend()
                 {
                     Id = friendId,
                      Email = friendEmail
                 }
            });

            var profileRepositoryMock = new Mock<IProfileRepository>();

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            var listOfFriends = friendService.ListFriendsOf(currentUserId).Result.ToArray();

            Assert.AreEqual(1, listOfFriends.Length);
            Assert.AreEqual(friendId, listOfFriends[0].Id);
            Assert.AreEqual(friendEmail, listOfFriends[0].Email);
        }

        [TestMethod]
        public void Throw_EmptyFriendsListException_WhenNoFriendsToList()
        {
            Guid currentUserId = Guid.NewGuid();
            int friendId = 12345;

            var friendsRepositoryMock = new Mock<IFriendRepository>();
            friendsRepositoryMock.Setup(f => f.ListFriendsOfUser(currentUserId)).Returns(It.IsAny<List<Friend>>);

            var profileRepositoryMock = new Mock<IProfileRepository>();

            var friendService = new FriendsService(friendsRepositoryMock.Object, profileRepositoryMock.Object);

            Assert.ThrowsException<EmptyFriendsListException>(() => friendService.ListFriendsOf(currentUserId));
        }
    }
}