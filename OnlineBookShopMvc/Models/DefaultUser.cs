using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookShopMvc.Models
{
	public class DefaultUser : IdentityUser
	{
		[PersonalData]
		public string FirstName { get; set; }

		[PersonalData]
		public string LastName { get; set; }

		[PersonalData]
		public string Address { get; set; }

		[PersonalData]
		public string City { get; set; }
		
		[PersonalData]
		[DataType(DataType.Date)]
		public DateTime	UserCreationDate{ get; set; } = DateTime.Now;
	}
}
