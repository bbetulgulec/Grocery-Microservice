using FluentValidation;
using manav.catalog.Entities;

namespace manav.catalog.Validators;

public class AboutValidator : AbstractValidator<About>
{
    public AboutValidator()
    {
        RuleFor(x => x.AboutDescription)
            .NotEmpty().WithMessage("Hakkında açıklaması boş bırakılamaz.")
            .MaximumLength(1000).WithMessage("Açıklama en fazla 500 karakter olmalıdır.");

        RuleFor(x => x.AboutWorkTime)
            .NotEmpty().WithMessage("Çalışma saatleri alanı boş bırakılamaz.")
            .MaximumLength(100).WithMessage("Çalışma saatleri bilgisi en fazla 100 karakter olmalıdır.");

        RuleFor(x => x.AboutNumber)
            .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
            .Matches(@"^\+?[0-9\s\-\(\)]{7,15}$").WithMessage("Lütfen geçerli bir telefon numarası giriniz.");

        RuleFor(x => x.AboutAdress)
            .NotEmpty().WithMessage("Adres alanı boş bırakılamaz.")
            .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olmalıdır.");

        RuleFor(x => x.AboutMail)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.");
    }
}