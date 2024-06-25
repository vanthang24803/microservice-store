using System.Net;
using Product.Core.Common.Utils;
using Product.Core.Dtos.Voucher;
using Product.Core.Interfaces;
using Product.Core.Models;
using Product.Core.Repositories;

namespace Product.Core.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<string> CheckVoucherExpiryAsync()
        {
            return await _voucherRepository.CheckVoucherExpiry();
        }


        public async Task<Response<Voucher>> CreateAsync(CreateVoucher createVoucher)
        {
            var voucher = await _voucherRepository.Save(createVoucher);

            return new Response<Voucher>(HttpStatusCode.Created, voucher);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            return await _voucherRepository.Delete(id);
        }

        public async Task<Response<Voucher>> ExtendAsync(Guid id, ExtendVoucher extendVoucher)
        {
            var exitingVoucher = await _voucherRepository.Extend(id, extendVoucher);
            return new Response<Voucher>(HttpStatusCode.OK, exitingVoucher);
        }

        public async Task<Response<Voucher>> FindVoucherByCodeAsync(VoucherRequest voucher)
        {
            var exitingVoucher = await _voucherRepository.FindVoucherByCode(voucher);
            return new Response<Voucher>(HttpStatusCode.OK, exitingVoucher);
        }

        public async Task<Voucher> FindVoucherByIdAsync(Guid id)
        {
            return await _voucherRepository.FindVoucherById(id);
        }

        public async Task<List<Voucher>> GetAsync()
        {
            return await _voucherRepository.FindAll();
        }

        public async Task<Response<Voucher>> UpdateAsync(Guid id, UpdateVoucher updateVoucher)
        {
            var voucher = await _voucherRepository.Update(id, updateVoucher);

            return new Response<Voucher>(HttpStatusCode.OK, voucher);
        }

        public async Task<string> UseAsync(UseVoucher useVoucher)
        {
            return await _voucherRepository.Use(useVoucher);
        }
    }
}