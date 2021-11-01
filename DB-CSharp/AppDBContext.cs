using DB_CSharp.Configurations;
using DB_CSharp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DB_CSharp
{
    public class AppDBContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDBContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Adm_AppRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("Adm_AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("Adm_AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("Adm_AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("Adm_AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("Adm_AppUserTokens").HasKey(x => x.UserId);
        }
    }
}
