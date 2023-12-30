
namespace LeHotelMax.Domain.Exceptions
{
    public class HotelNotValidException: DomainModelInvalidException
    {
        internal HotelNotValidException() { }
        internal HotelNotValidException(string message) : base(message) { }
        internal HotelNotValidException(string message, Exception inner) : base(message, inner) { }

    }
}
