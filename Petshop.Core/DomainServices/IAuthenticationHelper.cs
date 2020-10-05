using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;

namespace Petshop.Core.DomainServices
{
    public interface IAuthenticationHelper
    {
        string GenerateToken(User user);
    }
}
