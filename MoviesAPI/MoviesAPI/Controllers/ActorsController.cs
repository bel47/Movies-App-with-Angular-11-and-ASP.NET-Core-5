using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using MoviesAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly AppliactionDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string contanerName = "actors";

        public ActorsController(AppliactionDbContext context, IMapper mapper, IFileStorageService fileStorageService)
        {
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<List<ActorDTO>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Actors.AsQueryable();
            await HttpContext.InsertParamewtersPaginationInHeader(queryable);

            var actors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            /* var genreDTOs = new List<GenreDTO>();
                foreach (var genre in genres)
                {
                    genreDTOs.Add(new GenreDTO { Id = genre.Id, Name = genre.Name });
                }
                return genreDTOs;*/
            return _mapper.Map<List<ActorDTO>>(actors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
            {
                return NoContent();
            }
            return _mapper.Map<ActorDTO>(actor);
        }

        [HttpPost]
        public async Task<ActionResult> post([FromForm] ActorCreationDTO actorCreationDTO)
        {
            var actor = _mapper.Map<Actor>(actorCreationDTO);

            if (actorCreationDTO != null)
            {
                actor.Picture = await _fileStorageService.SaveFile(contanerName, actorCreationDTO.Picture);
            }
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(int id, [FromForm] ActorCreationDTO actorCreationDTO)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            actor = _mapper.Map(actorCreationDTO, actor);
            if (actorCreationDTO.Picture != null)
            {
                actor.Picture = await _fileStorageService.EditFile(contanerName, actorCreationDTO.Picture, actor.Picture);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            _context.Remove(actor);
            await _context.SaveChangesAsync();
            await _fileStorageService.DeleteFIle(actor.Picture, contanerName);
            return NoContent();
        }

    }
}
