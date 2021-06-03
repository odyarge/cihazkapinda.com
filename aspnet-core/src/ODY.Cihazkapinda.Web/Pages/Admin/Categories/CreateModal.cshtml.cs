using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.CategoryModals;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Web.Pages.Admin.Categories
{
    public class CreateModalModel : CihazkapindaPageModel
    {

        #region PROPS
        [BindProperty]
        public CategoryCreateModal categoryCreateModal { get; set; }

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        [HiddenInput]
        [Display(Name = "OwnerCategorySelect")]
        public string categorySelect { get; set; }

        public PagedResultDto<CategoryDto> list { get; set; }

        StringBuilder cat_list = new StringBuilder();

        [BindProperty]
        public string cat_list_text { get; set; }

        #endregion PROPS

        #region SERVICES
        public ISiteSettingAppService _siteSettingAppService { get; set; }
        public ICategoryAppService _categoryAppService { get; set; }
        #endregion SERVICES
        public CreateModalModel(ISiteSettingAppService siteSettingAppService,
            ICategoryAppService categoryAppService) : base(siteSettingAppService)
        {
            _siteSettingAppService = siteSettingAppService;
            _categoryAppService = categoryAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            categoryCreateModal = new CategoryCreateModal();

            await GetCategories();
            return await Task.FromResult<IActionResult>(Page());
        }
        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            if (categorySelect == "main")
            {
                categoryCreateModal.SubCategory = null;
            }
            else
            {
                categoryCreateModal.SubCategory = Guid.Parse(categorySelect);
            }

            categoryCreateModal.TenantId = CurrentTenant.Id;
            var create = ObjectMapper.Map<CategoryCreateModal, CategoryCreateUpdateDto>(categoryCreateModal);
            await _categoryAppService.CreateAsync(create);
            return NoContent();
        }
        public async Task GetCategories()
        {
            PagedAndSortedResultRequestDto dto = new PagedAndSortedResultRequestDto() { Sorting = "creationTime ASC" };
            list = await _categoryAppService.GetListAsync(dto);

            List<CategoryDto> mainCategories = list.Items.Where(w => w.SubCategory == null).ToList();

            cat_list.AppendLine("<li style='margin-bottom:5px;'><span class='num'>A</span><a style='cursor:pointer;' class='main-category'>Ana Kategori</a>");
            cat_list.AppendLine("<input type='hidden' value='main' id='mainCategory'/>");
            cat_list.AppendLine("</li>");

            foreach (var item in mainCategories)
            {
                cat_list.AppendLine("<li style='margin-bottom:5px;'><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='main-category'>" + item.Name + "</a>");
                cat_list.AppendLine("<input type='hidden' value=" + item.Id + " id=" + item.Id + "/>");
                await GetCategoriesList(item.Id);
                cat_list.AppendLine("</li>");
            }

            cat_list_text = cat_list.ToString();
        }
        public async Task GetCategoriesList(Guid? topCatID)
        {
            List<CategoryDto> subCategories = list.Items.Where(w => w.SubCategory == topCatID).ToList();
            if (subCategories.Count == 0)
                return;

            cat_list.AppendLine("<ol class='sub-category' style='display: none;'>");
            foreach (CategoryDto item in subCategories)
            {
                cat_list.AppendLine("<li><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='sub-main-category'>" + item.Name + "</a>");
                cat_list.AppendLine("<input type='hidden' value=" + item.Id + " id=" + item.Id + "/>");
                await GetCategoriesList(item.Id);
                cat_list.AppendLine("</li>");
            }
            cat_list.AppendLine("</ol>");
        }
    }
}
