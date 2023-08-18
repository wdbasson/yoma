using FluentValidation;
using Yoma.Core.Domain.Core.Validators;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Domain.Lookups.Validators
{
    public class SkillSearchFilterValidator : PaginationFilterValidator<SkillSearchFilter>
    {
        #region Constructor
        public SkillSearchFilterValidator()
        {
            RuleFor(x => x.NameContains).Length(3, 50).When(x => !string.IsNullOrEmpty(x.NameContains));
            RuleFor(x => x.PaginationEnabled).Equal(true).WithMessage("Pagination required");
        }
        #endregion
    }
}
