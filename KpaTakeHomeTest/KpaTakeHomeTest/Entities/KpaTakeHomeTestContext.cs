using KpaTakeHomeTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KpaTakeHomeTest.Entities
{
    public class KpaTakeHomeTestContext: DbContext
    {
        public KpaTakeHomeTestContext(DbContextOptions<KpaTakeHomeTestContext> options)
           : base(options)
        {
            //Create tables if not already created
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

    }
}
