using Microsoft.AspNetCore.Identity;
using PluralSightBook.Core.Models;
using System.Collections.Generic;

namespace PluralSightBook.Core.Identity
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