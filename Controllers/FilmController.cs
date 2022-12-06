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

    public class FilmController : Controller
    {
        BoolflixDbContext db;
        DbContentRepository boolflixRepository;

        public FilmController(DbContentRepository _boolflixRepository, BoolflixDbContext _db) : base()
        {
            db = _db;

            boolflixRepository = _boolflixRepository;
        }

        //index page
        public IActionResult Index()
        {
            List<Film> listFilm = boolflixRepository.AllFilms();
            return View(listFilm);
        }

        //create page
        public IActionResult Create()
        {
            ContentForm formData = new ContentForm();

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
    }
}
