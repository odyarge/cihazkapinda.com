using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.ProductColorTypes;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.ProductTypes;
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
        public StrToDecimal strToDecimal { get; set; }

        [BindProperty]
        public string cat_list_text { get; set; }
        public List<CategoryDto> list { get; set; }
        StringBuilder cat_list = new StringBuilder();

        [Required]
        [BindProperty]
        public List<Guid?> PropId { get; set; }

        [Required]
        [BindProperty]
        public List<string> PropKey { get; set; }

        [Required]
        [BindProperty]
        public List<string> PropValue { get; set; }

        [Required]
        [BindProperty]
        public List<string> PropTitle { get; set; }

        [BindProperty]
        public List<SelectListItem> SelectPropTitle { get; set; }

        [BindProperty]
        public List<SelectListItem> ProductTypeSelect { get; set; }

        [BindProperty]
        public List<SelectListItem> ProductColorSelect { get; set; }

        [BindProperty]
        public List<ProductInfoTemplateDto> productInfoTemplates { get; set; }

        [BindProperty]
        public List<Guid> activeInfo { get; set; }

        public Guid? Id { get; set; }
        #endregion PROPS
        #region SERVICES
        public IProductAppService _productAppService { get; set; }
        public IProductImageAppService _productImageAppService { get; set; }
        public IProductPropertyAppService _productPropertyAppService { get; set; }
        public IProductPropertyTitleAppService _productPropertyTitleAppService { get; set; }
        public IProductInfoTemplateAppService _productInfoTemplateAppService { get; set; }
        public IProductInfoAppService _productInfoAppService { get; set; }
        public ICategoryAppService _categoryAppService { get; set; }
        #endregion SERVICES
        public IndexModel(ISiteSettingAppService _siteSettingAppService,
            IProductAppService productAppService,
            IProductImageAppService productImageAppService,
            IProductPropertyAppService productPropertyAppService,
            IProductPropertyTitleAppService productPropertyTitleAppService,
            IProductInfoTemplateAppService productInfoTemplateAppService,
            IProductInfoAppService productInfoAppService,
            ICategoryAppService categoryAppService) : base(_siteSettingAppService)
        {
            _productAppService = productAppService;
            _productImageAppService = productImageAppService;
            _productPropertyAppService = productPropertyAppService;
            _productPropertyTitleAppService = productPropertyTitleAppService;
            _productInfoTemplateAppService = productInfoTemplateAppService;
            _productInfoAppService = productInfoAppService;
            _categoryAppService = categoryAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid? id)
        {
            await CheckAll();

            if (id == null)
            {
                ViewData["ProductCreateAndUpdateTitle"] = "Yeni Ürün";
                productAllCreateModal = new ProductAllCreateModal();
                strToDecimal = new StrToDecimal();
                Fill(0, 0);
                await GetPropTitles();
                await GetCategories(null);
            }
            else
            {
                ViewData["ProductCreateAndUpdateTitle"] = "Ürün Düzenle";
                productAllEditModal = new ProductAllEditModal();
                productAllEditModal.productEditModal = ObjectMapper.Map<ProductDto, ProductEditModal>(
                        await _productAppService.GetAsyncProductWithDetail((Guid)id)
                    );
                strToDecimal = new StrToDecimal();
                strToDecimal.Price = productAllEditModal.productEditModal.Price.ToString();
                strToDecimal.DiscountPrice = productAllEditModal.productEditModal.DiscountPrice.ToString();

                ProductColorType _color = productAllEditModal.productEditModal.ProductColor;
                ProductType _type = productAllEditModal.productEditModal.ProductType;
                int colorIndex = Array.IndexOf(Enum.GetValues(_color.GetType()), _color);
                int typeIndex = Array.IndexOf(Enum.GetValues(_type.GetType()), _type);

                Fill(typeIndex, colorIndex);
                await GetPropTitles();
                await GetCategories(productAllEditModal.productEditModal.CategoryId);
            }
            Id = id;
            productInfoTemplates = await _productInfoTemplateAppService.GetAllList();
            return await Task.FromResult<IActionResult>(Page());
        }

        public async virtual Task<IActionResult> OnPostAsync(List<IFormFile> files)
        {
            var a = ModelState.IsValid;
            ValidateModel();
            if (productAllEditModal.productEditModal == null)
            {
                List<string> ignoreWWW = new List<string>();
                string tenantPath = string.Empty;
                string oldFilePath = string.Empty;
                string filePath = string.Empty;

                if (files != null)
                {
                    foreach (var item in files)
                    {
                        if (item.Length > 0)
                        {
                            if (CurrentTenant.Id == null)
                            {
                                tenantPath = "wwwroot\\host\\products";
                                ignoreWWW.Add(Path.Combine("\\host\\products", item.FileName));
                                filePath = Path.Combine(tenantPath, item.FileName);
                            }
                            else
                            {
                                tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\products";
                                ignoreWWW.Add(Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\products", item.FileName));
                                filePath = Path.Combine(tenantPath, item.FileName);
                            }

                            if (!Directory.Exists(tenantPath))
                            {
                                Directory.CreateDirectory(tenantPath);
                            }
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                            }
                        }
                    }
                }

                productAllCreateModal.productCreateModal.TenantId = CurrentTenant.Id;
                productAllCreateModal.productCreateModal.Price = decimal.Parse(strToDecimal.Price);
                productAllCreateModal.productCreateModal.DiscountPrice = decimal.Parse(strToDecimal.DiscountPrice == null ? "0" : strToDecimal.DiscountPrice);
                var createProduct = ObjectMapper.Map<ProductCreateModal, ProductCreateUpdateDto>(productAllCreateModal.productCreateModal);
                var productResult = await _productAppService.CreateAsync(createProduct);

                if (productResult != null)
                {
                    for (int i = 0; i < PropKey.Count; i++)
                    {
                        ProductPropertyCreateUpdateDto prop = new ProductPropertyCreateUpdateDto
                        {
                            KEY = PropKey[i],
                            VALUE = PropValue[i],
                            TITLE = PropTitle[i],
                            ProductId = productResult.Id,
                            TenantId = CurrentTenant.Id
                        };

                        await _productPropertyAppService.CreateAsync(prop);
                    }

                    for (int i = 0; i < ignoreWWW.Count; i++)
                    {
                        ProductImageCreateUpdateDto image = new ProductImageCreateUpdateDto
                        {
                            Image = ignoreWWW[i],
                            ProductId = productResult.Id,
                            TenantId = CurrentTenant.Id
                        };

                        await _productImageAppService.CreateAsync(image);
                    }

                    for (int i = 0; i < activeInfo.Count; i++)
                    {
                        ProductInfoCreateUpdateDto info = new ProductInfoCreateUpdateDto
                        {
                            Active = true,
                            ProductId = productResult.Id,
                            ProductInfoTemplateId = activeInfo[i],
                            TenantId = CurrentTenant.Id
                        };

                        await _productInfoAppService.CreateAsync(info);
                    }
                }

                Alerts.Success(
                   text: "Güncelleme iþleminiz baþarýyla gerçekleþtirilmiþtir.",
                   title: "Baþarýlý"
               );
                return await Task.FromResult<IActionResult>(Page());
            }
            else
            {
                List<string> ignoreWWW = new List<string>();
                string tenantPath = string.Empty;
                string oldFilePath = string.Empty;
                string filePath = string.Empty;

                if (files != null)
                {
                    foreach (var item in files)
                    {
                        if (item.Length > 0)
                        {
                            if (CurrentTenant.Id == null)
                            {
                                tenantPath = "wwwroot\\host\\products";
                                ignoreWWW.Add(Path.Combine("\\host\\products", item.FileName));
                                filePath = Path.Combine(tenantPath, item.FileName);
                            }
                            else
                            {
                                tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\products";
                                ignoreWWW.Add(Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\products", item.FileName));
                                filePath = Path.Combine(tenantPath, item.FileName);
                            }

                            if (!Directory.Exists(tenantPath))
                            {
                                Directory.CreateDirectory(tenantPath);
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                            }
                        }
                    }
                }

                productAllEditModal.productEditModal.TenantId = CurrentTenant.Id;
                productAllEditModal.productEditModal.Price = decimal.Parse(strToDecimal.Price);
                productAllEditModal.productEditModal.DiscountPrice = decimal.Parse(strToDecimal.DiscountPrice == null ? "0" : strToDecimal.DiscountPrice);
                var updateProduct = ObjectMapper.Map<ProductEditModal, ProductCreateUpdateDto>(productAllEditModal.productEditModal);
                var productResult = await _productAppService.UpdateAsync(productAllEditModal.productEditModal.Id, updateProduct);

                if (productResult != null)
                {
                    for (int i = 0; i < PropKey.Count - 1; i++)
                    {
                        if (PropId[i] != null)
                        {
                            var getProp = await _productPropertyAppService.GetAsync((Guid)PropId[i]);
                            getProp.KEY = PropKey[i];
                            getProp.VALUE = PropValue[i];
                            getProp.TITLE = PropTitle[i];

                            var updateProp = ObjectMapper.Map<ProductPropertyDto, ProductPropertyCreateUpdateDto>(getProp);

                            await _productPropertyAppService.UpdateAsync(getProp.Id, updateProp);
                        }
                        else
                        {
                            ProductPropertyCreateUpdateDto prop = new ProductPropertyCreateUpdateDto
                            {
                                KEY = PropKey[i],
                                VALUE = PropValue[i],
                                TITLE = PropTitle[i],
                                ProductId = productResult.Id,
                                TenantId = CurrentTenant.Id
                            };

                            await _productPropertyAppService.CreateAsync(prop);
                        }

                    }

                    if (files.Count > 0 && files != null)
                    {
                        for (int i = 0; i < ignoreWWW.Count; i++)
                        {
                            ProductImageCreateUpdateDto image = new ProductImageCreateUpdateDto
                            {
                                Image = ignoreWWW[i],
                                ProductId = productResult.Id,
                                TenantId = CurrentTenant.Id
                            };

                            await _productImageAppService.CreateAsync(image);
                        }
                    }

                    await _productInfoAppService.AllProductInfoPassive(productResult.Id);
                    for (int i = 0; i < activeInfo.Count; i++)
                    {
                        var control = await _productInfoAppService.GetAsyncProductInfoControl(activeInfo[i]);
                        if (control == false)
                        {
                            ProductInfoCreateUpdateDto info = new ProductInfoCreateUpdateDto
                            {
                                Active = true,
                                ProductId = productResult.Id,
                                ProductInfoTemplateId = activeInfo[i],
                                TenantId = CurrentTenant.Id
                            };

                            await _productInfoAppService.CreateAsync(info);
                        }
                        else
                        {
                            var info = await _productInfoAppService.GetAsyncProductInfoByTemplateId(activeInfo[i]);
                            info.Active = true;

                            var update = ObjectMapper.Map<ProductInfoDto, ProductInfoCreateUpdateDto>(info);
                            await _productInfoAppService.UpdateAsync(info.Id, update);
                        }
                    }
                }

                Alerts.Success(
                   text: "Güncelleme iþleminiz baþarýyla gerçekleþtirilmiþtir.",
                   title: "Baþarýlý"
               );
                return RedirectSafely("/Admin/ProductManagement/Products/");
            }

        }

        public async Task GetCategories(Guid? id)
        {
            list = await _categoryAppService.GetAllList();

            List<CategoryDto> mainCategories = list.Where(w => w.SubCategory == null).ToList();

            cat_list.AppendLine("<li style='margin-bottom:5px;'><span class='num'>A</span><a style='cursor:pointer;' class='main-category'>Ana Kategori</a>");
            cat_list.AppendLine("<input type='hidden' value='main' id='mainCategory'/>");
            cat_list.AppendLine("</li>");

            foreach (var item in mainCategories)
            {
                if (id != null)
                {
                    if (id == item.Id)
                    {
                        cat_list.AppendLine("<li style='margin-bottom:5px;'><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='main-category selected'>" + item.Name + "</a>");
                    }
                    else
                    {
                        cat_list.AppendLine("<li style='margin-bottom:5px;'><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='main-category'>" + item.Name + "</a>");
                    }
                }
                else
                {
                    cat_list.AppendLine("<li style='margin-bottom:5px;'><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='main-category'>" + item.Name + "</a>");
                }

                cat_list.AppendLine("<input type='hidden' value=" + item.Id + " id=" + item.Id + "/>");
                await GetCategoriesList(item.Id, id);
                cat_list.AppendLine("</li>");
            }

            cat_list_text = cat_list.ToString();
        }
        public async Task GetCategoriesList(Guid? topCatID, Guid? id)
        {
            List<CategoryDto> subCategories = list.Where(w => w.SubCategory == topCatID).ToList();
            if (subCategories.Count == 0)
                return;

            cat_list.AppendLine("<ol class='sub-category' style='display: none;'>");
            foreach (CategoryDto item in subCategories)
            {
                if (id != null)
                {
                    if (id == item.Id)
                    {
                        cat_list.AppendLine("<li><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='sub-main-category selected'>" + item.Name + "</a>");
                    }
                    else
                    {
                        cat_list.AppendLine("<li><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='sub-main-category'>" + item.Name + "</a>");
                    }
                }
                else
                {
                    cat_list.AppendLine("<li><span class='num'>" + item.Name.Substring(0, 1) + "</span><a style='cursor:pointer;' class='sub-main-category'>" + item.Name + "</a>");
                }

                cat_list.AppendLine("<input type='hidden' value=" + item.Id + " id=" + item.Id + "/>");
                await GetCategoriesList(item.Id, id);
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
                Text = "Seçiniz.",
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

        public void Fill(int type, int color)
        {
            ProductTypeSelect = new List<SelectListItem>() {
                new SelectListItem { Text = L["Enum:ProductType:0"], Value = "0"},
                new SelectListItem { Text = L["Enum:ProductType:1"], Value = "1"},
                new SelectListItem { Text = L["Enum:ProductType:2"], Value = "2"},
                new SelectListItem { Text = L["Enum:ProductType:3"], Value = "3"},
                new SelectListItem { Text = L["Enum:ProductType:4"], Value = "4"},
                new SelectListItem { Text = L["Enum:ProductType:5"], Value = "5"}
            };

            ProductColorSelect = new List<SelectListItem>() {
                new SelectListItem { Text = L["Enum:ProductColorType:0"], Value = "0"},
                new SelectListItem { Text = L["Enum:ProductColorType:1"], Value = "1"},
                new SelectListItem { Text = L["Enum:ProductColorType:2"], Value = "2"},
                new SelectListItem { Text = L["Enum:ProductColorType:3"], Value = "3"},
                new SelectListItem { Text = L["Enum:ProductColorType:4"], Value = "4"},
                new SelectListItem { Text = L["Enum:ProductColorType:5"], Value = "5"},
                new SelectListItem { Text = L["Enum:ProductColorType:6"], Value = "6"},
                new SelectListItem { Text = L["Enum:ProductColorType:7"], Value = "7"},
                new SelectListItem { Text = L["Enum:ProductColorType:8"], Value = "8"},
                new SelectListItem { Text = L["Enum:ProductColorType:9"], Value = "9"},
                new SelectListItem { Text = L["Enum:ProductColorType:10"], Value = "10"},
                new SelectListItem { Text = L["Enum:ProductColorType:11"], Value = "11"},
                new SelectListItem { Text = L["Enum:ProductColorType:12"], Value = "12"},
            };

            ProductTypeSelect[type].Selected = true;
            ProductColorSelect[color].Selected = true;
        }

        public async Task AllProductInfoUpdated(Guid id)
        {
            var list = await _productInfoAppService.GetAllList();
            var filterList = list.FindAll(x => x.ProductId == id);
            foreach (var item in filterList)
            {
                item.Active = false;
                var update = ObjectMapper.Map<ProductInfoDto, ProductInfoCreateUpdateDto>(item);

                await _productInfoAppService.UpdateAsync(item.Id, update);
            }
        }
    }

    public class ProductAllCreateModal
    {

        [BindProperty]
        public ProductCreateModal productCreateModal { get; set; }

        public ProductAllCreateModal()
        {
            productCreateModal = new ProductCreateModal();
        }
    }

    public class ProductAllEditModal
    {

        [BindProperty]
        public ProductEditModal productEditModal { get; set; }
    }

    public class StrToDecimal
    {
        [BindProperty]
        [Required]
        [Display(Name = "Price")]
        public string Price { get; set; }
        [BindProperty]
        [Display(Name = "DiscountPrice")]
        public string DiscountPrice { get; set; }
    }
}
