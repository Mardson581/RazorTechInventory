using System.ComponentModel.DataAnnotations;

namespace TechInventory.Models;

public class MaintenanceRecord
{
    [Key]
    public int MaintenanceRecordId { get; set; }

    [Required(ErrorMessage="O registro de manutenção deve ter um dispositivo associado")]
    public int DeviceId { get; set; }
    public Device Device { get; set; }

    [Required(ErrorMessage="O registro de manutenção deve ter uma data")]
    public DateTime Data { get; set; }

    [Required(ErrorMessage="O registro de manutenção deve ter uma descrição")]
    [MinLength(5, ErrorMessage="A descrição deve ter no mínimo 5 caracteres")]
    [MaxLength(500, ErrorMessage="A descrição deve ter no máximo 500 caracteres")]
    public string Description { get; set; }

    [Required(ErrorMessage="O registro deve ter algum técnico responsável")]
    [MinLength(5, ErrorMessage="O nome do técnico deve ter no mínimo 5 caracteres")]
    [MaxLength(100, ErrorMessage="O nome do técnico deve ter no máximo 100 caracteres")]
    public string Technician { get; set; }
}