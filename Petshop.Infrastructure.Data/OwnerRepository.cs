using System;
using Petshop.Core.DomainServices;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        public IEnumerable<Owner> ReadAll()
        {
            return FakeDB.OwnerTable;
        }

        public Owner Read(int id)
        {
            var toFind = FakeDB.OwnerTable.FirstOrDefault(owner => owner.Id == id);
            return toFind;
        }

        public Owner Update(Owner updatedOwner)
        {
            int index = FakeDB.OwnerTable.FindIndex(owner => owner.Id == updatedOwner.Id);
            if (index != -1)
            {
                FakeDB.OwnerTable[index] = updatedOwner;
                return updatedOwner;
            }
            return null;
        }

        public Owner Create(Owner owner)
        {
            owner.Id = FakeDB.OwnerId++;
            FakeDB.OwnerTable.Add(owner);
            return owner;
        }

        public Owner Delete(int id)
        {
            // FirstOrDefault returns null if no id is found
            // that way án exception can be thrown and handled in service and ui
            var toRemove = FakeDB.OwnerTable.FirstOrDefault(owner => owner.Id == id);
            FakeDB.OwnerTable.Remove(toRemove);
            return toRemove;
        }

        public FilteredList<Owner> ReadAll(Filter filter)
        {
            throw new NotImplementedException();
        }
    }
}
