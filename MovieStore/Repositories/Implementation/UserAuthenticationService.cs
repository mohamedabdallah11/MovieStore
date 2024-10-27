using Microsoft.AspNetCore.Identity;
using MovieStore.Models.DTO;
using MovieStore.Models.Domain;
using MovieStore.Repositories.Abstract;
using System.Security.Claims;

namespace MovieStore.Repositories.Implementation

{
	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		public UserAuthenticationService(RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}
		public async Task<StatusModel> LoginAsync(LoginModel model)
		{
			var status = new StatusModel();
			var  user = await userManager.FindByNameAsync(model.UserName);
			if (user == null)
			{
				status.StatusCode = 0;
				status.Message = "Invalid UserName";
				return status;
			}
			if(!await userManager.CheckPasswordAsync(user, model.Password))
			{
				status.StatusCode = 0;
				status.Message = "Invalid Password";
				return status;
			}
			var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
			if (signInResult.Succeeded)
			{
				var userRoles = await signInManager.UserManager.GetRolesAsync(user);
				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,user.UserName)
				};
				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));

				}
				status.StatusCode = 1;
				status.Message = "Logged In Successfully";
				return status;
			}
			else if (signInResult.IsLockedOut)
			{
				status.StatusCode = 0;
				status.Message = "User LoggedOut";
				return status;
			}
			else
			{
				status.StatusCode = 0;
				status.Message = "Error in logged in ";
				return status;
			}

		}

		public async Task LogoutAsync()
		{
			await signInManager.SignOutAsync();
		}

		public async Task<StatusModel> RegistrationAsync(RegistrationModel model)
		{
			var status = new StatusModel();
			var userExists = await userManager.FindByNameAsync(model.UserName);
			if (userExists != null)
			{
				status.Message = "User Already Exist";
				status.StatusCode = 0;
				return status;
			}
			ApplicationUser user = new ApplicationUser
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				Name = model.UserName,
				Email = model.Email,
				UserName = model.UserName,
				EmailConfirmed = true,
			};
			var result = await userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded) 
			{
				status.StatusCode = 0;
				status.Message = " User Creation Failed";
				return status;
			}

			if (!await roleManager.RoleExistsAsync(model.Role))
				await roleManager.CreateAsync(new IdentityRole(model.Role));

			if(await roleManager.RoleExistsAsync(model.Role))
			{
				await userManager.AddToRoleAsync(user, model.Role);
			}

			status.StatusCode = 1;
			status.Message = "User Created Sucessfully";
			return status;
		}
        public async Task<StatusModel> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            var status = new StatusModel();

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                status.Message = "User does not exist";
                status.StatusCode = 0;
                return status;
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                status.Message = "Password has updated successfully";
                status.StatusCode = 1;
            }
            else
            {
                status.Message = "Some error occcured";
                status.StatusCode = 0;
            }
            return status;

        }
    }
}