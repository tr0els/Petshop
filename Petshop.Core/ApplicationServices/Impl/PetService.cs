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
            if (id < 1) throw new ArgumentException($"Id must be a positive integer.");
            Pet pet = _petRepository.Read(id);
            if (pet is null) throw new KeyNotFoundException($"No record with id {id} was found.");
            return pet;
        }

        public Pet Create(Pet pet)
        {
            ValidatePetData(pet);
            return _petRepository.Create(pet);
        }
        public Pet Update(Pet pet)
        {
            if (pet.Id < 1) throw new ArgumentException($"Id must be a positive integer.");
            ValidatePetData(pet);
            Pet petUpdated = _petRepository.Update(pet);
            if (petUpdated is null) throw new KeyNotFoundException($"No record with id {pet.Id} was found.");

            return _petRepository.Update(pet);
        }

        public Pet Delete(int id)
        {
            if (id < 1) throw new ArgumentException($"Id must be a positive integer.");
            Pet pet = _petRepository.Delete(id);
            if (pet is null) throw new KeyNotFoundException($"No record with id {id} was found.");
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

        private void ValidatePetData(Pet pet)
        {
            if (string.IsNullOrEmpty(pet.Name) || pet.Name.Length< 2) throw new ArgumentException("Name must be minimum 2 characters long.");
            if (!Enum.IsDefined(typeof(PetType), pet.Type)) throw new ArgumentException("The pet type is invalid.");
            if (!DateTime.TryParse(pet.BirthDate.ToString(), out _)) throw new ArgumentException("Birth date invalid format."); // service check - api controller validates date first
            if (DateTime.Compare(pet.BirthDate, DateTime.Now) > 0) throw new ArgumentException("Birth date must be in the past.");
            if (!DateTime.TryParse(pet.SoldDate.ToString(), out _)) throw new ArgumentException("Sold date invalid format."); // service check - api controller validates date first
            if (DateTime.Compare(pet.SoldDate, DateTime.Now) > 0) throw new ArgumentException("Sold date must be in the past.");
            if (pet.Price< 0) throw new ArgumentException("Price must be an integer greater than or equal to 0.");
        }
}
}