using LeHotelMax.Application.Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace LeHotelMax.Application.Common.Models
{
    public struct Error
    {
        public ErrorCode Code { get; set; }
        public required string Message { get; set; }

        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is not Error) { return false; }
            return Code == ((Error)obj).Code &&
                   Message == ((Error)obj).Message;
        }

        public override readonly int GetHashCode()
        {
            return Message.GetHashCode() ^ Message.GetHashCode();
        }

        public static bool operator ==(Error left, Error right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Error left, Error right)
        {
            return !(left == right);
        }
    }
}
