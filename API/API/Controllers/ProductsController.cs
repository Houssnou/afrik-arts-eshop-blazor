using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;
using API.Helpers;
using System.Net;
using FluentResults;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {

            var totalItems = await _productService.CountProductsAsync(productParams);

            var products = await _productService.GetProductsAsync(productParams);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsyncById(id);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
        [Cached(600)]
        [HttpGet("origins")]
        public async Task<ActionResult<List<ProductOrigin>>> GetOrigins()
        {
            return Ok(_mapper.Map<IReadOnlyList<OriginForReturnDto>>(await _productService.GetProductOriginsAsync()));
        }
        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {
            return Ok(_mapper.Map<IReadOnlyList<TypeForReturnDto>>(await _productService.GetProductTypesAsync()));
        }

        [HttpPost("types")]
        [ProducesResponseType(typeof(BaseResponse<TypeForReturnDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<TypeForReturnDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<TypeForReturnDto>> AddNewType(TypeDto typeDto)
        {
            var productType = _mapper.Map<ProductType>(typeDto);

            var result = await _productService.AddProductTypeAsync(productType);

            if (result.IsFailed )
            {
                return BadRequest(new BaseResponse<TypeForReturnDto>
                {
                    Success = result.IsSuccess,
                    Errors = new List<string>
                    {
                        result.Errors[0].Message

                    }
                });
            }

            return Ok(new BaseResponse<TypeForReturnDto>
            {
                Success = result.IsSuccess,
                Data = _mapper.Map<TypeForReturnDto>(result.Value)

            });
        }
        [HttpPost("origins")]
        public async Task<ActionResult<OriginForReturnDto>> AddNewOrigin(OriginDto originDto)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<ProductToReturnDto>> AddNewProduct()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            return Ok();
        }
    }
}