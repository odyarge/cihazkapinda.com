using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfoAppService : CrudAppService<ProductInfo, ProductInfoDto, Guid, PagedAndSortedResultRequestDto, ProductInfoCreateUpdateDto, ProductInfoCreateUpdateDto>, IProductInfoAppService
    {
        private readonly IRepository<ProductInfoTemplate, Guid> _productInfoTemplateRepository;
        public ProductInfoAppService(IRepository<ProductInfo, Guid> repository, IRepository<ProductInfoTemplate, Guid> productInfoTemplateRepository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Products.ProductsDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Products.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Products.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Products.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Products.Delete;

            _productInfoTemplateRepository = productInfoTemplateRepository;
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<List<ProductInfoDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<ProductInfo>, List<ProductInfoDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<List<ProductInfoDto>> GetAllListWithDetail(Guid id)
        {
            var list = await Repository.GetListAsync();
            var filterList = list.FindAll(x => x.ProductId == id);
            
            foreach (var item in filterList)
            {
                item.productInfoTemplate = await _productInfoTemplateRepository.SingleOrDefaultAsync(x => x.Id == item.ProductInfoTemplateId);
            }
            return ObjectMapper.Map<List<ProductInfo>, List<ProductInfoDto>>(filterList.OrderByDescending(x => x.CreationTime).ToList());
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<bool> GetControl(Guid id)
        {
            var check = await Repository.SingleOrDefaultAsync(x => x.Id == id);
            return check != null ? true : false;
        }
    }
}
