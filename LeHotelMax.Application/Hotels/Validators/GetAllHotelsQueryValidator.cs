using FluentValidation;
using LeHotelMax.Application.Hotels.Query;

namespace LeHotelMax.Application.Hotels.Validators
{
    public class GetAllHotelsQueryValidator : AbstractValidator<GetAllHotelsQuery>
    {
        public GetAllHotelsQueryValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(0).WithMessage("PageNumber number must be greater or equal to 0");

            RuleFor(p => p.PageSize)
               .GreaterThan(0).WithMessage("PageSize number must be greater than 0");

        }
    }
}
