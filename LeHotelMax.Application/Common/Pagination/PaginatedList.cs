using LeHotelMax.Application.Common.Interfaces;


namespace LeHotelMax.Application.Common.Pagination
{
    public class PaginatedList<T>(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize) : IPaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; set; } = items;
        public int PageNumber { get; } = pageNumber;
        public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);
        public int TotalCount { get; } = count;
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

    }
}
