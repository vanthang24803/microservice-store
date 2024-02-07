using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Book;
using Product.Core.Dtos.Category;
using Product.Core.Dtos.Image;
using Product.Core.Dtos.Option;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public static class BookMapper
    {
        public static Book MapFromDto(CreateBookDto createBookDto)
        {
            return new Book
            {
                Name = createBookDto.Name,
                Brand = createBookDto.Brand,
                Options = createBookDto.Options.Select(o => new Options
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Price,
                    Sale = o.Sale,
                    Status = o.Status,
                    Quantity = o.Quantity,
                    CreateAt = o.CreateAt,
                    UpdateAt = o.UpdateAt,
                }).ToList(),
                Thumbnail = createBookDto.Thumbnail,
                CreateAt = createBookDto.CreateAt,
                UpdateAt = createBookDto.UpdateAt
            };
        }

        public static BookDto MapToDto(Book product)
        {
            return new BookDto
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Thumbnail = product.Thumbnail,
                Categories = product.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreateAt = c.CreateAt,
                    UpdateAt = c.UpdateAt,
                }).ToList(),
                Images = product.Images.Select(i => new ImageDto
                {
                    Id = i.Id,
                    Url = i.Url,
                    CreateAt = i.CreateAt,
                    UpdateAt = i.UpdateAt,
                }).ToList(),
                Options = product.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Price,
                    Sale = o.Sale,
                    Status = o.Status,
                    Quantity = o.Quantity,
                    CreateAt = o.CreateAt,
                    UpdateAt = o.UpdateAt,
                }).ToList(),
                CreateAt = product.CreateAt,
                UpdateAt = product.UpdateAt,
            };
        }
    }

}