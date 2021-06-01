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
            GetPolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.GeneralSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.Delete;
        }
    }
}
