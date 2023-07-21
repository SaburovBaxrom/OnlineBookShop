using System.ComponentModel.DataAnnotations;

namespace OnlineBookShopMvc.ViewModel
{
	public class AddRoleVM
	{
		[Required]
		[Display(Name = "Role")]
		public string RoleName { get; set; }
	}
}
