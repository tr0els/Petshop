using System;
using System.Collections.Generic;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Core.ApplicationServices
{
    public interface IPetService
    {
        FilteredList<Pet> ReadAll(Filter.Filter filter);
        Pet Read(int id);
        Pet Create(Pet pet);
        Pet Update(Pet pet);
        Pet Delete(int id);
    }
}
