using Dapper;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace MVC_Spiel_ReST.Models
{
    public class SQLDAL : IDaten
    {
        readonly SqlConnection Conn;
        public SQLDAL(string Connstr)
        {

            Conn = new SqlConnection(Connstr);
        }
        public async Task<bool> DeleteGameByID(int id)
        {
            string SelectStr = $@"Delete Spiel where SIP=@SIP";
            int affected = await Conn.ExecuteAsync(SelectStr, new { SIP = id });
            if (affected > 0) { return true; } else { return false; }
        }

        public List<Spiel> GetAllGames()
        {
            string SelectStr = $@"Select Spiel.*,Publisher.Bezeichnung AS SpielPublisher
                                ,Typ.Name as SpieleTyp
                                from Spiel inner join Publisher 
                                on (Publisher.PID=Spiel.PID) inner Join Typ 
                                on (Spiel.TID=Typ.TID)";
            return Conn.Query<Spiel>(SelectStr).ToList();
        }

        public Spiel GetGameByID(int id)
        {
            string Prüfung = $@"Select Count(*) from Spiel where SIP=@SIP";
            int anzahl = Conn.QuerySingle<int>(Prüfung, new { SIP = id });
            if (anzahl == 0)
            {
                return new Spiel{ TID = 1,
                PID = 1,
                Name = "Nicht Vorhanden",
                Erscheinungsjahr = "0000",
                SpielerMin = 1,
                SpielerMax = 1,
                Rating = 1,
                SIP = id};
            }
            else
            {
                string SelectStr = $@"Select Spiel.*,Publisher.Bezeichnung AS SpielPublisher
                                ,Typ.Name as SpieleTyp
                                from Spiel inner join Publisher 
                                on (Publisher.PID=Spiel.PID) inner Join Typ 
                                on (Spiel.TID=Typ.TID) where Spiel.SIP=@SIP";
                return Conn.QuerySingle<Spiel>(SelectStr, new { SIP = id });
            }
        }

        public int InsertGame(Spiel Game)
        {
            string SelectStr = $@"Insert into Spiel 
            (TID,
            PID,
            Name,
            Erscheinungsjahr,
            SpielerMin,
            SpielerMax,
            Rating) 
            Values (@TID,
            @PID,
            @Name,
            @Erscheinungsjahr,
            @SpielerMin,
            @SpielerMax,
            @Rating);
            ";
            var anonym = new
            {
                TID = Game.TID,
                PID = Game.PID,
                Name = Game.Name,
                Erscheinungsjahr = Game.Erscheinungsjahr,
                SpielerMin = Game.SpielerMin,
                SpielerMax = Game.SpielerMax,
                Rating = Game.Rating
            };
            int PK = Conn.ExecuteScalar<int>(SelectStr, anonym);
            return PK;
        }

        public async Task<bool> UpdateGame(Spiel Game)
        {
          
                string SelectStr = $@"Update Spiel SET
            TID=@TID,
            PID=@PID,
            Name=@Name,
            Erscheinungsjahr=@Erscheinungsjahr,
            SpielerMin=@SpielerMin,
            SpielerMax=@SpielerMax,
            Rating=@Rating
            where SIP=@SIP;";
                var anonym = new
                {
                    TID = Game.TID,
                    PID = Game.PID,
                    Name = Game.Name,
                    Erscheinungsjahr = Game.Erscheinungsjahr,
                    SpielerMin = Game.SpielerMin,
                    SpielerMax = Game.SpielerMax,
                    Rating = Game.Rating,
                    SIP = Game.SIP
                };
                return (await Conn.ExecuteAsync(SelectStr, anonym)>0);
               
            
        }
        public List<Publisher> GetAllPublisher()
        {
            string SelectStr = $@"Select * from Publisher";
            return Conn.Query<Publisher>(SelectStr).ToList();
        }
        public List<Typ> GetAllTyp()
        {
            string SelectStr = $@"Select * from Typ";
            return Conn.Query<Typ>(SelectStr).ToList();
        }
    }
}
