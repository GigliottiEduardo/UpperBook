using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Upper.Models
{
    [Table("Livro")]
    public class Livro
    {
        [Column("Id")]
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Column("Name")]
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Título obrigatório")]
        public string Name { get; set; }

        [Column("Genero")]
        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Gênero obrigatório")]
        public string Genero { get; set; }

        [Column("Autor")]
        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Autor obrigatório")]
        public string Autor { get; set; }

        [Column("Editora")]
        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Editora obrigatória")]
        public string Editora { get; set; }

        [Column("Foto")]
        [Display(Name = "Foto")]
        public byte[]? Foto { get; set; }

        [NotMapped]
        [Display(Name = "Imagem Livro")]
        public IFormFile? ImagemLivro { get; set; }

        [NotMapped]
        [Display(Name = "Imagem")]
        public string? ImagemLivroBase64 { get; set; }

    }
}
