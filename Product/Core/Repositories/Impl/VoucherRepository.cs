using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Common.Exceptions;
using Product.Core.Dtos.Voucher;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Repositories.Impl
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ApplicationDbContext _context;

        public VoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CheckVoucherExpiry()
        {
            var vouchers = _context.Vouchers;

            foreach (var voucher in vouchers)
            {
                voucher.Expire = voucher.ShelfLife <= DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return "Check voucher successfully";
        }

        public async Task<string> Delete(Guid id)
        {
            var exitingVoucher = await this.FindById(id);

            _context.Vouchers.Remove(exitingVoucher);

            await _context.SaveChangesAsync();

            return "Voucher deleted successfully";
        }

        public async Task<Voucher> Extend(Guid id, ExtendVoucher extendVoucher)
        {
            var exitingVoucher = await this.FindById(id);

            exitingVoucher.Day = extendVoucher.Day;
            exitingVoucher.ShelfLife = exitingVoucher.ShelfLife.AddDays(exitingVoucher.Day);

            await _context.SaveChangesAsync();

            return exitingVoucher;
        }

        public async Task<List<Voucher>> FindAll()
        {
            return await _context.Vouchers.OrderByDescending(x => x.CreateAt).ToListAsync();
        }

        public async Task<Voucher> FindVoucherByCode(VoucherRequest voucher)
        {
            return await this.FindVoucherByCode(voucher.Code);
        }

        public Task<Voucher> FindVoucherById(Guid id)
        {
            return this.FindById(id);
        }

        public async Task<Voucher> Save(CreateVoucher voucher)
        {
            var newVoucher = VoucherMapper.MapFromDto(voucher);

            _context.Vouchers.Add(newVoucher);
            await _context.SaveChangesAsync();

            return newVoucher;
        }

        public async Task<Voucher> Update(Guid id, UpdateVoucher updateVoucher)
        {
            var exitingVoucher = await this.FindById(id);

            exitingVoucher.Name = updateVoucher.Name;
            exitingVoucher.Title = updateVoucher.Title;
            exitingVoucher.Quantity = updateVoucher.Quantity;
            exitingVoucher.Type = updateVoucher.Type;
            exitingVoucher.Day = updateVoucher.Day;
            exitingVoucher.ShelfLife = updateVoucher.ShelfLife;
            exitingVoucher.Discount = updateVoucher.Discount;
            exitingVoucher.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return exitingVoucher;
        }

        public async Task<string> Use(UseVoucher useVoucher)
        {
            var exitingVoucher = await this.FindVoucherByCode(useVoucher.Code);

            if (exitingVoucher.ShelfLife > DateTime.UtcNow && exitingVoucher.Quantity > 0)
            {
                exitingVoucher.Quantity -= 1;

                await _context.SaveChangesAsync();

                return "Voucher used successfully";
            }
            else
            {
                throw new BadHttpRequestException("Voucher has expired or is out of stock");
            }
        }


        private async Task<Voucher> FindVoucherByCode(string code)
        {
            var exitingVoucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == code) ?? throw new NotFoundException("Voucher not found!");

            if (exitingVoucher.ShelfLife <= DateTime.Now)
            {
                throw new NotFoundException("Voucher expired!");
            }

            return exitingVoucher;
        }

        private async Task<Voucher> FindById(Guid id)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("Voucher not found!");
        }
    }
}