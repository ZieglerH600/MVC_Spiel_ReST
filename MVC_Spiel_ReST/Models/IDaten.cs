namespace MVC_Spiel_ReST.Models
{
    public interface IDaten
    {
        List<Spiel> GetAllGames();
        Spiel GetGameByID(int id);
        int InsertGame(Spiel Game);//Gibt die ID zurück
        Task<bool> UpdateGame(Spiel Game);
        Task<bool> DeleteGameByID(int id);
        List<Publisher> GetAllPublisher();
        List<Typ> GetAllTyp();
    }
}
