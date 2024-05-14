using System.ComponentModel.DataAnnotations;

namespace indexTable.Models
{
    public class hesaplanmisOranlar
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Tarihi { get; set; }
        public double varlikTutari { get; set; }
        public double oncekiAyaGoreArtis { get; set; }
        public double degisimOrani { get; set; }
        public double varlikTarihiDolarKuru { get; set; }
        public double dolarizasyonVarlikTutari { get; set; }
        public double dolarizasyonOncekiAyaGoreArtis { get; set; }
        public double dolarizasyonVarlikDegisimOrani { get; set; }
        public double dolarizasyonEtkisiYüzde { get; set; }
        public double endeksOran { get; set; }
        public double enflasyonVarlikTutari { get; set; }
        public double enflasyonOncekiAyaGorevarlikArtis { get; set; }
        public double enflasyonVarlikDegisimOrani { get; set; }
        public double enflasyonEtkisiYüzde { get; set; }
    }
}
