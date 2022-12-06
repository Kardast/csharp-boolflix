using Microsoft.AspNetCore.Mvc.Rendering;

namespace csharp_boolflix.Models.Form
{
    public class SerieForm
    {
        public Serie Serie { get; set; }
        public List<SelectListItem>? Genres { get; set; }
        public List<int>SelectedGenres { get; set; }
        public List<SelectListItem>? Actors { get; set; }
        public List<int> SelectedActors { get; set; }
        public List<Director>? Directors { get; set; }
    }
}
