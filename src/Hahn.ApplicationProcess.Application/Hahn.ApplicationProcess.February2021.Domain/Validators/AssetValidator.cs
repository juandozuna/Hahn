namespace Hahn.ApplicationProcess.February2021.Domain.Validators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Models;
    using Repositories;

    /// <summary>
    /// Contains the validations rules for the <see cref="Asset"/> entity
    /// </summary>
    public sealed class AssetValidator : AbstractValidator<Asset>
    {

        private readonly ICountryRepository _countryRepository;
        
        /// <summary>
        /// Creates a valid instance of <see cref="AssetValidator"/>
        /// </summary>
        public AssetValidator(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
            
            RuleFor(x => x.AssetName).MinimumLength(5);

            RuleFor(x => x.EmailAddressOfDepartment).EmailAddress();

            RuleFor(x => x.Department).IsInEnum();
            
            RuleFor(x => x.PurchaseDate)
                .GreaterThan(DateTimeOffset.Now.Subtract(TimeSpan.FromDays(366)))
                .WithMessage("PurchaseDate must not be older than 1 year");

            RuleFor(x => x.CountryOfDepartment)
                .MustAsync(ValidateCountry)
                .WithMessage("Enter a valid country");
        }

        private  Task<bool> ValidateCountry(string country, CancellationToken token)
            => _countryRepository.IsCountryValid(country);
        
    }
}