using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
		public String Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
