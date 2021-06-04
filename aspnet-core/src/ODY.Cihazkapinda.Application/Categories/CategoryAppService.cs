using Microsoft.AspNetCore.Authorization;
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
        public CategoryAppService(IRepository<Category, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Categories.CategoriesDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Categories.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Categories.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Categories.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Categories.Delete;
        }

        [Authorize(Permissions.CihazkapindaPermissions.Categories.List)]
        public async Task<List<CategoryDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }
    }
}
