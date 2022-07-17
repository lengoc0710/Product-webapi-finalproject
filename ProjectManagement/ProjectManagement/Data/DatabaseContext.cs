using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Configurations.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUserSystem>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }


        public DbSet<Loai> Loais { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LoaiConfiguration());
            builder.ApplyConfiguration(new HangHoaConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
