using Microsoft.AspNetCore.Authorization;
using ODY.Cihazkapinda.BannerImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.BannerSettings
{
    public class BannerSettingAppService : CrudAppService<BannerSetting, BannerSettingDto, Guid, PagedAndSortedResultRequestDto, BannerSettingCreateUpdateDto, BannerSettingCreateUpdateDto>, IBannerSettingAppService
    {
        IRepository<BannerImage, Guid> _bannerImageRepository;
        public BannerSettingAppService(IRepository<BannerSetting, Guid> repository, IRepository<BannerImage, Guid> bannerImageRepository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.BannerSettings.BannerSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.BannerSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.BannerSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.BannerSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.BannerSettings.Delete;

            _bannerImageRepository = bannerImageRepository;
        }

        public async override Task DeleteAsync(Guid id)
        {
            var images = await _bannerImageRepository.GetListAsync();
            var selectedList = images.FindAll(x => x.BannerSettingId == id);
            foreach (var item in selectedList)
            {
                await _bannerImageRepository.DeleteAsync(item.Id);
            }
            await base.DeleteAsync(id);
        }
        [RemoteService(false)]
        public async Task<long> GetAsyncBannerCount(Guid? tenantId)
        {
            var item = await Repository.GetListAsync();
            return item.Count;
        }

        public async Task<BannerSettingDto> GetAsyncBannerWithImage(Guid Id)
        {
            var item = await Repository.GetAsync(Id);
            var images = await _bannerImageRepository.GetListAsync();
            item.Images = images.FindAll(x => x.BannerSettingId == Id);
            return ObjectMapper.Map<BannerSetting, BannerSettingDto>(item);
        }
    }
}
