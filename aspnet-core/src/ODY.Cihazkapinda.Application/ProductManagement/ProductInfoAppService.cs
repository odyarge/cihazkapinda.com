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
        private readonly IRepository<ProductInfoTemplate, Guid> _productInfoTemplateAppService;
        public ProductInfoAppService(IRepository<ProductInfo, Guid> repository, IRepository<ProductInfoTemplate, Guid> productInfoTemplateAppService) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Products.ProductsDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Products.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Products.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Products.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Products.Delete;

            _productInfoTemplateAppService = productInfoTemplateAppService;
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<List<ProductInfoDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<ProductInfo>, List<ProductInfoDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<List<ProductInfoDto>> GetAllListWithDetail()
        {
            var list = await Repository.GetListAsync();
            foreach (var item in list)
            {
                item.ProductInfoTemplate = await _productInfoTemplateAppService.GetAsync(item.ProductInfoTemplateId);
            }
            return ObjectMapper.Map<List<ProductInfo>, List<ProductInfoDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.ProductsDefault)]
        public async Task<ProductInfoDto> GetAsyncProductInfoWithDetail(Guid id)
        {
            var entity = await Repository.GetAsync(id);
            entity.ProductInfoTemplate = await _productInfoTemplateAppService.GetAsync(entity.ProductInfoTemplateId);

            return ObjectMapper.Map<ProductInfo, ProductInfoDto>(entity);
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.ProductsDefault)]
        public async Task<ProductInfoDto> GetAsyncProductInfoByTemplateId(Guid id)
        {
            var entity = await Repository.SingleOrDefaultAsync(x => x.ProductInfoTemplateId == id);
            return ObjectMapper.Map<ProductInfo, ProductInfoDto>(entity);
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.ProductsDefault)]
        public async Task<bool> GetAsyncProductInfoControl(Guid id)
        {
            var check = await Repository.SingleOrDefaultAsync(x => x.ProductInfoTemplateId == id);
            return check != null ? true : false;
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.ProductsDefault)]
        public async Task AllProductInfoPassive(Guid id)
        {
            var list = await Repository.GetListAsync();
            var infoList = list.FindAll(x => x.ProductId == id);
            if (infoList.Count > 0)
            {
                foreach (var item in infoList)
                {
                    item.Active = false;
                    //var update = ObjectMapper.Map<ProductInfoDto, ProductInfoCreateUpdateDto>(item);
                    await Repository.UpdateAsync(item, true);
                }
            }
        }
    }
}
