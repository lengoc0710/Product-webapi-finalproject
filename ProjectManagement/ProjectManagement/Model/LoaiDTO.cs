using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Model
{
    public class CreateLoaiDTO
    {
        public string TenLoai { get; set; }
        public string MaLoai { get; set; }
    }
    public class UpdateLoaiDTO : CreateLoaiDTO
    {
        public IList<CreateLoaiDTO> Hotels { get; set; }
    }

    public class LoaiDTO : CreateLoaiDTO
    {
        public int Id { get; set; }
        public IList<HangHoaDTO> Hotels { get; set; }
    }
}
