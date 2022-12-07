using csharp_boolflix.Data;
using Microsoft.EntityFrameworkCore;

namespace csharp_boolflix.Models.Repositories
{
    public class DbContentRepository : IContentRepository
    {
        BoolflixDbContext db;

        public DbContentRepository(BoolflixDbContext _db)
        {
            db = _db;
        }

        //Film
        public List<Film> AllFilms()
        {
            return db.Films.Include(film => film.Actors).Include(film => film.Genres).Include(film => film.Director).ToList();
        }
        public Film GetFilmById(int id)
        {
            return db.Films.Where(f => f.Id == id).Include("Actors").Include("Genres").Include("Director").FirstOrDefault();
        }
        public void CreateFilm(Film film, List<int> selectedActors, List<int> selectedGenres)
        {

            film.Actors = new List<Actor>();

            foreach (int actorId in selectedActors)
            {
                Actor actor = db.Actors.Where(a => a.Id == actorId).FirstOrDefault();
                film.Actors.Add(actor);
            }

            film.Genres = new List<Genre>();

            foreach (int genreId in selectedGenres)
            {
                Genre genre = db.Genres.Where(g => g.Id == genreId).FirstOrDefault();
                film.Genres.Add(genre);
            }


            db.Films.Add(film);
            db.SaveChanges();
        }
        public void DeleteFilm(Film film)
        {
            db.Films.Remove(film);
            db.SaveChanges();
        }

        //Serie
        public List<Serie> AllSeries()
        {
            return db.Series.Include(serie => serie.Actors).Include(serie => serie.Genres).Include(serie => serie.Seasons).Include(serie => serie.Director).ToList();
        }

        public Serie GetSerieById(int id)
        {
            return db.Series.Where(s => s.Id == id).Include("Actors").Include("Genres").Include("Seasons").Include("Director").Include("Seasons.Episodes").FirstOrDefault();
        }

        public void CreateSerie(Serie serie, List<int> selectedActors, List<int> selectedGenres)
        {

            serie.Actors = new List<Actor>();

            foreach (int actorId in selectedActors)
            {
                Actor actor = db.Actors.Where(a => a.Id == actorId).FirstOrDefault();
                serie.Actors.Add(actor);
            }

            serie.Genres = new List<Genre>();

            foreach (int genreId in selectedGenres)
            {
                Genre genre = db.Genres.Where(g => g.Id == genreId).FirstOrDefault();
                serie.Genres.Add(genre);
            }

            db.Series.Add(serie);
            db.SaveChanges();
        }

        public void DeleteSerie(Serie serie)
        {
            db.Series.Remove(serie);
            db.SaveChanges();
        }

        //seasons
        public void CreateSeason(Season season)
        {
            db.Seasons.Add(season);
            db.SaveChanges();
        }

        //public List<Season> GetSeasons(int id)
        //{
        //    return db.Seasons.Where(s => s.SerieId == id).ToList();
        //}

        //episodes
        public void CreateEpisode(Episode episode)
        {
            db.Episodes.Add(episode);
            db.SaveChanges();
        }

        //public List<Episode> GetEpisodes(int id)
        //{
        //    return db.Episodes.Where(e => e.SeasonId == id).ToList();
        //}
    }
}
