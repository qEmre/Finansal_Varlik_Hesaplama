using System.ComponentModel.DataAnnotations;

namespace indexTable.Models
{
    public class ufeEndeks
    {
        [Key]
        public int Id { get; set; }
        public double endeksOrani { get; set; }
        public double dolarKuru { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tarihi { get; set; }
    }
}
