using System;
using System.Collections.Generic;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Core.DomainServices
{
    public interface IUserRepository
    {
        User Create(User owner);
        List<User> ReadAll();
        User Read(int id);
        //User Update(User owner);
        //User Delete(int id);
    }
}
