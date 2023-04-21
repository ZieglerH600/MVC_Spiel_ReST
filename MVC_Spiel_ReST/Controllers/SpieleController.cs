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
        [Route("/api/AllGames")]
        [HttpGet]
        public IActionResult AllGames()
        {
            List<Spiel> AlleSpiele = new List<Spiel>();
            AlleSpiele = dataAccess.GetAllGames();

            return new JsonResult(AlleSpiele);
        }
        [Route("/api/GetAllPublisher")]
        [HttpGet]
        public IActionResult GetAllPublisher()
        {
            List<Publisher> publishers = new List<Publisher>();
            publishers = dataAccess.GetAllPublisher();
            
            return new JsonResult(publishers);
        }

        [Route("/api/GetAllTyp")]
        [HttpGet]
        public IActionResult GetAllTyp()
        {
            List<Typ> type = new List<Typ>();
            type = dataAccess.GetAllTyp();

            return new JsonResult(type);
        }

        //Sucht das Spiel mithilfe der ID raus und schickt das Ergebnis als JsonResult an den Client
        [Route("/api/GameByID/{SIP}")]
        [HttpGet("{SIP}")]
        public IActionResult GameByID(int SIP)
        {
            Spiel spiel = dataAccess.GetGameByID(SIP);

            return new JsonResult(spiel);
        }

        //Fügt ein Spiel hinzu und sendet das Ergebnis an den Client
        [Route("/api/InsertGame")]
        [HttpPost]
        public IActionResult InsertGame([FromBody]string value)
        {
            Spiel spiel = System.Text.Json.JsonSerializer.Deserialize<Spiel>(value);

            dataAccess.InsertGame(spiel);

            return new JsonResult(spiel);

        }

        //Ändert die Daten eines Spiels
        [Route("/api/UpdateGame/{SIP}")]
        [HttpPut("{id}")]
        public IActionResult UpdateGame(int SIP, [FromBody] string value)
        {
            Spiel spiel = System.Text.Json.JsonSerializer.Deserialize<Spiel>(value);
            dataAccess.UpdateGame(spiel);

            return new JsonResult(spiel);
        }

        //Löscht ein Spiel aus der Datenbank vom Client ausgehend       
        [Route("/api/DeleteGame/{SIP}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int SIP)
        {
            dataAccess.DeleteGameByID(SIP);
            bool geklappt;
            if (SIP > 0) { geklappt= true; }else { geklappt= false; }
            return new JsonResult(geklappt);

        }

    }
}
