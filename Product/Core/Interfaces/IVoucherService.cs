using Product.Core.Common.Utils;
using Product.Core.Dtos.Response;
using Product.Core.Dtos.Voucher;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IVoucherService
    {
        Task<Response<Voucher>> CreateAsync(CreateVoucher createVoucher);

        Task<List<Voucher>> GetAsync();

        Task<string> UseAsync(UseVoucher useVoucher);

        Task<string> DeleteAsync(Guid id);

        Task<Response<Voucher>> FindVoucherByCodeAsync(VoucherRequest voucher);
        Task<Voucher> FindVoucherByIdAsync(Guid id);

        Task<Response<Voucher>> UpdateAsync(Guid id, UpdateVoucher updateVoucher);

       Task<Response<Voucher>> ExtendAsync(Guid id, ExtendVoucher extendVoucher);

        Task<string> CheckVoucherExpiryAsync();
    }
}