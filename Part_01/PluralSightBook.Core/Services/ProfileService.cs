using PluralSightBook.Core.DTOs;
using PluralSightBook.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public async Task<ProfileViewModelDTO> Profile(Guid userId)
        {
            var viewModel = new ProfileViewModelDTO();

            var currentUser = await this.profileRepository.GetUser(userId);

            viewModel.Name = currentUser.UserName;
            viewModel.FavoriteAuthor = currentUser.FavoriteAuthor;

            return viewModel;
        }

        public async Task EditProfile(string modelFavoriteAuthor, Guid userId)
        {
            await this.profileRepository.EditProfile(modelFavoriteAuthor, userId);
        }
    }
}