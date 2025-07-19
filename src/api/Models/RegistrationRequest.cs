
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class RegistrationRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string RegistrationPlates { get; set; }

        public int CityId { get; set; }
        public int ZoneId { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
