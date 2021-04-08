using AutoMapper;
using FilmCatalog.API.DAL;
using FilmCatalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmCatalog.API.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenresController : ControllerBase
    {
        private readonly FilmCatalogContext _db;
        private readonly IMapper _automapper;

        public GenresController(FilmCatalogContext db, IMapper automapper)
        {
            this._db = db;
            this._automapper = automapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            var genresDB = await _db.Genres.ToListAsync();
            var dtos = _automapper.Map<List<GenreDTO>>(genresDB);
            return Ok(dtos);
        }


        [HttpGet("{id:int}", Name = "GetGenreById")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var genreDB = await _db.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (genreDB == null)
            {
                return NotFound();
            }

            var dtos = _automapper.Map<GenreDTO>(genreDB);
            return Ok(dtos);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreateDTO model)
        {
            try
            {
                var entity = _automapper.Map<Genre>(model);
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();

                var genreDTO = _automapper.Map<GenreDTO>(entity);

                return new CreatedAtRouteResult("GetGenreById", new { id = genreDTO.Id}, genreDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreateDTO model)
        {
            try
            {
                var entity = _automapper.Map<Genre>(model);
                entity.Id = id;
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var exist = await _db.Genres.AnyAsync(x => x.Id == id);

                if (!exist)
                    return NotFound();
                
                _db.Remove(new Genre() { Id = id });
                await _db.SaveChangesAsync();
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
