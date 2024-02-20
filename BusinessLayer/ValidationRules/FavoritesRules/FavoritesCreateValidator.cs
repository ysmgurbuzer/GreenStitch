using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FavoritesRules
{
    public class FavoritesCreateValidator:AbstractValidator<FavoritesCreateDto>
    {
        public FavoritesCreateValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();
        }
    }
}
