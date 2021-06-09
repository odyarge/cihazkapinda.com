using AutoMapper;
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.Licenses;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.ProductManagement;
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

            CreateMap<OperatorSetting, OperatorSettingDto>();
            CreateMap<OperatorSettingDto, OperatorSettingCreateUpdateDto>();
            CreateMap<OperatorSettingCreateUpdateDto, OperatorSetting>();

            CreateMap<License, LicenseDto>();
            CreateMap<LicenseDto, LicenseCreateUpdateDto>();
            CreateMap<LicenseCreateUpdateDto, License>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, CategoryCreateUpdateDto>();
            CreateMap<CategoryCreateUpdateDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, ProductCreateUpdateDto>();
            CreateMap<ProductCreateUpdateDto, Product>();

            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductImageDto, ProductImageCreateUpdateDto>();
            CreateMap<ProductImageCreateUpdateDto, ProductImage>();

            CreateMap<ProductProperty, ProductPropertyDto>();
            CreateMap<ProductPropertyDto, ProductPropertyCreateUpdateDto>();
            CreateMap<ProductPropertyCreateUpdateDto, ProductProperty>();

            CreateMap<ProductPropertyTitle, ProductPropertyTitleDto>();
            CreateMap<ProductPropertyTitleDto, ProductPropertyTitleCreateUpdateDto>();
            CreateMap<ProductPropertyTitleCreateUpdateDto, ProductPropertyTitle>();

            CreateMap<ProductPropertyTemplate, ProductPropertyTemplateDto>();
            CreateMap<ProductPropertyTemplateDto, ProductPropertyTemplateCreateUpdateDto>();
            CreateMap<ProductPropertyTemplateCreateUpdateDto, ProductPropertyTemplate>();

            CreateMap<ProductPropertySubTemplate, ProductPropertySubTemplateDto>();
            CreateMap<ProductPropertySubTemplateDto, ProductPropertySubTemplateCreateUpdateDto>();
            CreateMap<ProductPropertySubTemplateCreateUpdateDto, ProductPropertySubTemplate>();

            CreateMap<ProductInfo, ProductInfoDto>();
            CreateMap<ProductInfoDto, ProductInfo>();
            CreateMap<ProductInfoDto, ProductInfoCreateUpdateDto>();
            CreateMap<ProductInfoCreateUpdateDto, ProductInfo>();

            CreateMap<ProductInfoTemplate, ProductInfoTemplateDto>();
            CreateMap<ProductInfoTemplateDto, ProductInfoTemplateCreateUpdateDto>();
            CreateMap<ProductInfoTemplateCreateUpdateDto, ProductInfoTemplate>();
        }
    }
}
