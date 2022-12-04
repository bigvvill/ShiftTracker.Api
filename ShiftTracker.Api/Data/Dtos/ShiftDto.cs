using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftTracker.Api.Data.Dtos
{
    public class ShiftDto
    {
        [Required]
        public int ShiftId { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Pay { get; set; }
        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Minutes { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
