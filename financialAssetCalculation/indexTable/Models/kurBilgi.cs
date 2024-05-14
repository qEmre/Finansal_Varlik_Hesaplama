using System.ComponentModel.DataAnnotations;

namespace indexTable.Models
{
    public class kurBilgi
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Tutar { get; set; }
        public DateTime Tarih { get; set; }
    }
}
