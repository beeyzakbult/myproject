using Microsoft.EntityFrameworkCore;
using myproject.Models;
using System.Collections.Generic;

namespace myproject.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Person> persons { get; set; }
    }
}
