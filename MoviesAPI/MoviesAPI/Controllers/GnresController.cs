using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GnresController : ControllerBase
    {
        private readonly ILogger<GnresController> _logger;
        private readonly AppliactionDbContext _context;
        private readonly IMapper _mapper;

        public GnresController(ILogger<GnresController> logger, AppliactionDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        //[ResponseCache(Duration = 60)]
        public async Task<List<GenreDTO>> Get()
        {
            _logger.LogInformation("Getting all the genres");
            var genres = await _context.Genres.OrderBy(x => x.Name).ToListAsync();
            /* var genreDTOs = new List<GenreDTO>();
                foreach (var genre in genres)
                {
                    genreDTOs.Add(new GenreDTO { Id = genre.Id, Name = genre.Name });
                }
                return genreDTOs;*/
            return _mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                return NoContent();
            }
            return _mapper.Map<GenreDTO>(genre);
        }

        [HttpPost]
        public async Task<ActionResult> post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = _mapper.Map<Genre>(genreCreationDTO);
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = _mapper.Map<Genre>(genreCreationDTO);
            genre.Id = id;
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null) {
                return NotFound();
            }
            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
