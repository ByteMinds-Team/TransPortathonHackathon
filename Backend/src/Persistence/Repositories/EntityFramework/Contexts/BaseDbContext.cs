using Application.Common.Utilities;
using Domain;
using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Repositories.EntityFramework.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
    public DbSet<Crew> Crews { get; set; }
    public DbSet<DateGap> DateGaps { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<TransportRequest> TransportRequests { get; set; }
    public DbSet<TransportType> TransportTypes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var data = ChangeTracker.Entries<Entity>();
        foreach (var item in data)
        {
            _ = item.State switch
            {

                EntityState.Added => item.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => item.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
            //item.Entity.Id = Guid.NewGuid().ToString();
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(p =>
        {
            p.ToTable("Users").HasKey(k => k.Id);
            p.Property(p => p.Id).HasColumnName("Id");
            p.Property(p => p.FullName).HasColumnName("Fullname");
            p.Property(p => p.Email).HasColumnName("Email");
            p.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
            p.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
            p.Property(p => p.Status).HasColumnName("Status").HasDefaultValue(true);
            p.HasMany(p => p.UserOperationClaims);
            p.HasMany(p => p.RefreshTokens);
        });

        modelBuilder.Entity<CorporateCustomer>(p =>
        {
            p.ToTable("CorporateCustomers");
            p.HasMany(p => p.Vehicles);
            p.HasMany(p => p.Employees);
        });

        modelBuilder.Entity<Driver>(p =>
        {
            p.ToTable("Drivers");
        });

        modelBuilder.Entity<OperationClaim>(p =>
        {
            p.ToTable("OperationClaims").HasKey(k => k.Id);
            p.Property(p => p.Id).HasColumnName("Id");
            p.Property(p => p.Name).HasColumnName("Name");
        });

        modelBuilder.Entity<UserOperationClaim>(p =>
        {
            p.ToTable("UserOperationClaims").HasKey(k => k.Id);
            p.Property(p => p.Id).HasColumnName("Id");
            p.Property(p => p.UserId).HasColumnName("UserId");
            p.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
            p.HasOne(p => p.OperationClaim);
            p.HasOne(p => p.User);
        });

        HashingHelper.CreatePasswordHash("1234567", out var passwordHash, out var passwordSalt);




        OperationClaim[] operationClaimEntitySeeds = { new(1, "admin"), new(2, "corporate") };
        modelBuilder.Entity<OperationClaim>().HasData(operationClaimEntitySeeds);

        User[] userEntitySeeds = { new(1, "admin", "admin@admin.com", "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg", passwordSalt, passwordHash, true), new(2, "normal", "normal@normal.com", "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg", passwordSalt, passwordHash, true),
            };
        modelBuilder.Entity<User>().HasData(userEntitySeeds);
        /*new(3, "sirket", "sirket@sirket.com", "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg", passwordSalt, passwordHash, true) */
        /**/
        CorporateCustomer[] corporateCustomerEntitySeeds = { new(){
        Id = 3,
        CompanyName = "BenimŞirket",
        FullName = "şirket",
        Email = "sirket@sirket.com",
        PasswordHash = passwordHash,
        PasswordSalt = passwordSalt,
        ProfileImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg",
        }
    };
        modelBuilder.Entity<CorporateCustomer>().HasData(corporateCustomerEntitySeeds);


        UserOperationClaim[] userOperationClaimEntitySeeds = { new(1, 1, 1), new(2, 3, 2) };
        modelBuilder.Entity<UserOperationClaim>().HasData(userOperationClaimEntitySeeds);


        DateGap[] dateGapEntitySeeds = { new() { Id = 1, Gap = "1 Ay İçinde" }, new() { Id = 2, Gap = "2 Ay İçinde" } };
        modelBuilder.Entity<DateGap>().HasData(dateGapEntitySeeds);

        TransportType[] transportTypeEntitySeeds = { new() { Id = 1, Type = "Ofis" }, new() { Id = 2, Type = "Ev" } };
        modelBuilder.Entity<TransportType>().HasData(transportTypeEntitySeeds);

        Employee[] employeeEntitySeeds = { new() { CorporateCustomerId = 3 , FullName = "Çalışan1" ,Id = 1,ProfileImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg" },
            new() { CorporateCustomerId = 3 , FullName = "Çalışan2" ,Id = 2,ProfileImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg" } };
        modelBuilder.Entity<Employee>().HasData(employeeEntitySeeds);

        //Crew[] crewEntitySeeds = { new() {Id = 1,Employees = employeeEntitySeeds , CorporateCustomerId = 3 }};
        //modelBuilder.Entity<Crew>().HasData(crewEntitySeeds);

        Vehicle[] vehicleEntitySeeds = { new() {Id = 1 , Brand = "Fiat" , Color = "Mor" , CorporateCustomerId = 3 , ImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg" , ModelYear = 2020,NumberPlate = "34 AZB 40" },
        new() {Id = 2 , Brand = "Fiat" , Color = "Mor" , CorporateCustomerId = 3 , ImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg" , ModelYear = 2000,NumberPlate = "34 AZB 41" },
        new() {Id = 3 , Brand = "Fiat" , Color = "Kırmızı" , CorporateCustomerId = 3 , ImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg" , ModelYear = 2020,NumberPlate = "34 AZB 42" },
        new() {Id = 4 , Brand = "Fiat" , Color = "Sarı" , CorporateCustomerId = 3 , ImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg" , ModelYear = 2010,NumberPlate = "34 AZB 43" },
        new() {Id = 5 , Brand = "Fiat" , Color = "Turuncu" , CorporateCustomerId = 3 , ImagePath = "https://res.cloudinary.com/emreaka/image/upload/v1694542771/TransportHackathonPics/eh6eikbvfo7dwjutjkdv.jpg" , ModelYear = 2001,NumberPlate = "34 AZB 44" }
        };
        modelBuilder.Entity<Vehicle>().HasData(vehicleEntitySeeds);

        Driver[] driverEntitySeeds = { new() { Id = 1, EmployeeId = 1 , VehicleId = 1} ,
            new() { Id = 2, EmployeeId = 1, VehicleId = 2 } ,
            new() { Id = 3, EmployeeId = 2 , VehicleId = 3},
            new() { Id = 4, EmployeeId = 2 , VehicleId = 4},
            new() { Id = 5, EmployeeId = 1 , VehicleId = 5}
        };
        modelBuilder.Entity<Driver>().HasData(driverEntitySeeds);
    }
}