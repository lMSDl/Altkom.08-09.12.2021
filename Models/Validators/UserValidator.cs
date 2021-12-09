using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Password).NotNull().NotEmpty()
                .MinimumLength(8)
                .WithMessage("Za krótkie!")
                .Must(x => x.Contains("#"))
                .WithMessage("Hasło musi zawierać znak specjalny #")
                .WithName("Hasło");
            RuleFor(x => x.Role).IsInEnum();
        }
    }
}
