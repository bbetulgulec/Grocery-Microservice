using FluentValidation;
using manav.DtoLayer.CatalogDtos.AboutDtos;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Validators;

public class CreateAboutValidator : AbstractValidator<CreateAboutDto>
{
    public CreateAboutValidator(IStringLocalizer<SharedResource> l)
    {
        RuleFor(x => x.AboutDescription)
            .NotEmpty().WithMessage(_ => l["Validation_About_DescriptionRequired"])
            .MinimumLength(10).WithMessage(_ => l["Validation_About_DescriptionMinLength"]);

        RuleFor(x => x.AboutWorkTime)
            .NotEmpty().WithMessage(_ => l["Validation_About_WorkTimeRequired"]);

        RuleFor(x => x.AboutNumber)
            .NotEmpty().WithMessage(_ => l["Validation_About_PhoneRequired"]);

        RuleFor(x => x.AboutAdress)
            .NotEmpty().WithMessage(_ => l["Validation_About_AddressRequired"]);

        RuleFor(x => x.AboutMail)
            .NotEmpty().WithMessage(_ => l["Validation_About_MailRequired"])
            .EmailAddress().WithMessage(_ => l["Validation_About_MailInvalid"]);
    }
}