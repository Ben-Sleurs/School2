using MVCBibliotheekBENSLE.CustomValidation;

namespace MVCBibliotheekBENSLE.Models.Data
{
    public class Reservatie
    {
        public int? ReservatieId { get; set; }
        public int? VergaderZaalId { get; set; }
        public int? GebruikerId { get; set; }
        [CustomReservatieDateValidation]
        public DateTime? Datum { get; set; }
        public VergaderZaal? VergaderZaal { get; set; }
        public Gebruiker? Gebruiker { get; set; }
    }
}
