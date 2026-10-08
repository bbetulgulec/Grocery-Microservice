using FluentValidation;
using manav.catalog.Entities;
using MongoDB.Bson;

namespace manav.catalog.Validators;

public class ProductDetailValidator : AbstractValidator<ProductDetail>
{
    public ProductDetailValidator()
    {
        RuleFor(x => x.ProductDetailDescription)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.ProductDetailInfo)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.ProductID)
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("Valid a ProductDetailID in enter");
    }
}