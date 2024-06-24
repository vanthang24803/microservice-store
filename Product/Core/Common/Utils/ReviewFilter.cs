using Product.Core.Models;

namespace Product.Core.Utils
{
    public class ReviewFilter
    {
        public List<Reviews> ApplyFilters(List<Reviews> reviews, QueryReview query)
        {
            // Filter By Date
            if (!string.IsNullOrEmpty(query.Status))
            {
                if (query.Status == "Lasted")
                {
                    reviews = [.. reviews.OrderByDescending(n => n.CreateAt)];
                }

                if (query.Status == "Image")
                {
                    reviews = reviews.Where(n => n.Images.Count != 0).ToList();
                }
            }

            //  Filter By Star
            if (!string.IsNullOrEmpty(query.Star) && int.TryParse(query.Star, out int starValue))
            {
                reviews = reviews.Where(n => n.Star == starValue).ToList();
            }


            return reviews;
        }
    }
}