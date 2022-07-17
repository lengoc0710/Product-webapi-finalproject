using AutoMapper;
using ProductsManagement.Data;
using ProductsManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Loai, LoaiDTO>().ReverseMap();
            CreateMap<Loai, CreateLoaiDTO>().ReverseMap();
            CreateMap<Loai, UpdateLoaiDTO>().ReverseMap();
            CreateMap<HangHoa, HangHoaDTO>().ReverseMap();
            CreateMap<HangHoa, CreateHangHoaDTO>().ReverseMap();
            CreateMap<HangHoa, UpdateHangHoaDTO>().ReverseMap();
            CreateMap<ApiUserSystem, UserDTO>().ReverseMap();
        }
    }
}
