namespace csharp_boolflix.Models.Repositories
{
    public interface IContentRepository
    {
        List<Film> AllFilms();
        List<Serie> AllSeries();
        void CreateFilm(Film film, List<int> selectedActors, List<int> selectedGenres);
        void CreateSerie(Serie serie, List<int> selectedActors, List<int> selectedGenres, Season season);
        void Delete(Film film);
        Film GetFilmById(int id);
        Serie GetSerieById(int id);
    }
}