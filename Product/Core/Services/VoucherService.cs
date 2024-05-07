using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Response;
using Product.Core.Dtos.Voucher;
using Product.Core.Interfaces;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly ApplicationDbContext _context;

        public VoucherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> CheckVoucherExpiryAsync()
        {
            var vouchers = _context.Vouchers;

            foreach (var voucher in vouchers)
            {
                voucher.Expire = voucher.ShelfLife <= DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Check voucher successfully"
            };
        }


        public async Task<ResponseDto> CreateAsync(CreateVoucher createVoucher)
        {
            var Voucher = VoucherMapper.MapFromDto(createVoucher);

            _context.Vouchers.Add(Voucher);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Voucher created successfully"
            };
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var exitingVoucher = await _context.Vouchers.FindAsync(id);

            if (exitingVoucher is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Voucher not found"
                };
            }

            _context.Vouchers.Remove(exitingVoucher);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Voucher deleted successfully"
            };
        }

        public async Task<ResponseDto> ExtendAsync(Guid id, ExtendVoucher extendVoucher)
        {
            var exitingVoucher = await _context.Vouchers.FindAsync(id);

            if (exitingVoucher is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Voucher not found"
                };
            }

            exitingVoucher.Day = extendVoucher.Day;
            exitingVoucher.ShelfLife = exitingVoucher.ShelfLife.AddDays(exitingVoucher.Day);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Extend voucher successfully"
            };
        }

        public async Task<IResponse> FindVoucherByCodeAsync(VoucherRequest voucher)
        {
            var exitingVoucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == voucher.Code);

            if (exitingVoucher == null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Voucher not found!"
                };
            }

            if (exitingVoucher.ShelfLife <= DateTime.Now)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Voucher expired!"
                };
            }

            return new VoucherResponse()
            {
                IsSucceed = true,
                Voucher = exitingVoucher,
            };

        }

        public async Task<Voucher?> FindVoucherById(Guid id)
        {
            var exitingVoucher = await _context.Vouchers.FindAsync(id);

            if (exitingVoucher == null)
            {
                return null;
            }

            return exitingVoucher;
        }

        public async Task<List<Voucher>> GetAsync()
        {
            return await _context.Vouchers.ToListAsync();
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateVoucher updateVoucher)
        {
            var exitingVoucher = await _context.Vouchers.FindAsync(id);

            if (exitingVoucher is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Voucher not found"
                };
            }

            exitingVoucher.Name = updateVoucher.Name;
            exitingVoucher.Title = updateVoucher.Title;
            exitingVoucher.Quantity = updateVoucher.Quantity;
            exitingVoucher.Type = updateVoucher.Type;
            exitingVoucher.Day = updateVoucher.Day;
            exitingVoucher.ShelfLife = updateVoucher.ShelfLife;
            exitingVoucher.Discount = updateVoucher.Discount;
            exitingVoucher.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Voucher updated successfully"
            };
        }

        public async Task<ResponseDto> UseAsync(UseVoucher useVoucher)
        {
            var exitingVoucher = await _context.Vouchers.FirstOrDefaultAsync(c => c.Code == useVoucher.Code);

            if (exitingVoucher is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Voucher not found"
                };
            }

            if (exitingVoucher.ShelfLife > DateTime.UtcNow && exitingVoucher.Quantity > 0)
            {
                exitingVoucher.Quantity -= 1;

                await _context.SaveChangesAsync();

                return new ResponseDto()
                {
                    IsSucceed = true,
                    Message = "Voucher used successfully"
                };
            }

            return new ResponseDto()
            {
                IsSucceed = false,
                Message = "Voucher has expired or is out of stock"
            };
        }
    }
}