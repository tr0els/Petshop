using Petshop.Core.Entity;
using Petshop.Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.Core.Filter;

namespace Petshop.Infrastructure.SQLite.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PetshopSQLiteContext _ctx;

        public OwnerRepository(PetshopSQLiteContext ctx)
        {
            _ctx = ctx;
        }

        public Owner Create(Owner owner)
        {
            var ownerEntry = _ctx.Add(owner);
            _ctx.SaveChanges();
            return ownerEntry.Entity;
        }

        public FilteredList<Owner> ReadAll(Filter filter)
        {
            var filteredList = new FilteredList<Owner>();
            filteredList.TotalCount = _ctx.Owners.Count();
            filteredList.FilterUsed = filter;
            filteredList.List = _ctx.Owners.ToList();
            return filteredList;
        }

        public Owner Read(int id)
        {
            return _ctx.Owners
                .AsNoTracking() // don't track changes for readonly (don't cache)
                .FirstOrDefault(o => o.Id == id);
        }

        public Owner Update(Owner owner)
        {
            throw new NotImplementedException();
        }

        public Owner Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
