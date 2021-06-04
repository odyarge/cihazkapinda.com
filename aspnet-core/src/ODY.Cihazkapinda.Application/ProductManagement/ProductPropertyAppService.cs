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
    public class ProductPropertyAppService : CrudAppService<ProductProperty, ProductPropertyDto, Guid, PagedAndSortedResultRequestDto, ProductPropertyCreateUpdateDto, ProductPropertyCreateUpdateDto>, IProductPropertyAppService
    {
        public ProductPropertyAppService(IRepository<ProductProperty, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ProductProperties.ProductPropertiesDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ProductProperties.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ProductProperties.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ProductProperties.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ProductProperties.Delete;
        }

        [Authorize(Permissions.CihazkapindaPermissions.ProductProperties.List)]
        public async Task<List<ProductPropertyDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<ProductProperty>, List<ProductPropertyDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }
    }
}