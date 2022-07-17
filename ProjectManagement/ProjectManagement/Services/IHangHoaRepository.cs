using ProductsManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Services
{
    public interface IHangHoaRepository
    {
        Task<object> GetHangHoas(PagingParams pagingParams);
        Task<object> GetHangHoa(int id);
        Task<object> CreateHangHoa(CreateHangHoaDTO hotelDTO);
        Task<string> UpdateHangHoa(int id, UpdateHangHoaDTO hotelDTO);
        Task<string> DeleteHangHoa(int id);
    }
}
