using Microsoft.AspNetCore.Identity;
using PluralSightBook.Web.Data.Models;
using System.Collections.Generic;

namespace PluralSightBook.Web.Identity
{
    public class PluralSightBookIdentityUser : IdentityUser
    {
        public PluralSightBookIdentityUser()
        {
            this.Friends = new HashSet<Friend>();
        }

        public string FavoriteAuthor { get; set; }

        public ICollection<Friend> Friends { get; set; }
    }
}