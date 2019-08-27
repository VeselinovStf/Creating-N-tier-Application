using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PluralSightBook.Core.Identity;
using PluralSightBook.Core.Interfaces;
using PluralSightBook.Core.Services;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Test.Core
{
    [TestClass]
    public class ProfileService_Should
    {
        [TestMethod]
        public async Task ReturnProfile_When_Correct_UserIdIsPassed()
        {
            Guid userId = new Guid();
            var userName = "TestUserName";
            var userFavoriteAuthor = "TestUserFavoriteAuthor";

            PluralSightBookIdentityUser user = new PluralSightBookIdentityUser()
            {
                Id = userId.ToString(),
                UserName = userName,
                FavoriteAuthor = userFavoriteAuthor
            };

            var profileRepositoryMoq = new Mock<IProfileRepository>();
            profileRepositoryMoq.Setup(s => s.GetUser(userId)).ReturnsAsync(user);

            var profileService = new ProfileService(profileRepositoryMoq.Object);

            var profileServiceCall = await profileService.Profile(userId);

            Assert.AreEqual(userName, profileServiceCall.Name);
            Assert.AreEqual(userFavoriteAuthor, profileServiceCall.FavoriteAuthor);
        }
    }
}