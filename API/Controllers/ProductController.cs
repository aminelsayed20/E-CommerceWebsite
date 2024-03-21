using API.Controllers;
using API.Dtos;
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
		public async Task<ActionResult<List<ProductDto>>> GetProducts()
		{
			var spec = new ProductWithTypesAndBrandsSpecification();
			var products = await _ProductRepo.ListSpecAsync(spec);
			//return Ok(products.Select(product => _Mapper.Map<Product, ProductDto>(product))); // another way
			return Ok (_Mapper.Map<IReadOnlyList <Product>, IReadOnlyList< ProductDto>>(products));
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
