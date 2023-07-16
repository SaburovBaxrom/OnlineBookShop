using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopWebApi.Models.Dto;
using OnlineBookShopWebApi.Repository.Token;

namespace OnlineBookShopWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenRepository tokenRepository;
		public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
		{
			this.userManager = userManager;	
			this.tokenRepository = tokenRepository;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
		{
			var identityUser = new IdentityUser
			{
				UserName = userRegisterDto.Username,
				Email = userRegisterDto.Username
			};

			var identityResult = await userManager.CreateAsync(identityUser, userRegisterDto.Password);
			
			if(identityResult.Succeeded)
			{
				identityResult = await userManager.AddToRolesAsync(identityUser, userRegisterDto.Roles);
				if(userRegisterDto.Roles != null && userRegisterDto.Roles.Any()) 
				{
					return Ok("Registred. Please Log in");
				}
			}

			return BadRequest("somehting went wrong");
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var user = await userManager.FindByEmailAsync(loginDto.Username);

			if (user != null) 
			{
				var checkPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

				if (checkPassword)
				{
					var roles = await userManager.GetRolesAsync(user);

					if(roles != null)
					{
						var token = tokenRepository.CreateJwtToken(user, roles.ToList());
						var response = new TokenDto
						{
							Token = token
						};

						return Ok(response);
					}
				}
			}

			return BadRequest("username or password incorrect");
		}
	}
}
