using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Core.Dtos;
using Order.Core.Utils;

namespace Order.Core.Interfaces
{
    public interface IStatistical
    {
        Task<StatisticalDto> CreateAsync(QueryObject query);
    }
}