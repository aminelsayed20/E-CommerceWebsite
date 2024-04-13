using API.Controllers;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastucture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : BaseApiController
	{
		private readonly IProductRepository _productRepository;
		private readonly IGenericRepository<Product> _ProductRepo;
		private readonly IGenericRepository<ProductBrand> _BrandRepo;
		private readonly IGenericRepository<ProductType> _TypeRepo;
		private readonly IMapper _Mapper;
        public ProductController(IProductRepository productRepository, IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            _productRepository = productRepository;
			_ProductRepo = productRepo;
			_BrandRepo = brandRepo;
			_Mapper = mapper;

        }

		[HttpGet]
		public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
		{
			var spec = new ProductWithTypesAndBrandsSpecification(productParams);
			var countSpec = new ProductWithFiltersForCount(productParams);
			var totalItems = await _ProductRepo.CountAsync(countSpec);

			var products = await _ProductRepo.ListSpecAsync(spec);

			var data = _Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
			//var data = Ok(products.Select(product => _Mapper.Map<Product, ProductDto>(product))); // another way
			return Ok (new Pagination<ProductDto> (productParams.PageIndex, productParams.PageSize, totalItems, data));
		}
		[HttpGet("test")]
		public IActionResult getTest ()
		{
			var product = new ProductDto { Id = 1, Name="test" };
			return Ok(product);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetProduct(int id)
		{
			var spec = new ProductWithTypesAndBrandsSpecification(id);
			var product = await _ProductRepo.GetEntityWithSpecAsync(spec);
			return Ok(_Mapper.Map<Product, ProductDto>(product));
		}

		[HttpGet ("Brands")]
		public async Task<ActionResult<List<ProductBrand>>> GetProductsBrand() =>
	       Ok(await _BrandRepo.ListAllAsync());

		[HttpGet ("Types")]	
		public async Task<ActionResult<List<ProductType>>> GetProductTypes() =>
          Ok(await _TypeRepo.ListAllAsync());


	}
}
