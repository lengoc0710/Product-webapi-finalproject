using FluentValidation;
using ProductsManagement.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Model.FluentValidation
{
   
        public class CreateHangHoaValidation : AbstractValidator<CreateHangHoaDTO>
        {
            public CreateHangHoaValidation()
            {
                RuleFor(x => x.MoTa).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Mô Tả"))
                    .MinimumLength(2).WithMessage(string.Format(Resource.VALIDATION_MIN_LENGTH, "Mô Tả", "2"))
                    .MaximumLength(200).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Mô Tả", "200"));

                RuleFor(x => x.TenHangHoa).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Tên Hàng Hóa"))
                    .MinimumLength(2).WithMessage(string.Format(Resource.VALIDATION_MIN_LENGTH, "Tên Hàng Hóa", "1"))
                    .MaximumLength(200).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Tên Hàng Hóa ", "200"));
            }
        }
    
}
