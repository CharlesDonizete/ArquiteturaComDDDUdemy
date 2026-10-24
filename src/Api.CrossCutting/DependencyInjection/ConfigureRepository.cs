using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            if(Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
                serviceCollection.AddDbContext<MyContext>(options =>
                    options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION")));

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementarion>();            
        }
    }
}
