using Product.Core.Dtos.Voucher;
using Product.Core.Models;
using Product.Core.Utils;

namespace Product.Core.Mapper
{
    public class VoucherMapper
    {
        public static Voucher MapFromDto(CreateVoucher createVoucher)
        {
            return new Voucher
            {
                Name = createVoucher.Name,
                Title = createVoucher.Title,
                Code = RandomCode.Generate(),
                Quantity = createVoucher.Quantity,
                Day = createVoucher.Day,
                ShelfLife = createVoucher.CreateAt.AddDays(createVoucher.Day),
                CreateAt = createVoucher.CreateAt,
                UpdateAt = createVoucher.UpdateAt,
            };
        }
    }
}