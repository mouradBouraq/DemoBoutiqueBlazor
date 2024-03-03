using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Commande;
using DemoBoutique.Domain.Produit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoBoutique.Infrastructure.Persistence
{
    public class DbContextDemoBoutique : IdentityDbContext<IdentityUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DbContextDemoBoutique(DbContextOptions<DbContextDemoBoutique> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Commande> Commande { get; set; }
        public DbSet<Produit> Produit { get; set; }
        public DbSet<CommandeLigne> CommandeLigne { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Client> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Disable client side (EF) cascading delete, null behavior
            var foreignKeysRelations = builder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.ClientSetNull);
            foreach (var fk in foreignKeysRelations)
            {
                fk.DeleteBehavior = DeleteBehavior.ClientNoAction;
            }
            InitUsers(builder);
            //InitDataBase(builder);
            base.OnModelCreating(builder);
        }

        private void InitUsers(ModelBuilder modelBuilder)
        {
            string SUPPER_ADMIN_ID = "ac7f3b1a-01b5-4236-ab2e-b7342ed75a4d";
            string ROLE_ID = "bc7f3b1a-01b5-4236-ab2e-b7342ed75a4d";

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create user
            var appUser = new IdentityUser
            {
                Id = SUPPER_ADMIN_ID,
                Email = "system@boutique.com",
                NormalizedEmail = "system@boutique.com",
                EmailConfirmed = true,
                UserName = "system@boutique.com",
                NormalizedUserName = "system@boutique.com",
                ConcurrencyStamp = "a194464e-3a04-48c2-b80f-5ffbbeb9d795"

            };

            //set user password
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "123456@ABc");

            //seed user
            modelBuilder.Entity<IdentityUser>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = SUPPER_ADMIN_ID
            });

        }
    }
}
