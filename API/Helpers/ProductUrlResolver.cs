using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
	public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
	{
		IConfiguration _config;
        public ProductUrlResolver(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.ImagePath)) 
			{
				return _config["ApiUrl"] + source.ImagePath;
			}
			return null;
			
		}
	}
}
