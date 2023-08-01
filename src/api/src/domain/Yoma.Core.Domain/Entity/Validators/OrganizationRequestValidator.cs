using FluentValidation;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Lookups.Interfaces;

namespace Yoma.Core.Domain.Entity.Validators
{
    public class OrganizationRequestValidator : AbstractValidator<OrganizationRequest>
    {
        #region Class Variables
        private ICountryService _countryService;
        #endregion

        #region Constructor
        public OrganizationRequestValidator(ICountryService countryService)
        {
            _countryService = countryService;

            RuleFor(x => x.Name).NotEmpty().Length(1, 255);
            RuleFor(x => x.WebsiteURL).Length(1, 2048).Must(ValidURL).WithMessage("'{PropertyName}' is invalid.");
            RuleFor(x => x.PrimaryContactName).Length(0, 255);
            RuleFor(x => x.PrimaryContactName).Length(0, 320).EmailAddress();

            //TODO: Complete
        }
        #endregion

        #region Private Members
        private bool ValidURL(string? url)
        {
            if (url == null) return true;
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        private bool BeNotInFuture(DateTimeOffset? date)
        {
            if (!date.HasValue) return true;
            return date <= DateTimeOffset.Now;
        }

        private bool CountryExists(Guid? countryId)
        {
            if (!countryId.HasValue) return true;
            return _countryService.GetByIdOrNull(countryId.Value) != null;
        }
        #endregion
    }
}
