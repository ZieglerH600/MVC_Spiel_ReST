using Dapper;
using Microsoft.Data.SqlClient;

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
            int affected = await Conn.ExecuteScalarAsync<int>(SelectStr, new { SIP = id });
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
            string SelectStr = $@"Select Spiel.*,Publisher.Bezeichnung AS SpielPublisher
                                ,Typ.Name as SpieleTyp
                                from Spiel inner join Publisher 
                                on (Publisher.PID=Spiel.PID) inner Join Typ 
                                on (Spiel.TID=Typ.TID) where Spiel.SIP=@SIP";
            return Conn.QuerySingle<Spiel>(SelectStr, new { SIP = id });
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
            select last_insert-rowid()";
            var anonym = new
            {
                TID = Game.TID,
                PID = Game.PID,
                Name = Game.Name,
                Erscheinungsjahr = Game.Date,
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
                Erscheinungsjahr  = Game.Date,
                SpielerMin = Game.SpielerMin,
                SpielerMax = Game.SpielerMax,
                Rating = Game.Rating,
                SIP = Game.SIP
            };
            int geklappt = await Conn.ExecuteAsync(SelectStr, anonym);
            if (geklappt > 0) { return true; } else { return false; }
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
