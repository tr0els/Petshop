using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.Core.DomainServices;

namespace Petshop.Core.ApplicationServices.Impl
{
    public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository repository)
        {
            this._ownerRepository = repository;
        }
        public List<Owner> ReadAll()
        {
            return _ownerRepository.ReadAll().ToList();
        }

        public Owner Read(int id)
        {
            if (id < 1) throw new ArgumentException($"Id must be a positive integer.");
            Owner owner = _ownerRepository.Read(id);
            if (owner is null) throw new KeyNotFoundException($"No record with id {id} was found.");
            return owner;
        }

        public Owner Create(Owner owner)
        {
            ValidateOwnerData(owner);
            return _ownerRepository.Create(owner);
        }
        public Owner Update(Owner owner)
        {
            if (owner.Id < 1) throw new ArgumentException($"Id must be a positive integer.");
            ValidateOwnerData(owner);
            Owner ownerUpdated = _ownerRepository.Update(owner);
            if (ownerUpdated is null) throw new KeyNotFoundException($"No record with id {owner.Id} was found.");
            return ownerUpdated;
        }

        public Owner Delete(int id)
        {
            if (id < 1) throw new ArgumentException($"Id must be a positive integer.");
            Owner owner = _ownerRepository.Delete(id);
            if (owner is null) throw new KeyNotFoundException($"No record with id {id} was found.");
            return owner;
        }

        private void ValidateOwnerData(Owner owner)
        {
            if (string.IsNullOrEmpty(owner.FirstName) || owner.FirstName.Length< 2) throw new ArgumentException("Firstname must be minimum 2 characters long.");
            if (string.IsNullOrEmpty(owner.LastName) || owner.LastName.Length < 2) throw new ArgumentException("Lastname must be minimum 2 characters long.");
        }
}
}