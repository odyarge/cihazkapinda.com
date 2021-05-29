using AutoMapper;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.OperatorSettingModals;
using ODY.Cihazkapinda.Web.Models.SiteSettingModals;

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
        }
    }
}
