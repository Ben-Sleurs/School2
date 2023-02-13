using System.ComponentModel.DataAnnotations;
using WebAppMVCClientLocationEFCore.CustomModelValidation;

namespace WebAppMVCClientLocationEFCore.Models
{
    public class Client
    {
        public int? ClientId { get; set; }
        [Required]
        public int? LocationId { get; set; }
        [StringLength(50)]
        [CustomNoNumbers]
        public string? ClientName { get; set; }

    }
}
