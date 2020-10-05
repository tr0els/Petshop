using System;
using System.Collections.Generic;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Core.ApplicationServices
{
    public interface IUserService
    {
        List<User> ReadAll();
        User Read(int id);
        User Create(User owner);
    }
}
