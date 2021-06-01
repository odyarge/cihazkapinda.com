using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.Licenses
{
    public class LicenseAppService : CrudAppService<License, LicenseDto, Guid, PagedAndSortedResultRequestDto, LicenseCreateUpdateDto, LicenseCreateUpdateDto>, ILicenseAppService
    {
        private readonly IDataFilter _dataFilter;
        public LicenseAppService(IRepository<License, Guid> repository, IDataFilter dataFilter) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Licenses.LicenseDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Licenses.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Licenses.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Licenses.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Licenses.Delete;

            _dataFilter = dataFilter;
        }

        public async Task<LicenseDto> GetCheckLicense(string license, string owner)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var item = await Repository.FindAsync(x => x.LICENSE == license && x.LICENSE_OWNER == owner);
                return ObjectMapper.Map<License, LicenseDto>(item);
            }
        }

        [Authorize(Permissions.CihazkapindaPermissions.Licenses.List)]
        public async Task<LicenseDto> GetAsyncByName(string input)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var item = await Repository.FindAsync(x => x.LICENSE_OWNER == input);
                return ObjectMapper.Map<License, LicenseDto>(item);
            }
        }

        [RemoteService(false)]
        [Authorize(Permissions.CihazkapindaPermissions.Licenses.Delete)]
        public async Task DeleteLicense(Guid id)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                await Repository.DeleteAsync(id);
            }
        }
    }
}
