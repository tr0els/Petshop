
using System;
using System.Collections.Generic;
using Petshop.Core.Entity;

namespace Petshop.Core.DomainServices
{
    public interface IPetRepository
    {
        Pet Create(Pet pet);
        IEnumerable<Pet> ReadAll();
        Pet Read(int id);
        Pet Update(Pet pet);
        Pet Delete(int id);
        IEnumerable<Pet> SearchByType(string type);
        IEnumerable<Pet> SortByPrice(IEnumerable<Pet> pets, string order);
        IEnumerable<Pet> TopXCheapest(IEnumerable<Pet> pets, int amount);
    }
}
