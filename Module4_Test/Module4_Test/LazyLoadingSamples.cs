using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Module4_Test
{
    public class LazyLoadingSamples
    {
        private readonly ApplicationContext _context;

        public LazyLoadingSamples(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Query1()
        {
            var songs = await _context.Songs
                .Select(x => new { Title = x.Title, ArtistName = string.Join(" ", x.Artists.Select(a => a.Name)), Genre = x.Genre.Title })
                .ToListAsync();

            Console.WriteLine("Songs data");
            foreach (var song in songs)
            {
                Console.WriteLine($"Song title: {song.Title}. Artists: {song.ArtistName}. Genre: {song.Genre}.");
            }
        }

        public async Task Query2()
        {
            var songsByGenre = await _context.Songs
                .GroupBy(s => s.Genre.Title)
                .Select(x => new { Title = x.Key, NumberOfSongs = x.Count() })
                .ToListAsync();

            Console.WriteLine("Songs data");
            foreach (var song in songsByGenre)
            {
                Console.WriteLine($"Song title: {song.Title}. Songs in genre: {song.NumberOfSongs}");
            }
        }

        public async Task Query3()
        {
            var youngestArtist = await _context.Artists
                .OrderByDescending(a => a.DateOfBirth)
                .FirstOrDefaultAsync();

            if (youngestArtist != null)
            {
                var songs = await _context.Songs
                .Where(s => s.ReleasedDate < youngestArtist.DateOfBirth)
                .ToListAsync();

                Console.WriteLine($"Artist: {youngestArtist.Name}");

                Console.WriteLine("Songs data");
                foreach (var song in songs)
                {
                    Console.WriteLine($"Song title: {song.Title}. Release date: {song.ReleasedDate}");
                }
            }
        }
    }
}
