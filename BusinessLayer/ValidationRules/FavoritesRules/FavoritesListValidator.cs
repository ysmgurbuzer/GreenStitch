﻿using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FavoritesRules
{
    public class FavoritesListValidator:AbstractValidator<FavoritesListDto>
    {
        public FavoritesListValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();
        }
    }
}
