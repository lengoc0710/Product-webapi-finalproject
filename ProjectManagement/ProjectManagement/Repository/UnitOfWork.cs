using ProductsManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Repository
{
    
        public class UnitOfWork : IUnitOfWork
        {
            private readonly DatabaseContext _context;
            private IGenericRepository<HangHoa> _hangHoas;
        private IGenericRepository<Loai> _loais;

            public UnitOfWork(DatabaseContext context)
            {
                _context = context;
            }
            public IGenericRepository<Loai> Loais => _loais ??= new GenericRepository<Loai>(_context);
            public IGenericRepository<HangHoa> HangHoas => _hangHoas ??= new GenericRepository<HangHoa>(_context);

            public void Dispose()
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }

            public async Task Save()
            {
                await _context.SaveChangesAsync();
            }
        }
}
