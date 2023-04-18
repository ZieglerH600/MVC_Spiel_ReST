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
        public bool DeleteGameByID(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool UpdateGame(Spiel Game)
        {
            throw new NotImplementedException();
        }
    }
}
