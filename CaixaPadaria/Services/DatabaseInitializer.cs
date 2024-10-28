using CaixaPadaria.Context;
using CaixaPadaria.Models;
using System.Linq;

namespace CaixaPadaria.Services
{
    public class DatabaseInitializer
    {
        private readonly AppDbContext _context;

        public DatabaseInitializer(AppDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();

            if (!_context.Users.Any(u => u.IsAdmin))
            {
                var adminUser = new User
                {
                    Name = "admin",
                    Password = "admin123",
                    IsAdmin = true,
                    TotalSales = 0
                };

                _context.Users.Add(adminUser);
                _context.SaveChanges();
            }
        }
    }
}
