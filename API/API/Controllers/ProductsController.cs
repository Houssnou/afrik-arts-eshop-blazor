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
        [ProducesResponseType(typeof(BaseResponse<OriginForReturnDto>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<ActionResult<BaseResponse<List<OriginForReturnDto>>>> GetOrigins()
        {
            var result = await _productService.GetProductOriginsAsync();

            return Ok(new BaseResponse<List<OriginForReturnDto>> { Success = true, Data = _mapper.Map<List<OriginForReturnDto>>(result) });
        }
        [Cached(600)]
        [HttpGet("types")]
        [ProducesResponseType(typeof(BaseResponse<TypeForReturnDto>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<ActionResult<BaseResponse<List<TypeForReturnDto>>>> GetTypes()
        {
            var result = await _productService.GetProductTypesAsync();

            return Ok(new BaseResponse<List<TypeForReturnDto>> { Success = true, Data = _mapper.Map<List<TypeForReturnDto>>(result) });
        }

        [HttpPost("types")]
        [ProducesResponseType(typeof(BaseResponse<TypeForReturnDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<TypeForReturnDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<TypeForReturnDto>> AddNewType(TypeDto typeDto)
        {
            var productType = _mapper.Map<ProductType>(typeDto);

            var result = await _productService.AddProductTypeAsync(productType);

            if (result.IsFailed)
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
        [ProducesResponseType(typeof(BaseResponse<OriginForReturnDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<OriginForReturnDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<OriginForReturnDto>> AddNewOrigin(OriginDto originDto)
        {
            var productOrigin = _mapper.Map<ProductOrigin>(originDto);

            var result = await _productService.AddProductOriginAsync(productOrigin);

            if (result.IsFailed)
            {
                return BadRequest(new BaseResponse<OriginForReturnDto>
                {
                    Success = result.IsSuccess,
                    Errors = new List<string>
                    {
                        result.Errors[0].Message
                    }
                });
            }

            return Ok(new BaseResponse<OriginForReturnDto>
            {
                Success = result.IsSuccess,
                Data = _mapper.Map<OriginForReturnDto>(result.Value)
            });
        }
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<ProductToReturnDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<ProductToReturnDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<ProductToReturnDto>> AddNewProduct(ProductDto productDto)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            return Ok();
        }
    }
}