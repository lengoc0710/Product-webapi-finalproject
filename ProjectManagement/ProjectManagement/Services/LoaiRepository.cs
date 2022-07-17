using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsManagement.Controllers;
using ProductsManagement.Data;
using ProductsManagement.Model;
using ProductsManagement.Models;
using ProductsManagement.Properties;
using ProductsManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoaiController> _logger;
        private readonly IMapper _mapper;
        public LoaiRepository(IUnitOfWork unitOfWork, ILogger<LoaiController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> CreateLoai(CreateLoaiDTO loaiDTO)
        {
            var loai = _mapper.Map<Loai>(loaiDTO);
            await _unitOfWork.Loais.Insert(loai);
            await _unitOfWork.Save();
            return new
            {
                id = loai.Id,
                loai
            };
        }

        public async Task<string> DeleteLoai(int id)
        {
            var loai = await _unitOfWork.Loais.Get(q => q.Id == id);
            if (loai == null || id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteLoai)}");
                throw new BusinessException(Resource.NOT_DATA);
            }

            await _unitOfWork.Loais.Delete(id);
            await _unitOfWork.Save();
            return Resource.DELETE_SUCCESS;
        }

        public async Task<object> GetLoais(PagingParams pagingParams)
        {
            var loais = await _unitOfWork.Loais.GetPagedList(pagingParams);
            var results = _mapper.Map<IList<LoaiDTO>>(loais);
            return new
            {
                results
            };
        }

        public async Task<object> GetLoai(int id)
        {
            var loai = await _unitOfWork.Loais.Get(q => q.Id == id, include: q => q.Include(x => x.HangHoas));
            var result = _mapper.Map<LoaiDTO>(loai);
            return new
            {
                result
            };
        }

        public async Task<string> UpdateLoai(int id, UpdateLoaiDTO loaiDTO)
        {
            var loai = await _unitOfWork.Loais.Get(q => q.Id == id);
            if (loai == null)
            {
                throw new BusinessException(Resource.NOT_DATA);
            }

            _mapper.Map(loaiDTO, loai);
            _unitOfWork.Loais.Update(loai);
            await _unitOfWork.Save();
            return Resource.UPDATE_SUCCESS;
        }
    }
}
