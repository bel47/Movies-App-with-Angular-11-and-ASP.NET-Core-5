using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces
{
    public interface IRepository
    {
       Task<List<Genre>> GetAllGenres();
        Genre GetGenreById(int id);
    }
}
