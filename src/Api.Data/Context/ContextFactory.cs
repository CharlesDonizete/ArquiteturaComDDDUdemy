using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=DESKTOP-J5VHRAJ\\SQLEXPRESS01;Initial Catalog=dbApi;Integrated Security=False;User Id=admin;Password=123;Trusted_Connection=False;TrustServerCertificate=true";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }    
    }
}
