using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using System;

namespace BlazorFormSample.Data.Migrations
{
    public class CreatureContextFactory : IDesignTimeDbContextFactory<CreatureMigrationDbContext>
    {
        private const string defaultDB = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=BlazorFormSample";
        public CreatureMigrationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CreatureMigrationDbContext>();
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.AddEnvironmentVariables();
            var config = builder.Build();
            
            // need to figure out why env vars aren't being respected by `dotnet ef` CLI commands. Meanwhile, this should be correct path.
            var constr = 
                Environment.GetEnvironmentVariable("CreatureDatabase" , EnvironmentVariableTarget.Process) 
                ?? Environment.GetEnvironmentVariable("CreatureDatabase", EnvironmentVariableTarget.User) 
                ?? Environment.GetEnvironmentVariable("CreatureDatabase", EnvironmentVariableTarget.Machine) 
                ?? config.GetConnectionString("CreatureDatabase") 
                ?? defaultDB;
            System.Diagnostics.Debug.WriteLine(constr);
            optionsBuilder.UseSqlServer(constr);            
            return new CreatureMigrationDbContext(optionsBuilder.Options);
        }
    }
}
