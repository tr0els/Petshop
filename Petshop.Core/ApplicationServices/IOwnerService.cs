using System;
using System.Collections.Generic;
using Petshop.Core.Entity;

namespace Petshop.Core.ApplicationServices
{
    public interface IOwnerService
    {
        List<Owner> ReadAll();
        Owner Read(int id);
        Owner Create(Owner owner);
        Owner Update(Owner owner);
        Owner Delete(int id);
    }
}
