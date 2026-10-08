using FluentValidation;
using manav.catalog.Entities;
using MongoDB.Bson;

namespace manav.catalog.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ProductPrice)
            .GreaterThan(0)
            .PrecisionScale(18, 2, false);

        RuleFor(x => x.ProductStok)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoryID)
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Geçerli bir CategoryID girilmelidir.");
    }
}