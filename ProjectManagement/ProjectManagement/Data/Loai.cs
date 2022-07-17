using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Data
{
    public class Loai
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public string MaLoai { get; set; }

        public virtual IList<HangHoa> HangHoas { get; set; }
    }
}
