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
    public class ProductAppService : CrudAppService<Product, ProductDto, Guid, PagedAndSortedResultRequestDto, ProductCreateUpdateDto, ProductCreateUpdateDto>, IProductAppService
    {
        public ProductAppService(IRepository<Product, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Products.ProductsDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Products.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Products.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Products.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Products.Delete;
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<List<ProductDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }
    }
}