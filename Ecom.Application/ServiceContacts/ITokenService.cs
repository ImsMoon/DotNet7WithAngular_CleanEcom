using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Domain.Entities.Identity;

namespace Ecom.Application.ServiceContacts
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}