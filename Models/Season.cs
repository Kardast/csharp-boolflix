namespace csharp_boolflix.Models
{
    public class Season
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SerieId { get; set; }
        public Serie? Serie { get; set; }
        public List<Episode>? Episodes { get; set; }
    }
}
