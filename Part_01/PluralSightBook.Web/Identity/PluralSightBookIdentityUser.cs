using Microsoft.AspNetCore.Identity;

namespace PluralSightBook.Web.Identity
{
    public class PluralSightBookIdentityUser : IdentityUser
    {
        public string FavoriteAuthor { get; set; }
    }
}