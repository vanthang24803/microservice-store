
using Product.Core.Dtos.Order;
using Product.Core.Utils;

namespace Product.Core.Interfaces
{
    public interface IStatistical
    {
        Task<StatisticalDto> CreateAsync(QueryObjectOrder query);
        
    }
}