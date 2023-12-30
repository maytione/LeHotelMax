using FluentValidation;
using LeHotelMax.Application.Hotels.Query;

namespace LeHotelMax.Application.Hotels.Validators
{
    public class SearchForHotelsQueryValidator : AbstractValidator<SearchForHotelsQuery>
    {
        public SearchForHotelsQueryValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(0).WithMessage("PageNumber number must be greater or equal to 0");

            RuleFor(p => p.PageSize)
               .GreaterThan(0).WithMessage("PageSize number must be greater than 0");

            RuleFor(x => x.Latitude)
               .InclusiveBetween(-90, 90)
               .WithMessage("Latitude must be between -90 and 90 degrees");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180 degrees");


        }
    }
}
