using FluentValidation;
using Yoma.Core.Domain.Opportunity.Models;

namespace Yoma.Core.Domain.Opportunity.Validators
{
    public class OpportunitySearchFilterValidator : AbstractValidator<OpportunitySearchFilter>
    {
        #region Constructor
        public OpportunitySearchFilterValidator()
        {
            RuleFor(model => model.EndDate).GreaterThanOrEqualTo(model => model.StartDate).When(model => model.EndDate.HasValue && model.StartDate.HasValue).WithMessage("{PropertyName} is earlier than the Start Date.");
            RuleFor(x => x.OrganizationId).NotEqual(Guid.Empty).When(x => x.OrganizationId.HasValue).WithMessage("{PropertyName} is an empty value.");
            RuleFor(x => x.TypeIds).Must(x => x == null || x.All(id => id != Guid.Empty)).WithMessage("{PropertyName} contains empty value(s).");
            RuleFor(x => x.CategoryIds).Must(x => x == null || x.All(id => id != Guid.Empty)).WithMessage("{PropertyName} contains empty value(s).");
            RuleFor(x => x.LanguageIds).Must(x => x == null || x.All(id => id != Guid.Empty)).WithMessage("{PropertyName} contains empty value(s).");
            RuleFor(x => x.CountryIds).Must(x => x == null || x.All(id => id != Guid.Empty)).WithMessage("{PropertyName} contains empty value(s).");
            RuleFor(x => x.StatusIds).Must(x => x == null || x.All(id => id != Guid.Empty)).WithMessage("{PropertyName} contains empty value(s).");
            RuleFor(x => x.ValueContains).Length(3, 50).When(x => !string.IsNullOrEmpty(x.ValueContains));
            RuleFor(x => x.PageNumber).NotNull().GreaterThanOrEqualTo(1).When(x => x.PageNumber.HasValue || x.PaginationEnabled).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.PageSize).NotNull().GreaterThanOrEqualTo(1).When(x => x.PageSize.HasValue || x.PaginationEnabled).WithMessage("{PropertyName} must be greater than 0");
        }
        #endregion
    }
}
