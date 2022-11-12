using MusicRancho_RanchoAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicRancho_RanchoAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Rancho> Ranchos { get; set; }
        public DbSet<RanchoNumber> RanchoNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rancho>().HasData(
                new Rancho
                {
                    Id = 1,
                    Name = "Royal Rancho",
                    Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://someWebSite.com/blueranchoimages/rancho3.jpg",
                    Occupancy = 4,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
              new Rancho
              {
                  Id = 2,
                  Name = "Premium Pool Rancho",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://someWebSite.com/blueranchoimages/rancho1.jpg",
                  Occupancy = 4,
                  Rate = 300,
                  Sqft = 550,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              },
              new Rancho
              {
                  Id = 3,
                  Name = "Luxury Pool Rancho",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://someWebSite.com/blueranchoimages/rancho4.jpg",
                  Occupancy = 4,
                  Rate = 400,
                  Sqft = 750,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              },
              new Rancho
              {
                  Id = 4,
                  Name = "Diamond Rancho",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://someWebSite.com/blueranchoimages/rancho5.jpg",
                  Occupancy = 4,
                  Rate = 550,
                  Sqft = 900,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              },
              new Rancho
              {
                  Id = 5,
                  Name = "Diamond Pool Rancho",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://someWebSite.com/blueranchoimages/rancho2.jpg",
                  Occupancy = 4,
                  Rate = 600,
                  Sqft = 1100,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              });
        }
    }
}
