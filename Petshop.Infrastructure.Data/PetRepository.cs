using System;
using Petshop.Core.DomainServices;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        public IEnumerable<Pet> ReadAll()
        {
            return FakeDB.PetTable;
        }

        public Pet Read(int id)
        {
            var toFind = FakeDB.PetTable.FirstOrDefault(pet => pet.Id == id);
            return toFind;
        }

        public Pet Update(Pet updatedPet)
        {
            int index = FakeDB.PetTable.FindIndex(pet => pet.Id == updatedPet.Id);
            if (index != -1)
            {
                FakeDB.PetTable[index] = updatedPet;
                return updatedPet;
            }
            return null;
        }

        public Pet Create(Pet pet)
        {
            pet.Id = FakeDB.PetId++;
            FakeDB.PetTable.Add(pet);
            return pet;
        }

        public Pet Delete(int id)
        {
            // FirstOrDefault returns null if no id is found
            // that way án exception can be thrown and handled in service and ui
            var toRemove = FakeDB.PetTable.FirstOrDefault(pet => pet.Id == id);
            FakeDB.PetTable.Remove(toRemove);
            return toRemove;
        }

        public IEnumerable<Pet> SortByPrice(IEnumerable<Pet> pets, string order)
        {
            return order.ToLower() switch
            {
                "asc" => pets.OrderBy(pet => pet.Price),
                "desc" => pets.OrderByDescending(pet => pet.Price),
                _ => throw new InvalidFilterCriteriaException("Parameter invalid")
            };
        }

        public IEnumerable<Pet> TopXCheapest(IEnumerable<Pet> pets, int amount)
        {
            var topXCheapest = pets.OrderByDescending(pet => pet.Price).Take(amount);
            return topXCheapest;
        }
        
        public IEnumerable<Pet> SearchByType(string type)
        {
            if (Enum.IsDefined(typeof(PetType), type))
            {
                return ReadAll().Where(pet => pet.Type.ToString().Equals(type));
            }
            else
            {
                throw new InvalidFilterCriteriaException("type invalid");
            }
        }
    }
}
