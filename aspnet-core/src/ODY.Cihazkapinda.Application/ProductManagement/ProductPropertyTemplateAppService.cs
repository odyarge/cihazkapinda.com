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
    public class ProductPropertyTemplateAppService : CrudAppService<ProductPropertyTemplate, ProductPropertyTemplateDto, Guid, PagedAndSortedResultRequestDto, ProductPropertyTemplateCreateUpdateDto, ProductPropertyTemplateCreateUpdateDto>, IProductPropertyTemplateAppService
    {
        IRepository<ProductPropertySubTemplate, Guid> _productPropertySubTemplate;
        public ProductPropertyTemplateAppService(IRepository<ProductPropertyTemplate, Guid> repository, IRepository<ProductPropertySubTemplate, Guid> productPropertySubTemplate) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTemplate.ProductPropertyTemplateDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTemplate.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTemplate.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTemplate.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ProductPropertyTemplate.Delete;

            _productPropertySubTemplate = productPropertySubTemplate;
        }

        [Authorize(Permissions.CihazkapindaPermissions.ProductPropertyTemplate.List)]
        public async Task<List<ProductPropertyTemplateDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<ProductPropertyTemplate>, List<ProductPropertyTemplateDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }


        [Authorize(Permissions.CihazkapindaPermissions.ProductPropertyTemplate.List)]
        public async Task<ProductPropertyTemplateDto> GetAsyncProductTemplateWithSub(Guid Id)
        {
            var item = await Repository.GetAsync(Id);
            var subs = await _productPropertySubTemplate.GetListAsync();
            item.ProductPropertySubTemplates = subs.FindAll(x => x.ProductPropertyTemplateId == Id);
            return ObjectMapper.Map<ProductPropertyTemplate, ProductPropertyTemplateDto>(item);
        }
    }
}