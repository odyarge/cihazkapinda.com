using Microsoft.AspNetCore.Authorization;
using ODY.Cihazkapinda.ProductColorTypes;
using ODY.Cihazkapinda.ProductTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductAppService : CrudAppService<Product, ProductDto, Guid, PagedAndSortedResultRequestDto, ProductCreateUpdateDto, ProductCreateUpdateDto>, IProductAppService
    {
        private readonly IRepository<ProductImage, Guid> _productImageAppService;
        private readonly IRepository<ProductProperty, Guid> _productPropertyAppService;
        private readonly IProductInfoAppService _productInfoAppService;
        public ProductAppService(IRepository<Product, Guid> repository,
            IRepository<ProductImage, Guid> productImageAppService,
            IRepository<ProductProperty, Guid> productPropertyAppService,
            IProductInfoAppService productInfoAppService) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Products.ProductsDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Products.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Products.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Products.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Products.Delete;

            _productImageAppService = productImageAppService;
            _productPropertyAppService = productPropertyAppService;
            _productInfoAppService = productInfoAppService;
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<List<ProductDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }

        [Authorize(Permissions.CihazkapindaPermissions.Products.List)]
        public async Task<ProductDto> GetAsyncProductWithDetail(Guid id)
        {
            var product = await Repository.GetAsync(id);
            var images = await _productImageAppService.GetListAsync();
            var properties = await _productPropertyAppService.GetListAsync();
            var info = await _productInfoAppService.GetAllListWithDetail();

            var infoList = ObjectMapper.Map<List<ProductInfoDto>, List<ProductInfo>>(info);

            product.Images = images.FindAll(x => x.ProductId == id);
            product.ProductProperty = properties.FindAll(x => x.ProductId == id);
            product.ProductInfo = infoList.FindAll(x => x.ProductId == id);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<PagedResultDto<ProductDto>> GetAsyncProductWithImageList(ProductSearchDto input)
        {

            var list = await Repository.GetListAsync();
            if (input != null)
            {
                if (input.Filter != null && input.Filter.Length > 2)
                    list = list.FindAll(x => x.Title.Contains(input.Filter));
                if (input.CategoryId.HasValue)
                    list = list.FindAll(x => x.CategoryId == input.CategoryId);
                if (input.PriceFrom.HasValue)
                    list = list.FindAll(x => x.Price >= input.PriceFrom);
                if (input.PriceTo.HasValue)
                    list = list.FindAll(x => x.Price <= input.PriceTo);
                if (input.Installment.HasValue)
                    list = list.FindAll(x => x.Installment == input.Installment);
                if (input.ProductType.HasValue)
                    list = list.FindAll(x => x.ProductType == input.ProductType);
                if (input.ProductColor.HasValue && input.ProductColor != ProductColorType.Yok)
                    list = list.FindAll(x => x.ProductColor == input.ProductColor);
            }

            list = list
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .OrderByDescending(x => x.CreationTime).ToList();

            var count = list.Count();

            foreach (var item in list)
            {
                var imageList = await _productImageAppService.GetListAsync();
                item.Images = imageList.FindAll(x => x.ProductId == item.Id);
            }

            return new PagedResultDto<ProductDto>(
                count,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(list)
            );
        }
    }
}