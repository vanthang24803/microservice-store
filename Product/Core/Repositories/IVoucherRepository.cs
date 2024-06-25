using Product.Core.Dtos.Voucher;
using Product.Core.Models;

namespace Product.Core.Repositories
{
    public interface IVoucherRepository
    {
        Task<string> Delete(Guid id);

        Task<Voucher> Save(CreateVoucher voucher);

        Task<List<Voucher>> FindAll();

        Task<Voucher> FindVoucherByCode(VoucherRequest voucher);
        Task<Voucher> FindVoucherById(Guid id);

        Task<Voucher> Update(Guid id, UpdateVoucher updateVoucher);

        Task<Voucher> Extend(Guid id, ExtendVoucher extendVoucher);

        Task<string> CheckVoucherExpiry();

        Task<string> Use(UseVoucher useVoucher);
    }
}