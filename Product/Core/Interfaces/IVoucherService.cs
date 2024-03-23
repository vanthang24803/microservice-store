using Product.Core.Dtos.Response;
using Product.Core.Dtos.Voucher;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IVoucherService
    {
        Task<ResponseDto> CreateAsync(CreateVoucher createVoucher);

        Task<List<Voucher>> GetAsync();

        Task<ResponseDto> UseAsync(UseVoucher useVoucher);

        Task<ResponseDto> DeleteAsync(Guid id);

        Task<IResponse> FindVoucherByCodeAsync(VoucherRequest voucher);
        Task<Voucher?> FindVoucherById(Guid id);

        Task<ResponseDto> UpdateAsync(Guid id, UpdateVoucher updateVoucher);

        Task<ResponseDto> ExtendAsync(Guid id, ExtendVoucher extendVoucher);

        Task<ResponseDto> CheckVoucherExpiryAsync();
    }
}