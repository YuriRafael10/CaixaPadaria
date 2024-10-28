using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaixaPadaria.Models;
using Microsoft.EntityFrameworkCore;

namespace CaixaPadaria.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=datafile.db");
        }

        public DbSet<User> Users { get; set; }
    }
}
