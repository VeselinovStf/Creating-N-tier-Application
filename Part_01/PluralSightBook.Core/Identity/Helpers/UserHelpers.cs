using System;
using System.Security.Claims;
using System.Security.Principal;

namespace PluralSightBook.Core.Identity.Helpers
{
    public static class UserHelpers
    {
        public static Guid GetUserId(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return new Guid(claim.Value);
        }
    }
}