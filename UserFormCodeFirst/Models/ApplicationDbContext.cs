using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UserFormCodeFirst.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext() : base("DefaultConnection")
        {

        }
    }
}