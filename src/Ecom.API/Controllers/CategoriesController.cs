using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.core.Entities;
using Ecom.core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uOW;
        private readonly object _context;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork UOW, ApplicationDbContext context, IMapper mapper)
        {
            _uOW = UOW;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> Get()
        {
            var categories = await _uOW.CategoryRepository.GetAllAsync();
            if (categories is not null)
            {

                var res = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ListingCategoryDto>>(categories);
                return Ok(res);
            }
            return BadRequest("No categories found");

        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _uOW.CategoryRepository.GetAsync(id);

            if (category is null)
            {
                
                return BadRequest($"No category found with id : {id}");
            }

            var newCategory = _mapper.Map<Category, ListingCategoryDto>(category);
            return Ok(newCategory);

        }



        [HttpPost("add-new-category")]
        public async Task<IActionResult> Post(CategoryDto categoryDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   
                    var res = _mapper.Map<Category>(categoryDto);
                    await _uOW.CategoryRepository.AddAsync(res);
                    return Ok(categoryDto);
                }
                else
                {
                    return BadRequest(categoryDto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("update-category-by-id")]
        public async Task<IActionResult> Put(UpdateCategoryDto categoryDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var category = await _uOW.CategoryRepository.GetAsync(categoryDto.Id);
                    if (category is not null)
                    {
                        _mapper.Map(categoryDto, category);
                        await _uOW.CategoryRepository.UpdateAsync(categoryDto.Id, category);
                        return Ok(categoryDto);
                    }
                    return BadRequest($"No category found with id : {categoryDto.Id}");
                }
                else
                {
                    return BadRequest(categoryDto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _uOW.CategoryRepository.GetAsync(id);
                if (category is not null)
                {
                    await _uOW.CategoryRepository.DeleteAsync(id);
                    return Ok($"Category with id : {id} deleted successfully");
                }
                return BadRequest($"No category found with id : {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
