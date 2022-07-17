using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Configurations.EntityFramework
{
    public class LoaiConfiguration : IEntityTypeConfiguration<Loai>
    {
        public void Configure(EntityTypeBuilder<Loai> builder)
        {
            builder.HasData(
                new Loai
                {
                    Id = 1,
                    TenLoai = "Bánh Mì",
                    MaLoai = "Old"
                },
                new Loai
                {
                    Id = 2,
                    TenLoai= "Mì",
                    MaLoai = "New"
                },
                new Loai
                {
                    Id = 3,
                    TenLoai = "Nước Uống",
                    MaLoai = "Old"
                }
            );
        }
    }
}
