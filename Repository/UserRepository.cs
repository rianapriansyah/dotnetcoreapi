using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using stargate.Models;

namespace stargate.Repository
{
    public class UserRepository : IRepository<User>
    {
        private UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
             _context.Users.Add(entity);
             _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            List<User> users = _context.Users.ToList();
            return users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User entity, int id)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}