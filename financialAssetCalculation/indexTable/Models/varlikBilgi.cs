using System.ComponentModel.DataAnnotations;

namespace indexTable.Models
{
    public class varlikBilgi
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Tutari { get; set; }
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Tarihi { get; set; }
    }
}
