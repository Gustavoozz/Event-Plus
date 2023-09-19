using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.event_.tarde.Domains
{
    [Table(nameof(Instituicao))]
    [Index(nameof(CNPJ), IsUnique = true)]
    public class Instituicao
    {
        [Key]
        public Guid IdInstituicao { get; set; } = Guid.NewGuid();


        [Column(TypeName = "CHAR(100)")]
        [Required(ErrorMessage = "CNPJ Obrigatório!")]
        [StringLength(14)]
        public string? CNPJ { get; set; }


        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "Endereço obrigatório!")]
        public string? Endereco { get; set; }


        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "O nome fantasia é obrigatório!")]
        public string? NomeFantasia { get; set; }


    }
}
