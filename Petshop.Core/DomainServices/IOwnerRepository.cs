using System;
using System.Collections.Generic;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Core.DomainServices
{
    public interface IOwnerRepository
    {
        Owner Create(Owner owner);
        FilteredList<Owner> ReadAll(Filter.Filter filter = null);
        Owner Read(int id);
        Owner Update(Owner owner);
        Owner Delete(int id);
    }
}
