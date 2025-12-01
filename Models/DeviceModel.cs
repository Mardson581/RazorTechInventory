using System.ComponentModel.DataAnnotations;

namespace TechInventory.Models;

public class DeviceModel
{
    [Key]
    public int DeviceModelId { get; set; }

    [Required(ErrorMessage="O nome do modelo é obrigatório")]
    [MinLength(2, ErrorMessage="O nome do modelo deve ter no mínimo 2 caracteres")]
    [MaxLength(50, ErrorMessage="O nome do modelo deve ter no máximo 50 caracteres")]
    public string ModelName { get; set; }

    [Required(ErrorMessage="A marca do dispositivo é obrigatória")]
    public int BrandId { get; set; }

    public Brand Brand { get; set; }
}