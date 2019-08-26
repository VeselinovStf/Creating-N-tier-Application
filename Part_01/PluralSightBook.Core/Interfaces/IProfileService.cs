using PluralSightBook.Core.DTOs;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Core.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileViewModelDTO> Profile(Guid userId);

        Task EditProfile(string modelFavoriteAuthor, Guid userId);

        Task<ProfileViewModelDTO> GetProfile(Guid userId);
    }
}