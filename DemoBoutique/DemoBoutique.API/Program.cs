using DemoBoutique.API.Filters;
using DemoBoutique.Application.Interfaces;
using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Application.Services;
using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Commande;
using DemoBoutique.Domain.Produit;
using DemoBoutique.Infrastructure;
using DemoBoutique.Infrastructure.Persistence;
using DemoBoutique.Infrastructure.Persistence.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


namespace DemoBoutique.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // config data base
            var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DbContextDemoBoutique>(options => options.UseSqlServer(defaultConnection, b => b.MigrationsAssembly("DemoBoutique.Infrastructure")));

            // Add services to the container.
            builder.Services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            // For Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DbContextDemoBoutique>().AddDefaultTokenProviders();

        

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedAccount = false;


                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = false;
            });

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            // Add services to the container.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceBase<Categorie>, CategorieService>();
            builder.Services.AddScoped<IServiceBase<Client>, ClientService>();
            builder.Services.AddScoped<IServiceBase<Commande>, CommandeService>();
            builder.Services.AddScoped<IServiceBase<CommandeLigne>, CommandeLigneService>();
            builder.Services.AddScoped<IServiceBase<Produit>, ProduitService>();
            builder.Services.AddScoped<IIdentityUserManager, IdentityUserManager>();

            builder.Services.AddScoped<IUserManagerService, UserManagerService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header. \r\n\r\n Enter the token in the text input below.,"
                });

                //c.OperationFilter<AddRequiredHeaderParameter>();
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.MigrateDatabase();


            app.Run();
        }
    }
}
