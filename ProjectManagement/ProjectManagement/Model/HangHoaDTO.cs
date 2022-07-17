using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Model
{
    public class CreateHangHoaDTO
    {
        public string TenHangHoa { get; set; }
        public string MoTa { get; set; }
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }
        public int LoaiId { get; set; }
    }

    public class UpdateHangHoaDTO : CreateHangHoaDTO
    {

    }

    public class HangHoaDTO : CreateHangHoaDTO
    {
        public int Id { get; set; }
        public LoaiDTO Loai { get; set; }
    }
}
