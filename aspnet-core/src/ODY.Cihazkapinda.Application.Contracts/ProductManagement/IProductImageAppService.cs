﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.ProductManagement
{
    public interface IProductImageAppService : ICrudAppService<ProductImageDto, Guid, PagedAndSortedResultRequestDto, ProductImageCreateUpdateDto, ProductImageCreateUpdateDto>
    {
    }
}
