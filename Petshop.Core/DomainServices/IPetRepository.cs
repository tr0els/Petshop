
using System;
using System.Collections.Generic;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Core.DomainServices
{
    public interface IPetRepository
    {
        Pet Create(Pet pet);
        FilteredList<Pet> ReadAll(Filter.Filter filter = null);
        Pet Read(int id);
        Pet Update(Pet pet);
        Pet Delete(int id);
        //IEnumerable<Pet> SearchByType(string type);
        //IEnumerable<Pet> SortByPrice(IEnumerable<Pet> pets, string order);
        //IEnumerable<Pet> TopXCheapest(IEnumerable<Pet> pets, int amount);
    }
}
