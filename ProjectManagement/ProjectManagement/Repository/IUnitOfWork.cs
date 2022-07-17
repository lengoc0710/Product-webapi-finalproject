using ProductsManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Loai> Loais { get; }
        IGenericRepository<HangHoa> HangHoas { get; }
        Task Save();
    }
}
