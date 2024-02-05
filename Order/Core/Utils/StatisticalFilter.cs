using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.core.Enum;
using Order.Core.Dtos;

namespace Order.Core.Utils
{
    public class StatisticalFilter
    {
        private Dictionary<string, Status> status;

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

        public IQueryable<Order.core.Models.Order> ApplyFilters(IQueryable<Order.core.Models.Order> query, QueryObject queryObject)
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