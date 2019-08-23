using Microsoft.AspNetCore.Identity;
using PluralSightBook.BLL.DTOs;
using PluralSightBook.DLL.Data;
using PluralSightBook.DLL.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PluralSightBook.BLL
{
    public class ProfileService
    {
        private readonly UserManager<PluralSightBookIdentityUser> userManager;
        private readonly PluralSightBookDbContext context;

        public ProfileService(
            UserManager<PluralSightBookIdentityUser> userManager,
            PluralSightBookDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<ProfileViewModelDTO> Profile(ClaimsPrincipal user)
        {
            var viewModel = new ProfileViewModelDTO();

            var currentUser = await userManager.GetUserAsync(user);

            viewModel.Name = currentUser.UserName;
            viewModel.FavoriteAuthor = currentUser.FavoriteAuthor;

            return viewModel;
        }
    }
}