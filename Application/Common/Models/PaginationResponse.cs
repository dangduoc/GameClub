

namespace Application.Common.Models
{
    public class PaginationResponse<T>
    {
        public PaginationResponse()
        {

        }
        public IList<T>? Items { set; get; } = [];
        public int Total { set; get; }
        public int Start { set; get; }
        public int Limit { set; get; }
        public PaginationResponse(IList<T> Items, int Total, int Start, int Limit)
        {
            this.Total = Total;
            this.Items = Items;
            this.Start = Start;
            this.Limit = Limit;
        }
        public static async Task<PaginationResponse<T>> FromQuery<TEntity>(
            IQueryable<TEntity> entities,
            Func<TEntity, T> selector,
            int Start, int Limit,
            CancellationToken cancellationToken=default
            ) where TEntity : class
        {
            Limit = Limit == 0 ? 50 : Limit;
            var count = await entities.CountAsync(cancellationToken);
            var items = await entities.Skip(Start).Take(Limit).ToListAsync(cancellationToken);
            return new PaginationResponse<T>()
            {
                Items = items.Select(selector).ToList(),
                Total = count,
                Start = Start,
                Limit = Limit
            };
        }

        public static PaginationResponse<T> Empty(PaginationRequest request)
        {
            return new PaginationResponse<T>()
            {
                Items = new List<T>(),
                Total = 0,
                Start = request.Start,
                Limit = request.Limit
            };
        }
    }
}
