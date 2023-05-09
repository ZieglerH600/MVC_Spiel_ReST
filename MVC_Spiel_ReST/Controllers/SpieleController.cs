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
        //Alle Spiele abrufen als Liste und als JsonResult an den Client geschickt
        
        [HttpGet("/api/AllGames")]
        public IActionResult AllGames()
        {
            List<Spiel> AlleSpiele = new List<Spiel>();
            AlleSpiele = dataAccess.GetAllGames();

            return new JsonResult(AlleSpiele);
        }
       
        [HttpGet("/api/GetAllPublisher")]
        public IActionResult GetAllPublisher()
        {
            List<Publisher> publishers = new List<Publisher>();
            publishers = dataAccess.GetAllPublisher();
            
            return new JsonResult(publishers);
        }

        
        [HttpGet("/api/GetAllTyp")]
        public IActionResult GetAllTyp()
        {
            List<Typ> type = new List<Typ>();
            type = dataAccess.GetAllTyp();

            return new JsonResult(type);
        }

        //Sucht das Spiel mithilfe der ID raus und schickt das Ergebnis als JsonResult an den Client
        
        [HttpGet("/api/GameByID/{SIP}")]
        public IActionResult GameByID(int SIP)
        {
            Spiel spiel = dataAccess.GetGameByID(SIP);

            return new JsonResult(spiel);
        }

        //Fügt ein Spiel hinzu und sendet das Ergebnis an den Client
        
        [HttpPost("/api/InsertGame")]
        public IActionResult InsertGame([FromBody]string value)
        {
            Spiel spiel = System.Text.Json.JsonSerializer.Deserialize<Spiel>(value);

            dataAccess.InsertGame(spiel);

            return new JsonResult(spiel);

        }

        //Ändert die Daten eines Spiels
        
        [HttpPut("/api/UpdateGame/{SIP}")]
        public async Task<IActionResult> UpdateGame(int SIP, [FromBody] string value)
        {
            Spiel spiel = System.Text.Json.JsonSerializer.Deserialize<Spiel>(value);
            bool result=await dataAccess.UpdateGame(spiel);

            return new JsonResult(result);
        }

        //Löscht ein Spiel aus der Datenbank vom Client ausgehend       
      
        [HttpDelete("/api/DeleteGame/{SIP}")]
        public async Task<IActionResult> DeleteGame(int SIP)
        {
            bool result = await dataAccess.DeleteGameByID(SIP);
            
            
            return new JsonResult(result);

        }

    }
}
