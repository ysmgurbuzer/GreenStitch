using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FavoritesRules
{
    public class FavoritesUpdateValidator:AbstractValidator<FavoritesUpdateDto>
    {
        public FavoritesUpdateValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();
        }
    }
}
