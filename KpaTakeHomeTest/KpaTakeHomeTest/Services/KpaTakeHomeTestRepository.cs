using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KpaTakeHomeTest.Entities;
using KpaTakeHomeTest.Models;

namespace KpaTakeHomeTest.Services
{
    public class KpaTakeHomeTestRepository : IKpaTakeHomeTestRepository
    {
        private KpaTakeHomeTestContext _context;
        public KpaTakeHomeTestRepository(KpaTakeHomeTestContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.EmailAddress).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
