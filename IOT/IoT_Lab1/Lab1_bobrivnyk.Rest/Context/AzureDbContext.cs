using Lab1_bobrivnyk.Rest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lab1_bobrivnyk.Rest.Context
{
    public class AzureDbContext : DbContext
    {
        public AzureDbContext()
        {

        }

        public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=tcp:lab3-database-server.database.windows.net,1433;
            Initial Catalog=bobrivnykLab1-cdv;Persist Security Info=False;
            User ID=dbobrivnyk;Password=j95h1VA15i;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Person> People { get; set; }
    }


}
