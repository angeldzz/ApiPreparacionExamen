using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ApiPreparacionExamen.Models
{
    [Table ("HOSPITAL")]
    public class Hospital
    {
        [Key]
        [Column ("HOSPITAL_COD")]
        public int HOSPITAL_COD { get; set; }
        [Column("NOMBRE")]
        public string NOMBRE { get; set; }
        [Column("DIRECCION")]
        public string DIRECCION { get; set; }
        [Column("TELEFONO")]
        public string TELEFONO { get; set; }
        [Column("NUM_CAMA")]
        public int NUM_CAMA { get; set; }

        [Column("Imagen")]
        public string? IMAGEN { get; set; }

        [NotMapped]
        public IFormFile? Imagen { get; set; }
    }
}
