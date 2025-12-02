using System.ComponentModel.DataAnnotations;

namespace TechInventory.Models;

public class Device
{
    [Key]
    public int DeviceId { get; set; }

    [Required(ErrorMessage="O nome do dispositivo é obrigatório")]
    [MinLength(5, ErrorMessage="O nome do dispositivo deve ter no mínimo 5 caracteres")]
    [MaxLength(50, ErrorMessage="O nome do dispositivo deve ter no máximo 50 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage="O tipo do dispositivo é obrigatório")]
    public DeviceType DeviceType { get; set; }

    [Required(ErrorMessage="O status do dispositivo é obrigatório")]
    public DeviceStatus Status { get; set; }

    [Required(ErrorMessage="O modelo do dispositivo é obrigatório")]
    public int DeviceModelId { get; set; }

    public DeviceModel Model { get; set; }
}