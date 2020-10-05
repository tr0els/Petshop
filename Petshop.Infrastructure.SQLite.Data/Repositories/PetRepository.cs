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
    public class PetRepository : IPetRepository
    {
        private readonly PetshopSQLiteContext _ctx;

        public PetRepository(PetshopSQLiteContext ctx)
        {
            _ctx = ctx;
        }

        public Pet Create(Pet pet)
        {
            var petEntry = _ctx.Add(pet);
            _ctx.SaveChanges();
            return petEntry.Entity;
        }

        public FilteredList<Pet> ReadAll(Filter filter)
        {
            var filteredList = new FilteredList<Pet>
            {
                TotalCount = _ctx.Pets.Count(),
                FilterUsed = filter,
                List = _ctx.Pets
                    //.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    //.Take(filter.ItemsPrPage)
                    .ToList()
            };
            return filteredList;
        }

        public Pet Read(int id)
        {
            return _ctx.Pets
                .AsNoTracking() // don't track changes for readonly (don't cache)
                .FirstOrDefault(p => p.Id == id);
        }

        public Pet Update(Pet pet)
        {
            var petEntry = _ctx.Update(pet);
            _ctx.SaveChanges();
            return petEntry.Entity;
        }

        public Pet Delete(int id)
        {
            var pet = Read(id); // not the most efficient solution?
            var petEntry = _ctx.Remove(pet);
            _ctx.SaveChanges();
            return petEntry.Entity;
        }
    }
}
