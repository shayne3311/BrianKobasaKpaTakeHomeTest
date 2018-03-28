using KpaTakeHomeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KpaTakeHomeTest.Services
{
    public interface IKpaTakeHomeTestRepository
    {
        IEnumerable<User> GetUsers();
        void AddUser(User user);
        bool Save();
    }
}
