
using System;
using System.Collections.Generic;
using Petshop.Core.Entity;

namespace Petshop.Core.DomainServices
{
    public interface IOwnerRepository
    {
        Owner Create(Owner owner);
        IEnumerable<Owner> ReadAll();
        Owner Read(int id);
        Owner Update(Owner owner);
        Owner Delete(int id);
    }
}
