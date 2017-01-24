using Domain.Model;
using Domain.Interface;
using System;
using System.Collections.Generic;
using Data.Context;

namespace Data.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumContext _context;

        public UserRepository(ForumContext context)
        {
            _context = context;            
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
