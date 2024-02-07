using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Option;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public class OptionMapper
    {
        public static Options MapFromDto(CreateOptionsDto createOptionsDto, Guid id)
        {
            return new Options
            {
                Name = createOptionsDto.Name,
                Sale = createOptionsDto.Sale,
                Price = createOptionsDto.Price,
                Quantity = createOptionsDto.Quantity,
                BookId = id,
            };
        }
    }
}