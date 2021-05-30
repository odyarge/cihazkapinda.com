using AutoMapper;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.Licenses;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Web.Models.GeneralSettingModals;
using ODY.Cihazkapinda.Web.Models.LicenseModals;
using ODY.Cihazkapinda.Web.Models.OperatorSettingModals;
using ODY.Cihazkapinda.Web.Models.SiteSettingModals;
using ODY.Cihazkapinda.Web.Models.ThemeSettingModals;

namespace ODY.Cihazkapinda.Web
{
    public class CihazkapindaWebAutoMapperProfile : Profile
    {
        public CihazkapindaWebAutoMapperProfile()
        {
            CreateMap<SiteSettingDto, SiteSettingEditModal>();
            CreateMap<SiteSettingEditModal, SiteSettingCreateUpdateDto>();
            CreateMap<SiteSettingCreateModal, SiteSettingCreateUpdateDto>();

            CreateMap<OperatorSettingDto, OperatorSettingEditModal>();
            CreateMap<OperatorSettingEditModal, OperatorSettingCreateUpdateDto>();
            CreateMap<OperatorSettingCreateModal, OperatorSettingCreateUpdateDto>();

            CreateMap<GeneralSettingDto, GeneralSettingEditModal>();
            CreateMap<GeneralSettingEditModal, GeneralSettingCreateUpdateDto>();
            CreateMap<GeneralSettingCreateModal, GeneralSettingCreateUpdateDto>();

            CreateMap<LicenseDto, LicenseEditModal>();
            CreateMap<LicenseEditModal, LicenseCreateUpdateDto>();
            CreateMap<LicenseCreateModal, LicenseCreateUpdateDto>();

            CreateMap<ThemeSettingDto, ThemeSettingEditModal>();
            CreateMap<ThemeSettingEditModal, ThemeSettingCreateUpdateDto>();
            CreateMap<ThemeSettingCreateModal, ThemeSettingCreateUpdateDto>();
        }
    }
}
