using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async  Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var product = await productRepo.ListAsync(spec);

            return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await productRepo.GetEntityWithspec(spec);
            return mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await brandRepo.GetAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await typeRepo.GetAllAsync());
        }
    }
}
