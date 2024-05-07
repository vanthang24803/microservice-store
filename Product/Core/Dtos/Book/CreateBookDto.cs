using Product.Core.Dtos.Option;

namespace Product.Core.Dtos.Book
{
    public class CreateBookDto
    {
        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public List<OptionDto> Options { get; set; } = [];

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}