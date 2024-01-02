using FluentValidation;
using LeHotelMax.Application.Hotels.Command;

namespace LeHotelMax.Application.Hotels.Validators
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Hotel name can't be null")
                .NotEmpty().WithMessage("Hotel name content can't be empty")
                .MinimumLength(1).WithMessage("Hotel name can't be that short");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Hotel price must be greater than 0");

            RuleFor(p => p.GeoLocation!)
                 .NotNull().WithMessage("Hotel Geo Location must be provided")
                 .SetValidator(new GeoLocationValidator());
            

        }
    }
}
