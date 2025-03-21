﻿using Application.DTOs;

namespace Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
