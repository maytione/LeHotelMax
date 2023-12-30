
namespace LeHotelMax.Application.Common.Interfaces
{
    public interface IPaginatedList<T>
    {
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        IReadOnlyCollection<T> Items { get; set; }
        int PageNumber { get; }
        int TotalCount { get; }
        int TotalPages { get; }
    }
}
