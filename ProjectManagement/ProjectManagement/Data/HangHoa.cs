using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Data
{
    public class HangHoa
    {
        public int Id { get; set; }
        public string TenHangHoa { get; set; }

        public string MoTa { get; set; }
        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public int LoaiId { get; set; }
        public Loai Loai { get; set; }
    }
}
