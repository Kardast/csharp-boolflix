namespace csharp_boolflix.Models
{
    public class Genre
    {    
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Content> Contents { get; set; }

    }
}
