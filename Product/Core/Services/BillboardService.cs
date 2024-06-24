using System.Net;
using Product.Core.Common.Utils;
using Product.Core.Domain.Dtos.Billboard;
using Product.Core.Interfaces;
using Product.Core.Models;
using Product.Core.Repositories;

namespace Product.Core.Services
{
    public class BillboardService : IBillboardService
    {
        private readonly IBillboardRepository _billboardRepository;

        public BillboardService(IBillboardRepository billboardRepository)
        {
            _billboardRepository = billboardRepository;
        }
        public async Task<Response<Billboard>> CreateAsync(BillboardDto createBillboard, IFormFile file)
        {

            var result = await _billboardRepository.Save(createBillboard, file);

            return new Response<Billboard>(HttpStatusCode.Created, result);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            return await _billboardRepository.Delete(id);
        }

        public async Task<List<Billboard>> GetAsync()
        {
            return await _billboardRepository.FindAll();
        }

        public async Task<Billboard> GetDetailAsync(Guid id)
        {
            return await _billboardRepository.FindDetail(id);
        }

        public async Task<Response<Billboard>> UpdateAsync(Guid id, BillboardDto updateBillboard, IFormFile file)
        {
            var result = await _billboardRepository.Update(id, updateBillboard, file);

            return new Response<Billboard>(HttpStatusCode.Created, result);

        }
    }
}