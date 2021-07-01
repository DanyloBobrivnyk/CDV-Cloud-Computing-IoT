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
        //this is a bad aproach, but I can't get this data from properties
        private readonly string configurationString = "Server=tcp:lab3-database-server.database.windows.net,1433;Initial Catalog=bobrivnykLab1-cdv;Persist Security Info=False;" +
            "User ID=dbobrivnyk;Password=VeryStrongPassword@1;MultipleActiveResultSets=False;Encrypt=True;" +
            "TrustServerCertificate=False;Connection Timeout=30;";
        public AzureDbContext()
        {

        }
        public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(configurationString);
        }

        public DbSet<User> Users { get; set; }
    }


}
