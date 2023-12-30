
namespace LeHotelMax.Domain.Common
{
    public abstract class BaseAuditable : BaseEntity
    {
        public DateTimeOffset Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
