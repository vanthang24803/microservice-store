namespace Product.Core.Utils
{
    public class QueryObject
    {
        public string? Name { get; set; } = null;

        public string? Category { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public string? Filter { get; set; } = null;

        public string? Status { get; set; } = null;

        public int Limit { get; set; }

        public int Page { get; set; }

    }
}