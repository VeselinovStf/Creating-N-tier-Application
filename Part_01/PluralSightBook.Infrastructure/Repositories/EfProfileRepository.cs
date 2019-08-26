using Microsoft.AspNetCore.Identity;
using PluralSightBook.Core.Identity;
using PluralSightBook.Core.Interfaces;
using PluralSightBook.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace PluralSightBook.Infrastructure.Repositories
{
    public class EfProfileRepository : IProfileRepository
    {
        private readonly PluralSightBookDbContext context;
        private readonly UserManager<PluralSightBookIdentityUser> userManager;

        public EfProfileRepository(PluralSightBookDbContext context, UserManager<PluralSightBookIdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<PluralSightBookIdentityUser> GetUser(Guid userId)
        {
            return await this.userManager.FindByIdAsync(userId.ToString());
        }
    }
}