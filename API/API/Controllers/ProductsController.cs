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

        //[Cached(600)]
        [HttpGet("all")]
        public async Task<ActionResult<BaseResponse<List<ProductForListDto>>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(new BaseResponse<List<ProductForListDto>> { Success = true, Data = _mapper.Map<List<ProductForListDto>>(products) });
        }

        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<ProductToReturnDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<BaseResponse<ProductToReturnDto>>> GetProduct(int id)
        {
            var result = await _productService.GetProductAsyncById(id);

            if (!result.IsSuccess) return NotFound(new BaseResponse<ProductToReturnDto>
            {
                Success = result.IsSuccess,
                Errors = new List<string>
               {
                   result.Errors[0].Message
               }
            });

            return Ok(new BaseResponse<ProductToReturnDto>() { Success = result.IsSuccess, Data = _mapper.Map<ProductToReturnDto>(result.Value) });
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
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<ProductToReturnDto>> AddNewProduct(ProductDto productDto)
        {
            var result = await _productService.AddProductAsync(productDto.Name, productDto.Description,
                productDto.Price, productDto.PictureUrl, productDto.Type, productDto.Origin);

            if (!result.IsSuccess)
            {
                return BadRequest(new BaseResponse<string>
                {
                    Success = result.IsSuccess,
                    Errors = new List<string>
                    {
                        result.Errors[0].Message
                    }
                });
            }

            return Ok(new BaseResponse<ProductToReturnDto>
            {
                Success = result.IsSuccess,
                Data = _mapper.Map<ProductToReturnDto>(result.Value)
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse<ProductToReturnDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest(new BaseResponse<string>
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        $"Invalid product id: '{productDto.Id}.'"
                    }
                });
            }

            var result = await _productService.UpdateProductAsync(productDto.Id, productDto.Name, productDto.Description,
                productDto.Price, productDto.PictureUrl, productDto.Type, productDto.Origin);

            if (!result.IsSuccess)
            {
                return BadRequest(new BaseResponse<string>
                {
                    Success = result.IsSuccess,
                    Errors = new List<string>
                    {
                        result.Errors[0].Message
                    }
                });
            }

            return Ok(new BaseResponse<ProductToReturnDto>
            {
                Success = result.IsSuccess,
                Data = _mapper.Map<ProductToReturnDto>(result.Value)
            });
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