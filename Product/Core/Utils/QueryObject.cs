using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Utils
{
    public class QueryObject
    {
        public string? Name { get; set; } = null;

        public string? Category { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public string? Status { get; set; } = null;

        public int Limit { get; set; } = 20;

        public int Page { get; set; } = 1;

    }
}