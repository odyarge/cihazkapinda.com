using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.Licenses
{
    public interface ILicenseAppService : ICrudAppService<LicenseDto, Guid, PagedAndSortedResultRequestDto, LicenseCreateUpdateDto, LicenseCreateUpdateDto>
    {
        Task<LicenseDto> GetCheckLicense(string license, string owner);
        Task<LicenseDto> GetAsyncByName(string input);
        Task DeleteLicense(Guid id);
    }
}
