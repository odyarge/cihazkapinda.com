using AutoMapper;
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.Customers;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.Licenses;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Web.Models.BannerImageModals;
using ODY.Cihazkapinda.Web.Models.BannerSettingModals;
using ODY.Cihazkapinda.Web.Models.CategoryModals;
using ODY.Cihazkapinda.Web.Models.CustomerModals;
using ODY.Cihazkapinda.Web.Models.GeneralSettingModals;
using ODY.Cihazkapinda.Web.Models.LicenseModals;
using ODY.Cihazkapinda.Web.Models.OperatorSettingModals;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;
using ODY.Cihazkapinda.Web.Models.SiteSettingModals;
using ODY.Cihazkapinda.Web.Models.ThemeSettingModals;
using Volo.Abp.AutoMapper;
using Volo.Abp.PermissionManagement;
using static ODY.Cihazkapinda.Web.Pages.AbpPermissionManagement.PermissionManagementModal;

namespace ODY.Cihazkapinda.Web
{
    public class CihazkapindaWebAutoMapperProfile : Profile
    {
        public CihazkapindaWebAutoMapperProfile()
        {

            CreateMap<PermissionGroupDto, PermissionGroupViewModel>().Ignore(p => p.IsAllPermissionsGranted);
            CreateMap<PermissionGrantInfoDto, PermissionGrantInfoViewModel>()
                .ForMember(p => p.Depth, opts => opts.Ignore());
            CreateMap<ProviderInfoDto, ProviderInfoViewModel>();
            //
            CreateMap<SiteSettingDto, SiteSettingEditModal>();
            CreateMap<SiteSettingDto, SiteSettingCreateUpdateDto>();
            CreateMap<SiteSettingEditModal, SiteSettingCreateUpdateDto>();
            CreateMap<SiteSettingCreateModal, SiteSettingCreateUpdateDto>();
            //
            CreateMap<BannerSettingDto, BannerSettingEditModal>();
            CreateMap<BannerSettingDto, BannerSettingCreateUpdateDto>();
            CreateMap<BannerSettingEditModal, BannerSettingCreateUpdateDto>();
            CreateMap<BannerSettingCreateModal, BannerSettingCreateUpdateDto>();
            //
            CreateMap<BannerImageDto, BannerImageEditModal>();
            CreateMap<BannerImageDto, BannerImageCreateUpdateDto>();
            CreateMap<BannerImageEditModal, BannerImageCreateUpdateDto>();
            CreateMap<BannerImageCreateModal, BannerImageCreateUpdateDto>();
            //
            CreateMap<OperatorSettingDto, OperatorSettingEditModal>();
            CreateMap<OperatorSettingEditModal, OperatorSettingCreateUpdateDto>();
            CreateMap<OperatorSettingCreateModal, OperatorSettingCreateUpdateDto>();
            //
            CreateMap<GeneralSettingDto, GeneralSettingEditModal>();
            CreateMap<GeneralSettingEditModal, GeneralSettingCreateUpdateDto>();
            CreateMap<GeneralSettingCreateModal, GeneralSettingCreateUpdateDto>();
            //
            CreateMap<LicenseDto, LicenseEditModal>();
            CreateMap<LicenseEditModal, LicenseCreateUpdateDto>();
            CreateMap<LicenseCreateModal, LicenseCreateUpdateDto>();
            //
            CreateMap<ThemeSettingDto, ThemeSettingEditModal>();
            CreateMap<ThemeSettingEditModal, ThemeSettingCreateUpdateDto>();
            CreateMap<ThemeSettingCreateModal, ThemeSettingCreateUpdateDto>();
            //
            CreateMap<CategoryDto, CategoryEditModal>();
            CreateMap<CategoryEditModal, CategoryCreateUpdateDto>();
            CreateMap<CategoryCreateModal, CategoryCreateUpdateDto>();
            //
            CreateMap<ProductDto, ProductEditModal>();
            CreateMap<ProductEditModal, ProductCreateUpdateDto>();
            CreateMap<ProductCreateModal, ProductCreateUpdateDto>();
            //
            CreateMap<ProductImageDto, ProductImageEditModal>();
            CreateMap<ProductImageEditModal, ProductImageCreateUpdateDto>();
            CreateMap<ProductImageCreateModal, ProductImageCreateUpdateDto>();
            //
            CreateMap<ProductPropertyDto, ProductPropertyEditModal>();
            CreateMap<ProductPropertyEditModal, ProductPropertyCreateUpdateDto>();
            CreateMap<ProductPropertyCreateModal, ProductPropertyCreateUpdateDto>();
            CreateMap<ProductPropertyDto, ProductPropertyCreateUpdateDto>();
            //
            CreateMap<ProductPropertyTitleDto, ProductPropertyTitleEditModal>();
            CreateMap<ProductPropertyTitleEditModal, ProductPropertyTitleCreateUpdateDto>();
            CreateMap<ProductPropertyTitleCreateModal, ProductPropertyTitleCreateUpdateDto>();
            //
            CreateMap<ProductPropertyTemplateDto, ProductPropertyTemplateEditModal>();
            CreateMap<ProductPropertyTemplateEditModal, ProductPropertyTemplateCreateUpdateDto>();
            CreateMap<ProductPropertyTemplateCreateModal, ProductPropertyTemplateCreateUpdateDto>();
            //
            CreateMap<ProductPropertySubTemplateDto, ProductPropertySubTemplateEditModal>();
            CreateMap<ProductPropertySubTemplateEditModal, ProductPropertySubTemplateCreateUpdateDto>();
            CreateMap<ProductPropertySubTemplateCreateModal, ProductPropertySubTemplateCreateUpdateDto>();
            //
            CreateMap<ProductInfoDto, ProductInfoEditModal>();
            CreateMap<ProductInfoDto, ProductInfoCreateUpdateDto>();
            CreateMap<ProductInfoEditModal, ProductInfoCreateUpdateDto>();
            CreateMap<ProductInfoCreateModal, ProductInfoCreateUpdateDto>();
            //
            CreateMap<ProductInfoTemplateDto, ProductInfoTemplateEditModal>();
            CreateMap<ProductInfoTemplateDto, ProductInfoTemplateSelectModal>();
            CreateMap<ProductInfoTemplateEditModal, ProductInfoTemplateCreateUpdateDto>();
            CreateMap<ProductInfoTemplateCreateModal, ProductInfoTemplateCreateUpdateDto>();
            //
            CreateMap<CustomerDto, CustomerEditModal>();
            CreateMap<CustomerEditModal, CustomerCreateUpdateDto>();
            CreateMap<CustomerCreateModal, CustomerCreateUpdateDto>();
        }
    }
}
