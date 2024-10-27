using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models.DTO
{
	public class LoginModel
	{
        [Required]
        public String UserName { get; set; }
		[Required]
		public String Password { get; set; }
    }
}
