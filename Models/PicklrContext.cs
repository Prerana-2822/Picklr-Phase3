using Microsoft.EntityFrameworkCore;

namespace Picklr.Models
{
    public class PicklrContext : DbContext
    {
        public PicklrContext(DbContextOptions<PicklrContext> options)
            : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; } = null!;
        public DbSet<PicklProgram> Programs { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One Club can have many Programs.
            modelBuilder.Entity<PicklProgram>()
                .HasOne(p => p.Club)
                .WithMany(c => c.Programs)
                .HasForeignKey(p => p.ClubID);

            // Seed Clubs
            modelBuilder.Entity<Club>().HasData(
                new Club
                {
                    ClubID = 1,
                    Name = "Picklr Downtown",
                    Location = "123 Main St, Chicago, IL",
                    Description = "Our flagship downtown club with 10 indoor courts."
                },
                new Club
                {
                    ClubID = 2,
                    Name = "Picklr Northside",
                    Location = "456 Oak Ave, Evanston, IL",
                    Description = "A vibrant outdoor facility with 8 courts and a pro shop."
                },
                new Club
                {
                    ClubID = 3,
                    Name = "Picklr New York",
                    Location = "789 Broadway, New York, NY",
                    Description = "An indoor pickleball club located in New York."
                }
            );

            // Seed Programs
            modelBuilder.Entity<PicklProgram>().HasData(
                new PicklProgram
                {
                    ProgramID = 1,
                    ClubID = 1,
                    Name = "Beginner Open Play",
                    Description = "Drop-in open play for new players. No experience needed.",
                    Fee = 10.00m,
                    Monday = true,
                    Tuesday = false,
                    Wednesday = true,
                    Thursday = false,
                    Friday = true,
                    Saturday = false,
                    Sunday = false
                },
                new PicklProgram
                {
                    ProgramID = 2,
                    ClubID = 1,
                    Name = "Intermediate Clinic",
                    Description = "Weekly skill-building clinic led by a certified coach.",
                    Fee = 25.00m,
                    Monday = false,
                    Tuesday = true,
                    Wednesday = false,
                    Thursday = true,
                    Friday = false,
                    Saturday = false,
                    Sunday = false
                },
                new PicklProgram
                {
                    ProgramID = 3,
                    ClubID = 2,
                    Name = "Advanced Tournament",
                    Description = "Competitive round-robin tournament for rated players.",
                    Fee = 40.00m,
                    Monday = false,
                    Tuesday = false,
                    Wednesday = false,
                    Thursday = false,
                    Friday = false,
                    Saturday = true,
                    Sunday = true
                },
                new PicklProgram
                {
                    ProgramID = 4,
                    ClubID = 3,
                    Name = "Picklr 101",
                    Description = "Introduction to pickleball for new players.",
                    Fee = 10.00m,
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = true,
                    Sunday = true
                },
                new PicklProgram
                {
                    ProgramID = 5,
                    ClubID = 2,
                    Name = "Picklr Social",
                    Description = "Weekend social play for all skill levels.",
                    Fee = 0.00m,
                    Monday = false,
                    Tuesday = false,
                    Wednesday = false,
                    Thursday = false,
                    Friday = false,
                    Saturday = true,
                    Sunday = false
                }
            );

            // Seed Users
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    UserID = 1,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice@picklr.com",
                    Role = "Admin"
                },
                new AppUser
                {
                    UserID = 2,
                    FirstName = "Bob",
                    LastName = "Jones",
                    Email = "bob@picklr.com",
                    Role = "Client"
                }
            );
        }
    }
}