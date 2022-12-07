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
    public class SerieController : Controller
    {
        BoolflixDbContext db;
        IContentRepository contentRepository;

        public SerieController(IContentRepository _contentRepository, BoolflixDbContext _db) : base()
        {
            db = _db;

            contentRepository = _contentRepository;
        }

        //index page
        public IActionResult Index()
        {
            List<Serie> listSerie = contentRepository.AllSeries();
            return View(listSerie);
        }

        //details
        public IActionResult Details(int id)
        {

            Serie serie = contentRepository.GetSerieById(id);

            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        //create page
        public IActionResult Create()
        {
            SerieForm formData = new SerieForm();

            formData.Serie = new Serie();
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
        public IActionResult Create(SerieForm formData)
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

            contentRepository.CreateSerie(formData.Serie, formData.SelectedGenres, formData.SelectedActors);

            return RedirectToAction("Index");
        }

        //delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Serie serie = contentRepository.GetSerieById(id);
            if (serie == null)
            {
                return NotFound();
            }

            contentRepository.DeleteSerie(serie);

            return RedirectToAction("Index");
        }

        //add season page
        public IActionResult AddSeason(int id)
        {
            Season season = new Season();
            season.SerieId = id;
            return View(season);
        }

        //add season save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSeason(Season season, int id)
        {
            season.Id = 0;
            season.SerieId = id;
            if (!ModelState.IsValid)
            {
                return View(season);
            }
            contentRepository.CreateSeason(season);
            return RedirectToAction("Details", new { id = season.SerieId });
        }

        //add episode page
        public IActionResult AddEpisode(int id)
        {
            Episode episode = new Episode();
            episode.SeasonId = id;
            return View(episode);
        }

        //add episode save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEpisode(Episode episode, int id)
        {
            episode.Id = 0;
            episode.SeasonId = id;
            if (!ModelState.IsValid)
            {
                return View(episode);
            }
            contentRepository.CreateEpisode(episode);
            return RedirectToAction("Index");
        }
    }
}
