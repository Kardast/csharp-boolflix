﻿namespace csharp_boolflix.Models
{
    public abstract class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public DateTime Year { get; set; }
        public string Image { get; set; }

        public List<Genre> Genres { get; set; }
        public List<Actor> Actors { get; set; }


    }
}