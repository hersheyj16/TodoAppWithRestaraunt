using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Models;

namespace DotNetCoreSqlDb.Models
{
public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext (DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetCoreSqlDb.Models.Todo> Todo { get; set; }
        public DbSet<DotNetCoreSqlDb.Models.Restaurant> Restaurants { get; set; }
        public DbSet<DotNetCoreSqlDb.Models.Event> Event { get; set; }
        public DbSet<DotNetCoreSqlDb.Models.Choice> Choice { get; set; }

    }
}
