using PluralSightBook.Core.Identity;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Core.Interfaces
{
    public interface IProfileRepository
    {
        Task<PluralSightBookIdentityUser> GetUser(Guid userId);

        Task EditProfile(string modelFavoriteAuthor, Guid userId);
    }
}