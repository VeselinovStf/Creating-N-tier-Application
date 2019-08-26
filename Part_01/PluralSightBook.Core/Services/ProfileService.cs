using PluralSightBook.Core.DTOs;
using PluralSightBook.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileService;

        public ProfileService(IProfileRepository profileService)
        {
            this.profileService = profileService;
        }

        public async Task<ProfileViewModelDTO> Profile(Guid userId)
        {
            var viewModel = new ProfileViewModelDTO();

            var currentUser = await this.profileService.GetUser(userId);

            viewModel.Name = currentUser.UserName;
            viewModel.FavoriteAuthor = currentUser.FavoriteAuthor;

            return viewModel;
        }

        public async Task EditProfile(string modelFavoriteAuthor, Guid userId)
        {
            await this.profileService.EditProfile(modelFavoriteAuthor, userId);
        }

        public async Task<ProfileViewModelDTO> GetProfile(Guid userId)
        {
            var viewModel = new ProfileViewModelDTO();

            var currentUser = await this.profileService.GetUser(userId);

            viewModel.Name = currentUser.UserName;

            return viewModel;
        }
    }
}