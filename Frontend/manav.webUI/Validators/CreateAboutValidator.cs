using FluentValidation;
using manav.DtoLayer.CatalogDtos.AboutDtos;

namespace manav.webUI.Validators;

public class CreateAboutValidator : AbstractValidator<CreateAboutDto>
{
    public CreateAboutValidator()
    {
        RuleFor(x => x.AboutDescription)
            .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
            .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır.");

        RuleFor(x => x.AboutWorkTime)
            .NotEmpty().WithMessage("Çalışma saatleri alanı boş bırakılamaz.");

        RuleFor(x => x.AboutNumber)
            .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");

        RuleFor(x => x.AboutAdress)
            .NotEmpty().WithMessage("Adres alanı boş bırakılamaz.");

        RuleFor(x => x.AboutMail)
            .NotEmpty().WithMessage("Mail adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz.");
    }
}