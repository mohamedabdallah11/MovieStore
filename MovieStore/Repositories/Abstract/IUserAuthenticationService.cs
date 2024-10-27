using MovieStore.Models.DTO;

namespace MovieStore.Repositories.Abstract
{
	public interface IUserAuthenticationService
	{
		Task<StatusModel> LoginAsync(LoginModel model);
		Task<StatusModel> RegistrationAsync(RegistrationModel model);
		Task LogoutAsync();
	}
}
 