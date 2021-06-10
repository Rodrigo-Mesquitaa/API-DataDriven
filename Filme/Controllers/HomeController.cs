using Filme.Data;
using Filme.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Filme.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get(
            [FromServices] DataContext context
        )
        {
            var employee = new User { Id = 1, Username = "Batman", Password = "Batman", Role = "employee" };
            var manager = new User { Id = 2, Username = "Vingadores", Password = "Vingadores", Role = "manager" };
            var category = new Category { Id = 1, Title = "Informática" };
            var product = new Movie { Id = 1, Category = category, Title = "Terror", Price = 299, Description = "Terror in Sallent Hill" };
            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Movies.Add(product);
            await context.SaveChangesAsync();

            return Ok(new { message = "Dados configurados" });
        }
    }
}