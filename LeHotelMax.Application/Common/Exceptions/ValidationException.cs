using FluentValidation.Results;

namespace LeHotelMax.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Validations error(s)")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }

}
