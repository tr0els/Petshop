using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.Core.DomainServices;

namespace Petshop.Core.ApplicationServices.Impl
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepository;

        // dependency injection, this way repositories are interchangeable
        public PetService(IPetRepository repository)
        {
            this._petRepository = repository;
        }
        public List<Pet> ReadAll()
        {
            return _petRepository.ReadAll().ToList();
        }

        public Pet Read(int id)
        {
            return _petRepository.Read(id);
        }

        public Pet Create(Pet pet)
        {
            return _petRepository.Create(pet);
        }
        public Pet Update(Pet pet)
        {
            return _petRepository.Update(pet);
        }

        public Pet Delete(int id)
        {
            return _petRepository.Delete(id);
        }

        public IEnumerable<Pet> SearchByType(string type)
        {
            return _petRepository.SearchByType(type);
        }

        public IEnumerable<Pet> SortByPrice(IEnumerable<Pet> pets, string order)
        {
            return _petRepository.SortByPrice(pets, order);
        }

        public IEnumerable<Pet> TopXCheapest(IEnumerable<Pet> pets, int amount)
        {
            return _petRepository.TopXCheapest(pets, amount);
        }
    }
}