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
    public class ProductPropertySubTemplateAppService : CrudAppService<ProductPropertySubTemplate, ProductPropertySubTemplateDto, Guid, PagedAndSortedResultRequestDto, ProductPropertySubTemplateCreateUpdateDto, ProductPropertySubTemplateCreateUpdateDto>, IProductPropertySubTemplateAppService
    {
        public ProductPropertySubTemplateAppService(IRepository<ProductPropertySubTemplate, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ProductPropertySubTemplate.ProductPropertySubTemplateDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ProductPropertySubTemplate.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ProductPropertySubTemplate.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ProductPropertySubTemplate.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ProductPropertySubTemplate.Delete;
        }

        [Authorize(Permissions.CihazkapindaPermissions.ProductPropertySubTemplate.List)]
        public async Task<List<ProductPropertySubTemplateDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<ProductPropertySubTemplate>, List<ProductPropertySubTemplateDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }
    }
}
