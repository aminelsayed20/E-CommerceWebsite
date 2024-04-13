using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustomerBasket(id));
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>>UpdateBasket(CustomerBasket basket)
		{
			var ubdateedBasket = await _basketRepository.UpdateBasketAsync(basket);
			return Ok(ubdateedBasket);
		}
		[HttpDelete]
		public async Task DeleteBasketAsync(string id)
		{
			await _basketRepository.DeleteBasketAsync(id);
		}
    }
}
