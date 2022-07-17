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
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HangHoaController> _logger;
        private readonly IMapper _mapper;
        public HangHoaRepository(IUnitOfWork unitOfWork, ILogger<HangHoaController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> CreateHangHoa(CreateHangHoaDTO hangHoaDTO)
        {
            var hangHoa = _mapper.Map<HangHoa>(hangHoaDTO);
            await _unitOfWork.HangHoas.Insert(hangHoa);
            await _unitOfWork.Save();
            return new
            {
                id = hangHoa.Id,
                hangHoa
            };
        }

        public async Task<string> DeleteHangHoa(int id)
        {
            var hangHoa = await _unitOfWork.HangHoas.Get(q => q.Id == id);
            if (hangHoa == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteHangHoa)}");
                throw new BusinessException(Resource.NOT_DATA);
            }

            await _unitOfWork.HangHoas.Delete(id);
            await _unitOfWork.Save();
            return Resource.DELETE_SUCCESS;
        }

        public async Task<object> GetHangHoa(int id)
        {
            var hangHoa= await _unitOfWork.HangHoas.Get(q => q.Id == id, include: q => q.Include(x => x.Loai));
            var result = _mapper.Map<HangHoaDTO>(hangHoa);
            return new
            {
                result
            };
        }

        public async Task<object> GetHangHoas(PagingParams requestParams)
        {
            var hangHoas = await _unitOfWork.HangHoas.GetPagedList(requestParams);
            var results = _mapper.Map<IList<HangHoaDTO>>(hangHoas);
            return new
            {
                results
            };
        }

        public async Task<string> UpdateHangHoa(int id, UpdateHangHoaDTO hotelDTO)
        {
            var hangHoa = await _unitOfWork.HangHoas.Get(q => q.Id == id);
            if (hangHoa == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateHangHoa)}");
                throw new BusinessException(Resource.NOT_DATA);
            }

            _mapper.Map(hotelDTO, hangHoa);
            _unitOfWork.HangHoas.Update(hangHoa);
            await _unitOfWork.Save();
            return Resource.UPDATE_SUCCESS;
        }
    }
}
