using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options) { }

        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }
    }
}
