using PluralSightBook.Core.Identity;

namespace PluralSightBook.Core.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public string PluralSightBookIdentityUser { get; set; }

        public PluralSightBookIdentityUser IdentityUser { get; set; }
    }
}