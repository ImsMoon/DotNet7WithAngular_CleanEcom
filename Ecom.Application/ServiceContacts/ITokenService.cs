using Ecom.Domain.Entities.Identity;

namespace Ecom.Application.ServiceContacts
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}