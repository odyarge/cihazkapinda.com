using AutoMapper;
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;

namespace ODY.Cihazkapinda
{
    public class CihazkapindaApplicationAutoMapperProfile : Profile
    {
        public CihazkapindaApplicationAutoMapperProfile()
        {
            CreateMap<SiteSetting, SiteSettingDto>();
            CreateMap<SiteSettingDto, SiteSettingCreateUpdateDto>();
            CreateMap<SiteSettingCreateUpdateDto, SiteSetting>();

            CreateMap<GeneralSetting, GeneralSettingDto>();
            CreateMap<GeneralSettingDto, GeneralSettingCreateUpdateDto>();
            CreateMap<GeneralSettingCreateUpdateDto, GeneralSetting>();

            CreateMap<ThemeSetting, ThemeSettingDto>();
            CreateMap<ThemeSettingDto, ThemeSettingCreateUpdateDto>();
            CreateMap<ThemeSettingCreateUpdateDto, ThemeSetting>();

            CreateMap<BannerSetting, BannerSettingDto>();
            CreateMap<BannerSettingDto, BannerSettingCreateUpdateDto>();
            CreateMap<BannerSettingCreateUpdateDto, BannerSetting>();

            CreateMap<BannerImage, BannerImageDto>();
            CreateMap<BannerImageDto, BannerImageCreateUpdateDto>();
            CreateMap<BannerImageCreateUpdateDto, BannerImage>();
        }
    }
}
