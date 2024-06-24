using Product.Core.Enum;
using Product.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace Product.Core.Utils
{
    public class StatisticalFilter
    {
        private readonly Dictionary<string, Status> status;

        public StatisticalFilter()
        {
            status = new Dictionary<string, Status>
        {
            { "Pending" , Status.PENDING },
            { "Create" , Status.CREATE },
            { "Shipping" , Status.SHIPPING },
            { "Success" , Status.SUCCESS },
        };
        }

        public IQueryable<Order> ApplyFilters(IQueryable<Order> query, QueryObjectOrder queryObject)
        {
            // Filter by status
            if (!string.IsNullOrWhiteSpace(queryObject.Status) && status.TryGetValue(queryObject.Status, out Status value))
            {
                var statusValue = value;
                query = query.Where(o => o.Status == statusValue);
            }


            // Filter by month
            var month = DateTime.Now.Month;
            if (!string.IsNullOrWhiteSpace(queryObject.Month))
            {
                month = int.Parse(queryObject.Month);
            }
            query = query.Where(o => o.CreateAt.Month == month);



            return query;
        }
    }
}