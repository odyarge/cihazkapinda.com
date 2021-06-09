using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfoTemplateAppService : CrudAppService<ProductInfoTemplate, ProductInfoTemplateDto, Guid, PagedAndSortedResultRequestDto, ProductInfoTemplateCreateUpdateDto, ProductInfoTemplateCreateUpdateDto>, IProductInfoTemplateAppService
    {
        public ProductInfoTemplateAppService(IRepository<ProductInfoTemplate, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Products.ProductsDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Products.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Products.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Products.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Products.Delete;
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<List<ProductInfoTemplateDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<ProductInfoTemplate>, List<ProductInfoTemplateDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }
    }
}
