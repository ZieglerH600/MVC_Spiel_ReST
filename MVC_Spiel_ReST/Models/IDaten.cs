namespace MVC_Spiel_ReST.Models
{
    public interface IDaten
    {
        List<Spiel> GetAllGames();
        Spiel GetGameByID(int id);
        int InsertGame(Spiel Game);//Gibt die ID zurück
        bool UpdateGame(Spiel Game);
        bool DeleteGameByID(int id);
    }
}
