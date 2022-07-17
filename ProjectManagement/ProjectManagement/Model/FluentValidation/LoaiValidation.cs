using FluentValidation;
using ProductsManagement.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Model.FluentValidation
{
    public class LoaiValidation : AbstractValidator<CreateLoaiDTO>
    {
        public LoaiValidation()
        {
            RuleFor(x => x.TenLoai).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Tên"))
                .MaximumLength(200).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Tên Loại", "200"))
                .MinimumLength(2).WithMessage(string.Format(Resource.VALIDATION_MIN_LENGTH, "Mã Loại", "1"));

            RuleFor(x => x.MaLoai).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Mã"))
                .MaximumLength(200).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Mã Loại", "200"))
                .MinimumLength(2).WithMessage(string.Format(Resource.VALIDATION_MIN_LENGTH, "Mã Loại", "2"));
        }
    }
}
