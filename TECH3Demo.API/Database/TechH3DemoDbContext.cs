using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;

namespace TecH3Demo.API.Database
{
    public class TechH3DemoDbContext : DbContext
    {

        public TechH3DemoDbContext(){}
        public TechH3DemoDbContext(DbContextOptions<TechH3DemoDbContext> options) : base(options) {}
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
