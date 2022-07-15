using MoviesAPI.Entities;
using MoviesAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Genre> _genres;
        public InMemoryRepository()
        {
            _genres = new List<Genre>()
            {
                new Genre(){ Id=1,Name="Action"},
                new Genre(){ Id=2,Name="Commedy"}
            };
        }

        public async Task<List<Genre>> GetAllGenres() => _genres;
        public Genre GetGenreById(int id) => _genres.FirstOrDefault(x => x.Id == id);

    }
}
