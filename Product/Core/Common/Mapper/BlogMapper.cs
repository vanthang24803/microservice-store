using Product.Core.Dtos.Blogs;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public class BlogMapper
    {
        public static Blog MapFromDto(BlogDto blog)
        {
            return new Blog
            {
                Title = blog.Title,
                Content = blog.Content,
                Thumbnail = blog.Thumbnail,
                AuthorId = blog.AuthorId,
                AuthorName = blog.AuthorName,
                AuthorAvatar = blog.AuthorAvatar
            };
        }
    }
}