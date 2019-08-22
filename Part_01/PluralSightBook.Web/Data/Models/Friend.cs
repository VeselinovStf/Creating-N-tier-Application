using PluralSightBook.Web.Identity;

namespace PluralSightBook.Web.Data.Models
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