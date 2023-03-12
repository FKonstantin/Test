using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFisenko.Models;

namespace TestFisenko.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Goods> GoodsOrder { get; set; }
        public DbSet<Image> GoodsImage { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {

        }
    }
}
