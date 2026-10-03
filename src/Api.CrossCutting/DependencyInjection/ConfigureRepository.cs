using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<MyContext>(options =>
             options.UseSqlServer("Data Source=DESKTOP-J5VHRAJ\\SQLEXPRESS01;Initial Catalog=dbApi;Integrated Security=False;User Id=admin;Password=123;Trusted_Connection=False;TrustServerCertificate=true"));

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementarion>();            
        }
    }
}
