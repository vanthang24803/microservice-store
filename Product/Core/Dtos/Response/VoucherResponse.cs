

namespace Product.Core.Dtos.Response
{
    public class VoucherResponse : IResponse
    {
        public bool IsSucceed { get; set; }

        public required Models.Voucher Voucher { get; set; }

    }
}