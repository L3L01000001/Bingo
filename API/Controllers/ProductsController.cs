using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<SubCategory> _subCategoriesRepo;
        private readonly IGenericRepository<Brand> _brandsRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productsRepo,
             IGenericRepository<Brand> brandsRepo,
             IGenericRepository<SubCategory> subCategoriesRepo,
             IMapper mapper)
        {
            _brandsRepo = brandsRepo;
            _subCategoriesRepo = subCategoriesRepo;
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts(
                [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithSubCategoriesAndBrandsSpecification(productParams);
            var products = await _productsRepo.ListAsync(spec);

            var countSpec = new ProductsWithFiltersForCount(productParams);
            var totalItems = await _productsRepo.CountAsync(countSpec);

            // return Ok(products.Select(product => new ProductDto
            // {
            //      Id = product.Id,
            //     Name = product.Name,
            //     Price = product.Price,
            //     Unit = product.Unit,
            //     ImageUrl = product.ImageUrl,
            //     Brand = product?.Brand?.Name,
            //     SubCategory = product?.SubCategory?.Name
            // }).ToList());


            var productsData = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>
            {
                Data = productsData,
                PageIndex = productParams.PageIndex,
                PageSize = productParams.PageSize,
                Count = totalItems
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var spec = new ProductsWithSubCategoriesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<ProductDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetProductBrands()
        {
            return Ok(await _brandsRepo.ListAllAsync());
        }
        [HttpGet("subcategories")]
        public async Task<ActionResult<IReadOnlyList<SubCategoryDto>>> GetProductSubCategories()
        {
            var spec = new SubCategoriesWithCategoriesSpecification();
            var subCategories = await _subCategoriesRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<SubCategoryDto>>(subCategories));
        }
    }
}