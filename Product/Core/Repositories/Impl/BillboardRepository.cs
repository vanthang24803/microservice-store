
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Common.Exceptions;
using Product.Core.Domain.Dtos.Billboard;
using Product.Core.Interfaces;
using Product.Core.Models;

namespace Product.Core.Repositories.Impl
{
    public class BillboardRepository : IBillboardRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IUploadService _uploadService;

        public BillboardRepository(ApplicationDbContext context, IUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public async Task<string> Delete(Guid id)
        {

            var exitingBillboard = await this.GetBillboardById(id);

            _context.Billboards.Remove(exitingBillboard);

            await _context.SaveChangesAsync();

            return "OK";
        }

        public async Task<List<Billboard>> FindAll()
        {
            return await _context.Billboards.ToListAsync();
        }

        public Task<Billboard> FindDetail(Guid id)
        {
            return this.GetBillboardById(id);
        }

        public async Task<Billboard> Save(BillboardDto createBillboard, IFormFile file)
        {
            var result = await _uploadService.AddPhotoAsync(file);

            if (result.Error != null)
            {
                throw new BadRequestException(result.Error.Message);
            }

            var billboard = new Billboard
            {
                Thumbnail = result.SecureUrl.AbsoluteUri,
                Url = createBillboard.Url,
            };

            _context.Billboards.Add(billboard);

            await _context.SaveChangesAsync();

            return billboard;
        }

        public async Task<Billboard> Update(Guid id, BillboardDto updateBillboard, IFormFile file)
        {

            var exitingBillboard = await this.GetBillboardById(id);

            var result = await _uploadService.AddPhotoAsync(file);

            if (result.Error != null)
            {
                throw new BadRequestException(result.Error.Message);
            }
            exitingBillboard.Url = updateBillboard.Url;
            exitingBillboard.Thumbnail = result.SecureUrl.AbsoluteUri;
            exitingBillboard.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return exitingBillboard;
        }

        private async Task<Billboard> GetBillboardById(Guid id)
        {
            return await _context.Billboards.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFiniteNumberException("Billboard not found!");
        }
    }
}