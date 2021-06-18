namespace Hahn.ApplicationProcess.February2021.Domain.Validators
{
    using System;
    using FluentValidation;
    using Models;

    /// <summary>
    /// Contains the validations rules for the <see cref="Asset"/> entity
    /// </summary>
    public sealed class AssetValidator : AbstractValidator<Asset>
    {
        /// <summary>
        /// Creates a valid instance of <see cref="AssetValidator"/>
        /// </summary>
        public AssetValidator()
        {
            RuleFor(x => x.AssetName).MinimumLength(5);

            RuleFor(x => x.EmailAddressOfDepartment).EmailAddress();

            RuleFor(x => x.Department).IsInEnum();
            
            RuleFor(x => x.PurchaseDate)
                .GreaterThan(DateTimeOffset.Now.Subtract(TimeSpan.FromDays(366)))
                .WithMessage("PurchaseDate must not be older than 1 year");
        }
    }
}