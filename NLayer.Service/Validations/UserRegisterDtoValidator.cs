using FluentValidation;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDto>
    {
        private readonly IUserService _userService;
        public UserRegisterDtoValidator(IUserService userService)
        {
            _userService = userService;
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Phone).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("Invalid email format.")
                .Must(_userService.UniqueEmail).WithMessage("Such a {PropertyName} already exists ");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(8).WithMessage("'{PropertyName}' must min eight chracter.")
                .MaximumLength(15).WithMessage("'{PropertyName}' must max fifteen chracter.")
                .Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.");
                //.Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{PropertyName}' must contain one or more special characters.");
        }

    }
}
