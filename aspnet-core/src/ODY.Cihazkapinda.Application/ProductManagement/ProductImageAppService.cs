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
    public class ProductImageAppService : CrudAppService<ProductImage, ProductImageDto, Guid, PagedAndSortedResultRequestDto, ProductImageCreateUpdateDto, ProductImageCreateUpdateDto>, IProductImageAppService
    {
        public ProductImageAppService(IRepository<ProductImage, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ProductImages.ProductImagesDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ProductImages.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ProductImages.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ProductImages.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ProductImages.Delete;
        }
    }
}