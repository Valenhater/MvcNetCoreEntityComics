using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcNetCoreEntityComics.Models
{
    [Table("V_COMICS")]
    public class ComicView
    {
        [Key]
        [Column("IDCOMIC")]
        public int IdComic { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }       
    }
}
