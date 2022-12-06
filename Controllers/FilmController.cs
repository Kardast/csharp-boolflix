using csharp_boolflix.Data;
using csharp_boolflix.Models;
using csharp_boolflix.Models.Form;
using csharp_boolflix.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace csharp_boolflix.Controllers
{
    [Authorize]
    [Route("[controller]/[action]/{id?}", Order = 0)]
    public class FilmController : Controller
    {
        BoolflixDbContext db;
        IContentRepository contentRepository;

        public FilmController(IContentRepository _contentRepository, BoolflixDbContext _db) : base()
        {
            db = _db;

            contentRepository = _contentRepository;
        }

        //index page
        public IActionResult Index()
        {
            List<Film> listFilm = contentRepository.AllFilms();
            return View(listFilm);
        }

        //details
        public IActionResult Details(int id)
        {

            Film film = contentRepository.GetFilmById(id);

            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        //create page
        public IActionResult Create()
        {
            FilmForm formData = new FilmForm();

            formData.Film = new Film();
            formData.Directors = db.Directors.ToList();
            formData.Actors = new List<SelectListItem>();
            formData.Genres = new List<SelectListItem>();

            List<Actor> actorList = db.Actors.ToList();

            foreach (Actor actor in actorList)
            {
                formData.Actors.Add(new SelectListItem(actor.Name, actor.Id.ToString()));
            }

            List<Genre> genreList = db.Genres.ToList();

            foreach (Genre genre in genreList)
            {
                formData.Genres.Add(new SelectListItem(genre.Name, genre.Id.ToString()));
            }

            return View(formData);
        }

        //create save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FilmForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Directors = db.Directors.ToList();

                formData.Actors = new List<SelectListItem>();
                List<Actor> actorsList = db.Actors.ToList();
                foreach (Actor actor in actorsList)
                {
                    formData.Actors.Add(new SelectListItem(actor.Name, actor.Id.ToString()));
                }

                formData.Genres = new List<SelectListItem>();
                List<Genre> genreList = db.Genres.ToList();
                foreach (Genre genre in genreList)
                {
                    formData.Genres.Add(new SelectListItem(genre.Name, genre.Id.ToString()));
                }
                return View(formData);
            }

            contentRepository.CreateFilm(formData.Film, formData.SelectedGenres, formData.SelectedActors);

            return RedirectToAction("Index");
        }

        //delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Film film = contentRepository.GetFilmById(id);
            if (film == null)
            {
                return NotFound();
            }

            contentRepository.DeleteFilm(film);

            return RedirectToAction("Index");
        }
    }
}
