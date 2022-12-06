namespace csharp_boolflix.Models.Repositories
{
    public interface IContentRepository
    {
        //film
        List<Film> AllFilms();
        void CreateFilm(Film film, List<int> selectedActors, List<int> selectedGenres);
        Film GetFilmById(int id);
        void DeleteFilm(Film film);

        //serie
        List<Serie> AllSeries();
        void CreateSerie(Serie serie, List<int> selectedActors, List<int> selectedGenres);
        Serie GetSerieById(int id);
        void DeleteSerie(Serie serie);
    }
}