using System.ComponentModel.DataAnnotations;

namespace MVCFifa2022.Models
{
    public class TeamPlayer
    {
        public int Id { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int? PlayerId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public Team Team { get; set; }
        public Player Player { get; set; }

    }
}
