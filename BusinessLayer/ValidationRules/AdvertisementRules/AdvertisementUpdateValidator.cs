using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.AdvertisementRules
{
    public class AdvertisementUpdateValidator:AbstractValidator<AdvertisementUpdateDto>
    {
        public AdvertisementUpdateValidator()
        {
            RuleFor(x => x.AdvertTitle).NotEmpty();
            RuleFor(x => x.AdvertTitle).MinimumLength(3);
        }
    }
}
