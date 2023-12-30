
namespace LeHotelMax.Domain.Exceptions
{
    public abstract class DomainModelInvalidException : Exception
    {
        internal DomainModelInvalidException()
        {
            ValidationErrors = [];
        }

        internal DomainModelInvalidException(string message) : base(message)
        {
            ValidationErrors = [];
        }

        internal DomainModelInvalidException(string message, Exception inner) : base(message, inner)
        {
            ValidationErrors = [];
        }
        public List<string> ValidationErrors { get; }
    }
}
