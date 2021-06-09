using Microsoft.AspNetCore.Authorization;
using ODY.Cihazkapinda.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.Categories
{
    public class CategoryAppService : CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto, CategoryCreateUpdateDto, CategoryCreateUpdateDto>, ICategoryAppService
    {
        private readonly IProductAppService _productAppService;
        public CategoryAppService(IRepository<Category, Guid> repository, IProductAppService productAppService) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Categories.CategoriesDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Categories.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Categories.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Categories.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Categories.Delete;

            _productAppService = productAppService;
        }

        public async Task<List<CategoryDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }

        public async Task<CategoryDto> GetAllListWithDetail(Guid id)
        {
            var entity = await Repository.GetAsync(id);
            ProductSearchDto input = new ProductSearchDto { 
                MaxResultCount = 4,
                CategoryId = id
            };
            var productList = await _productAppService.GetAsyncProductWithImageList(input);
            var map = ObjectMapper.Map<Category, CategoryDto>(entity);
            map.Products = productList.Items.ToList();
            return map;
        }
    }
}
