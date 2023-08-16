using FluentValidation;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Domain.Lookups.Validators
{
    public class SkillSearchFilterValidator : AbstractValidator<SkillSearchFilter>
    {
        #region Constructor
        public SkillSearchFilterValidator()
        {
            RuleFor(x => x.NameContains).Length(3, 50).When(x => !string.IsNullOrEmpty(x.NameContains));
            RuleFor(x => x.PaginationEnabled).Equal(true).WithMessage("Pagination required");
            RuleFor(x => x.PageNumber).NotNull().GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.PageSize).NotNull().GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than 0");
        }
        #endregion
    }
}
