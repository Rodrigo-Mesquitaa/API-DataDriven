using Filme.Data;
using Filme.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filme.Controllers
{
    
    [Route("movies")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Movie>>> Get(
            [FromServices] DataContext context
        )
        {
            var movies = await context
                        .Movies
                        .Include(x => x.Category)
                        .AsNoTracking()
                        .ToListAsync();
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Movie>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            var movie = await context
                        .Movies
                        .Include(x => x.Category)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(movie);
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Movie>>> GetByCategory(
            [FromServices] DataContext context,
            int id
        )
        {
            var movies = await context
                        .Movies
                        .Include(x => x.Category)
                        .AsNoTracking()
                        .Where(x => x.CategoryId == id)
                        .ToListAsync();

            return Ok(movies);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<List<Movie>>> Post(
            [FromBody] Movie model,
            [FromServices] DataContext context

        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Adiciona os dados
                context.Movies.Add(model);

                // Persiste os dados no banco
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o produto" });
            }
        }
    }
}