using System.ComponentModel.DataAnnotations;

namespace TechInventory.Models;

public class Brand
{
    [Key]
    public int BrandId { get; set; }

    [Required(ErrorMessage="O nome da marca é obrigatório")]
    [MinLength(2, ErrorMessage="O nome da marca deve ter no mínimo 2 caracteres")]
    [MaxLength(40, ErrorMessage="O nome da marca deve ter no máximo 40 caracteres")]
    public string BrandName { get; set; }
}