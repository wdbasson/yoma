using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Yoma.Core.Domain.Core.Models
{
    public class PaginationFilter
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        [JsonIgnore]
        [MemberNotNull(nameof(PageNumber), nameof(PageSize))]
        internal bool PaginationEnabled => PageSize.HasValue || PageNumber.HasValue;

    }
}
