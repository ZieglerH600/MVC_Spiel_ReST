using Microsoft.AspNetCore.Mvc;
using MVC_Spiel_ReST.Models;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_Spiel_ReST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpieleController : ControllerBase
    {
        readonly IDaten dataAccess;
        public SpieleController(IConfiguration configuration)
        {
            dataAccess = new SQLDAL(configuration.GetConnectionString("SQLConnection"));
        }
        // GET: api/<SpieleController>
        [Route("/api/AllGames")]
        [HttpGet]
        public IActionResult AllGames()
        {
            List<Spiel> AlleSpiele = new List<Spiel>();
            AlleSpiele = dataAccess.GetAllGames();

            return new JsonResult(AlleSpiele);
        }

        // GET api/<SpieleController>/5
        [Route("/api/Games/{SIP}")]
        [HttpGet("{SIP}")]
        public IActionResult GameByID(int SIP)
        {
            Spiel spiel = dataAccess.GetGameByID(SIP);

            return new JsonResult(spiel);
        }

        // POST api/<SpieleController>
        [HttpPost]
        public IActionResult InsertGame([FromBody] string value)
        {
            Spiel spiel = System.Text.Json.JsonSerializer.Deserialize<Spiel>(value);

            dataAccess.InsertGame(spiel);

            return new JsonResult(spiel);

        }

        // PUT api/<SpieleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SpieleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
