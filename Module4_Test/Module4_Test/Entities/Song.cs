using System;
using System.Collections.Generic;

namespace Module4_Test.Entities
{
    public class Song
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ReleasedDate { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual List<Artist> Artists { get; set; } = new List<Artist>();
    }
}
