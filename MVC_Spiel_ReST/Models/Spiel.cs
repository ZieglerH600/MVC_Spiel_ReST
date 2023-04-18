namespace MVC_Spiel_ReST.Models
{
    public class Spiel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public int MinSpieler { get; set; }
        public int MaxSpieler { get; set; }
        public string SpielPublisher { get; set; }
    }
}
