using System.ComponentModel.DataAnnotations;

namespace MVC_Spiel_ReST.Models
{
    public class Spiel
    {
        
        public int SIP { get; set; }
        [Required]
        public int TID { get; set; }
        [Required]
        public int PID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Erscheinungsjahr { get; set; }
        public int SpielerMin { get; set; }
        public int SpielerMax { get; set; }
        public double Rating { get; set; }
        public string SpieleTyp { get; set; }
        public string SpielPublisher { get; set; }
    }
}
