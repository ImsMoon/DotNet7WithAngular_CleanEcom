using System.Security.Claims;

namespace Ecom.Infrastructure.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email)!;
        }
    }
}