using System;
using System.Collections.Generic;

namespace Module4_Test.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Title { get; set; }

        public virtual List<Song> Songs { get; set; } = new List<Song>();
    }
}
