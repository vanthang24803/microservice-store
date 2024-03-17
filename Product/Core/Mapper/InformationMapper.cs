using Product.Core.Dtos.Information;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public class InformationMapper
    {
        public static Information MapFromCreate(CreateInformation createInformation, Guid productId)
        {
            return new Information
            {
                Gift = createInformation.Gift,
                ISBN = createInformation.ISBN,
                Price = createInformation.Price,
                Format = createInformation.Format,
                Author = createInformation.Author,
                Company = createInformation.Company,
                Category = createInformation.Category,
                Released = createInformation.Released,
                Introduce = createInformation.Introduce,
                Publisher = createInformation.Publisher,
                Translator = createInformation.Translator,
                NumberOfPage = createInformation.NumberOfPage,
                BookId = productId,
            };
        }

        public static Information MapFromUpdate(UpdateInformation updateInformation)
        {
            return new Information
            {
                Gift = updateInformation.Gift,
                ISBN = updateInformation.ISBN,
                Price = updateInformation.Price,
                Format = updateInformation.Format,
                Author = updateInformation.Author,
                Company = updateInformation.Company,
                Category = updateInformation.Category,
                Released = updateInformation.Released,
                Introduce = updateInformation.Introduce,
                Publisher = updateInformation.Publisher,
                Translator = updateInformation.Translator,
                NumberOfPage = updateInformation.NumberOfPage,
            };
        }
    }
}