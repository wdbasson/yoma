using Newtonsoft.Json;

namespace Yoma.Core.Domain.Core.Models
{
    public class FilterPagination
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        [JsonIgnore]
        internal bool PaginationEnabled => PageSize.HasValue || PageNumber.HasValue;
    }
}
