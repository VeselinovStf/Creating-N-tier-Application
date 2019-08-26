using PluralSightBook.Core.Interfaces;

namespace PluralSightBook.Core.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository profileService;

        public ProfileService(IProfileRepository profileService)
        {
            this.profileService = profileService;
        }
    }
}