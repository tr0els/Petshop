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
    public class UserRepository : IUserRepository
    {
        private readonly PetshopSQLiteContext _ctx;

        public UserRepository(PetshopSQLiteContext ctx)
        {
            _ctx = ctx;
        }

        public User Create(User user)
        {
            var userEntry = _ctx.Add(user);
            _ctx.SaveChanges();
            return userEntry.Entity;
        }

        public List<User> ReadAll()
        {
            return _ctx.Users.ToList();
        }

        public User Read(int id)
        {
            return _ctx.Users
                .AsNoTracking() // don't track changes for readonly (don't cache)
                .FirstOrDefault(o => o.Id == id);
        }

        // returns void because I think validation should be done in service, 
        // so here I just update what I know is already good data.
        // The service could return the updated item if all is good.
        public void Update(User entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Remove(long id)
        {
            var item = _ctx.Users.FirstOrDefault(i => i.Id == id);
            _ctx.Users.Remove(item);
            _ctx.SaveChanges();
        }
    }
}
