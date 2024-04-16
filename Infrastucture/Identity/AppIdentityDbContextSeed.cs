using Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser
				{
					DisplayName = "Amin",
					Email = "Aminelsayed202020@gmail.com",
					UserName = "Amin123",
					Address = new Address
					{
						FirstName = "Amin",
						LastName = "Elsayed",
						Street = "Elmohandes",
						City = "Cairo",
						ZipCode = "1234"
					}
				};
				await userManager.CreateAsync(user, "Password");
			    
			  
			}
		}
	}
}
