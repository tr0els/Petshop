using System;
using System.Collections.Generic;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Core.ApplicationServices
{
    public interface IOwnerService
    {
        FilteredList<Owner> ReadAll(Filter.Filter filter);
        Owner Read(int id);
        Owner Create(Owner owner);
        Owner Update(Owner owner);
        Owner Delete(int id);
    }
}
