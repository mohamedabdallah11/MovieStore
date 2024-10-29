using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.Domain;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class GenreController : Controller
    {   private  readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public IActionResult Index()
        {
            var result = _genreService.GetAllGeneries().ToList();
            return View(result);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Genre model)

        {
            if (!ModelState.IsValid)
            {  
                return View(model);
            }
           var result = _genreService.Add(model);
            if (result)
            {
                TempData["msg"] = "Successfully Added";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Couldn't Added";
                return View(model);
            }
        }    
        public IActionResult Edit(int id )
        {
            var result = _genreService.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
            {  
                return View(model);
            }
           var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Successfully Updated";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["msg"] = "Couldn't Update";
                return View(model);
            }
        }
        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);
           
                TempData["msg"] = result?"Successfully Deleted": "Couldn't Delete";
                     
            return RedirectToAction(nameof(Index));

        }
    }
}
