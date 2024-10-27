using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.DTO;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Controllers
{
	public class UserAuthenticationController : Controller
	{
		private readonly IUserAuthenticationService _service;
        public UserAuthenticationController(IUserAuthenticationService service)
        {
			this._service = service;
        }

        public IActionResult Login()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}
			var result =await _service.LoginAsync(model);
			if(result.StatusCode==1)
			{
				return RedirectToAction("Display","Dashboard");
			}
			else
			{
				TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        public IActionResult Registration()
        {

            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Registration(RegistrationModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			model.Role = "user";
			var result = await _service.RegistrationAsync(model);
			TempData["msg"] = result.Message;
			return RedirectToAction(nameof(Registration));

		}

        public async Task<IActionResult> Logout()
		{
			await _service.LogoutAsync();
			return RedirectToAction(nameof(Login));
		}
        //public async Task<IActionResult> RegisterAdmin()
        //{
        //	var model = new RegistrationModel
        //	{
        //		UserName = "admin",
        //		Name = "mohamed",
        //		Email = "mohamed@gmail.com",
        //		Password = "Admin@12345#"  
        //	};
        //          model.Role = "admin";
        //          var result = await _service.RegistrationAsync(model);
        //          return Ok(result);

        //      }
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _service.ChangePasswordAsync(model, User.Identity.Name);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
