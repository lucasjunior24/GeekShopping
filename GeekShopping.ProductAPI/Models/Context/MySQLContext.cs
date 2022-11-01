using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Models.Context
{
    public class MySQLContext : DbContext
    {
        protected MySQLContext()
        {
        }
        public MySQLContext(DbContextOptions options) : base(options)
        {
        }

    }
}


