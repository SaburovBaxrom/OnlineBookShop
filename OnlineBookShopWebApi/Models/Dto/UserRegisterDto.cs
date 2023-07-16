using System.ComponentModel.DataAnnotations;

namespace OnlineBookShopWebApi.Models.Dto
{
	public class UserRegisterDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string[] Roles { get; set; }
	}
}
