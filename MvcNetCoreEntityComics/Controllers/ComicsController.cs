using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEntityComics.Models;
using MvcNetCoreEntityComics.Repositories;

namespace MvcNetCoreEntityComics.Controllers
{
    public class ComicsController : Controller
    {
        private IRepositoryComics repo;
        public ComicsController(IRepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {   
            List<ComicView> comics = await this.repo.GetComicsAsync();
            return View(comics);
        }
        public async Task<IActionResult> Details(int id)
        {
            ComicView comic = await this.repo.FindComicAsync(id);
            return View(comic);
        }
        public async Task<IActionResult> InsertComic()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertComic(ComicView comic)
        {
            if (ModelState.IsValid)
            {
                int newComicId = await this.repo.InsertComicAsync(comic.Nombre, comic.Imagen, comic.Descripcion);
                return RedirectToAction(nameof(Details), new { id = newComicId });
            }
            return View(comic);
        }
    }
}
