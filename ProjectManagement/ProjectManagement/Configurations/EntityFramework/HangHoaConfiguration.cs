using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Configurations.EntityFramework
{
    public class HangHoaConfiguration : IEntityTypeConfiguration<HangHoa>
    {
        public void Configure(EntityTypeBuilder<HangHoa> builder)
        {
            builder.HasData(
                new HangHoa
                {
                    Id = 1,
                    TenHangHoa = "Mì Vắn Thắn",
                    MoTa = "Sản phẩm mới được bày bán của cửa hàng",
                    LoaiId = 2,
                    DonGia = 30000,
                    GiamGia = 2
                },
                new HangHoa
                {
                    Id = 2,
                    TenHangHoa = "Bánh Mì Nướng Muối Ớt",
                    MoTa = "Sản  phẩm -Signature- của cửa hàng ",
                    LoaiId = 1,
                    DonGia = 13000,
                    GiamGia = 0
                },
                new HangHoa
                {
                    Id = 3,
                    TenHangHoa = "Sữa Đậu",
                    MoTa = "Sản phẩm dùng khi ăn kèm bữa sáng",
                    LoaiId = 3,
                    DonGia = 10000,
                    GiamGia = 1
                }
            );
        }
    }
}
