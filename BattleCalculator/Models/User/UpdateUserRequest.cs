using System.ComponentModel.DataAnnotations;

namespace BattleCalculator.Models.User
{
	public class UpdateUserRequest
	{

		[MinLength(2), RegularExpression(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$", ErrorMessage = "Le nom utilisé n'est pas valide.")]
		public string Username { get; set; }

		[RegularExpression(@"^[^\s@]+@([^\s@.,]+\.)+[^\s@.,]{2,}$", ErrorMessage = "Le valeur renseignée n'est pas une adresse mail valide.")]
		public string Email { get; set; }

		[MinLength(6)]
		public string Password { get; set; }
	}
}
