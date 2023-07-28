using FluentValidation;
using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Domain.Entity.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.FirstName).Length(0, 320).NotNull().NotEmpty();
        }
    }
}
