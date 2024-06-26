using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.core.Entities;
using Ecom.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Ecom.API.Errors;
using Ecom.core.Sharing;
using Ecom.API.Helper;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uOW;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork UOW, IMapper mapper)
        {
            _uOW = UOW;
            _mapper = mapper;
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAll([FromQuery]ProductParams productParams)
        {
            
            var products = await _uOW.ProductRepository.GetAllAsync(productParams);
            var newProduct = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countItmes = newProduct.Count();
            return Ok(new Pagination<ProductDto>(productParams.PageNumber, productParams.PageSize, countItmes, newProduct));
        }

        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _uOW.ProductRepository.GetByIdAsync(id, x=> x.Category);
            if (product is null)
                return NotFound(new BaseCommonResponse(404));
            var newProduct = _mapper.Map<ProductDto>(product);
            return Ok(newProduct);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromForm]CreateProductDto productDto)
        {
            await Console.Out.WriteLineAsync("hello");
            try
            {
                if(ModelState.IsValid)
                {
                 var res = await _uOW.ProductRepository.AddAsync(productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("update-exiting-rpoduct")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uOW.ProductRepository.UpdateAsync(id, productDto);
                    return res ? Ok(productDto) : BadRequest();
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-exiting-product/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var res = await _uOW.ProductRepository.DeleteAsyncWithPicture(id);
                return res ? Ok("Deleted") : BadRequest("Not Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
