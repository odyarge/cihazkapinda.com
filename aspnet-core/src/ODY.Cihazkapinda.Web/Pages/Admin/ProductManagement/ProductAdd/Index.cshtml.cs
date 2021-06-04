using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ProductManagement.ProductAdd
{
    public class IndexModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public ProductAllCreateModal productAllCreateModal { get; set; }


        [BindProperty]
        public ProductAllEditModal productAllEditModal { get; set; }

        [BindProperty]
        public string cat_list_text { get; set; }
        public List<CategoryDto> list { get; set; }
        StringBuilder cat_list = new StringBuilder();


        [BindProperty]
        public List<string> PropKey { get; set; }

        [BindProperty]
        public List<string> PropValue { get; set; }

        [BindProperty]
        public List<string> PropTitle { get; set; }

        [BindProperty]
        public List<SelectListItem> SelectPropTitle { get; set; }

        #endregion PROPS
        #region SERVICES
        public IProductAppService _productAppService { get; set; }
        public IProductImageAppService _productImageAppService { get; set; }
        public IProductPropertyAppService _productPropertyAppService { get; set; }
        public IProductPropertyTitleAppService _productPropertyTitleAppService { get; set; }
        public ICategoryAppService _categoryAppService { get; set; }
        #endregion SERVICES
        public IndexModel(ISiteSettingAppService _siteSettingAppService,
            IProductAppService productAppService,
            IProductImageAppService productImageAppService,
            IProductPropertyAppService productPropertyAppService,
            IProductPropertyTitleAppService productPropertyTitleAppService,
            ICategoryAppService categoryAppService) : base(_siteSettingAppService)
        {
            _productAppService = productAppService;
            _productImageAppService = productImageAppService;
            _productPropertyAppService = productPropertyAppService;
            _productPropertyTitleAppService = productPropertyTitleAppService;
            _categoryAppService = categoryAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid? id)
        {
            await CheckAll();
            if (id == null)
            {
                ViewData["ProductCreateAndUpdateTitle"] = "Yeni �r�n";
                productAllCreateModal = new ProductAllCreateModal();
                await GetPropTitles();
            }
            else
            {
                ViewData["ProductCreateAndUpdateTitle"] = "�r�n D�zenle";
                productAllEditModal.productEditModal = ObjectMapper.Map<ProductDto, ProductEditModal>(
                        await _productAppService.GetAsync((Guid)id)
                    );
                await GetPropTitles();
            }

            await GetCategories();
            return await Task.FromResult<IActionResult>(Page());
        }

        public async Task GetCategories()
        {
            list = await _categoryAppService.GetAllList();

            List<CategoryDto> mainCategories = list.Where(w => w.SubCategory == null).ToList();

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
            List<CategoryDto> subCategories = list.Where(w => w.SubCategory == topCatID).ToList();
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

        public async Task GetPropTitles(string selected = null)
        {
            var list = await _productPropertyTitleAppService.GetAllList();
            SelectPropTitle = new List<SelectListItem>();
            SelectListItem empty = new SelectListItem
            {
                Text = "Se�iniz.",
                Value = "choise"
            };
            SelectPropTitle.Add(empty);

            foreach (var item in list)
            {
                SelectListItem newItem;
                if (selected == null)
                {
                    newItem = new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Name
                    };
                }
                else
                {
                    if (selected == item.Name)
                    {
                        newItem = new SelectListItem
                        {
                            Text = item.Name,
                            Value = item.Name,
                            Selected = true
                        };
                    }
                    else
                    {
                        newItem = new SelectListItem
                        {
                            Text = item.Name,
                            Value = item.Name
                        };
                    }
                }

                SelectPropTitle.Add(newItem);
            }
        }
    }

    public class ProductAllCreateModal
    {

        [BindProperty]
        public ProductCreateModal productCreateModal { get; set; }

        [BindProperty]
        public List<ProductImageCreateUpdateDto> productImageCreateUpdateDtos { get; set; }

        [BindProperty]
        public List<ProductPropertyCreateUpdateDto> productPropertyCreateUpdateDtos { get; set; }

        public ProductAllCreateModal()
        {
            productCreateModal = new ProductCreateModal();
            productImageCreateUpdateDtos = new List<ProductImageCreateUpdateDto>();
            productPropertyCreateUpdateDtos = new List<ProductPropertyCreateUpdateDto>();
        }
    }

    public class ProductAllEditModal
    {

        [BindProperty]
        public ProductEditModal productEditModal { get; set; }

        [BindProperty]
        public List<ProductImageCreateUpdateDto> productImageCreateUpdateDtos { get; set; }

        [BindProperty]
        public List<ProductPropertyCreateUpdateDto> productPropertyCreateUpdateDtos { get; set; }

        public ProductAllEditModal()
        {
            productEditModal = new ProductEditModal();
            productImageCreateUpdateDtos = new List<ProductImageCreateUpdateDto>();
            productPropertyCreateUpdateDtos = new List<ProductPropertyCreateUpdateDto>();
        }
    }
}
