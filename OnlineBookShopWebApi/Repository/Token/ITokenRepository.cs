using Microsoft.AspNetCore.Identity;

namespace OnlineBookShopWebApi.Repository.Token
{
	public interface ITokenRepository
	{
		string CreateJwtToken(IdentityUser identityUser, List<string> roles);
	}
}
