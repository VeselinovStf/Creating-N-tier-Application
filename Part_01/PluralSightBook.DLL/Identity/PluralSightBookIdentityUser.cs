using Microsoft.AspNetCore.Identity;
using PluralSightBook.DLL.Data.Models;
using System.Collections.Generic;

namespace PluralSightBook.DLL.Identity
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