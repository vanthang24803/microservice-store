using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Billboard;
using Product.Core.Interfaces;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class BillboardService : IBillboardService
    {
        private readonly ApplicationDbContext _context;

        private readonly IUploadService _upload;

        public BillboardService(ApplicationDbContext context, IUploadService upload)
        {
            _context = context;
            _upload = upload;
        }
        public async Task<ResponseDto> CreateAsync(CreateBillboard createBillboard, IFormFile file)
        {

            var result = await _upload.AddPhotoAsync(file);

            if (result.Error != null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = result.Error.Message
                };
            }

            var Billboard = new Billboard
            {
                Thumbnail = result.SecureUrl.AbsoluteUri,
                Url = createBillboard.Url,
            };

            _context.Billboards.Add(Billboard);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Billboard created successfully!"
            };
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var exitingBillboard = await _context.Billboards.FindAsync(id);
            if (exitingBillboard is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Billboard not found!"
                };
            }

            _context.Billboards.Remove(exitingBillboard);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Billboard deleted successfully!"
            };
        }

        public async Task<List<Billboard>> GetAsync()
        {
            return await _context.Billboards.ToListAsync();
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateBillboard updateBillboard, IFormFile file)
        {
            var exitingBillboard = await _context.Billboards.FindAsync(id);
            if (exitingBillboard is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Billboard not found!"
                };
            }

            var result = await _upload.AddPhotoAsync(file);

            if (result.Error != null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = result.Error.Message
                };
            }

            exitingBillboard.Url = updateBillboard.Url;
            exitingBillboard.Thumbnail = result.SecureUrl.AbsoluteUri;
            exitingBillboard.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Billboard updated successfully!"
            };

        }
    }
}