using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public abstract class PaginationRequest
    {
        public PaginationRequest()
        {

        }
        public PaginationRequest(int Start, int Limit, string? SortBy = null, bool IsSortDesc = false)
        {
            this.Start = Start;
            this.Limit = Limit;
            this.SortBy = SortBy;
            this.IsSortDesc = IsSortDesc;
        }

        public int Start { get; init; } = 0;
        public int Limit { get; init; } = 50;
        public bool IsSortDesc { get; init; } = false;
        public string? SortBy { get; init; } = null;
        public string? SearchBy { get; init; } = null;
        public string? SearchText { get;init; }
    }
}
