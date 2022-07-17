using ProductsManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Services
{
    public interface ILoaiRepository
    {

        Task<object> GetLoais(PagingParams requestParams);
        Task<object> GetLoai(int id);
        Task<object> CreateLoai(CreateLoaiDTO countryDTO);
        Task<string> UpdateLoai(int id, UpdateLoaiDTO countryDTO);
        Task<string> DeleteLoai(int id);
    }
}
