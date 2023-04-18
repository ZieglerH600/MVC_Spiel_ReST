namespace MVC_Spiel_ReST.Models
{
    public class Spiel
    {
        public int SIP { get; set; }
        public int TID { get; set; }
        public int PID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int SpielerMin { get; set; }
        public int SpielerMax { get; set; }
        public double Rating { get; set; }
        public string SpieleTyp { get; set; }
        public string SpielPublisher { get; set; }
    }
}
