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
    public class ProductPropertyTitleAppService : CrudAppService<ProductPropertyTitle, ProductPropertyTitleDto, Guid, PagedAndSortedResultRequestDto, ProductPropertyTitleCreateUpdateDto, ProductPropertyTitleCreateUpdateDto>, IProductPropertyTitleAppService
    {
        public ProductPropertyTitleAppService(IRepository<ProductPropertyTitle, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTitles.ProductPropertyTitlesDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTitles.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTitles.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTitles.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTitles.Delete;
        }
    }
}