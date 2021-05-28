using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.BannerImages
{
    public class BannerImageAppService : CrudAppService<BannerImage, BannerImageDto, Guid, PagedAndSortedResultRequestDto, BannerImageCreateUpdateDto, BannerImageCreateUpdateDto>, IBannerImageAppService
    {
        public BannerImageAppService(IRepository<BannerImage, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.BannerImages.BannerImageDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.BannerImages.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.BannerImages.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.BannerImages.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.BannerImages.Delete;
        }
    }
}
