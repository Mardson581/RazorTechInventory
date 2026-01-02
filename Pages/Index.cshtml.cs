using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace TechInventory.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfigurationManager config;

    public IndexModel(ILogger<IndexModel> logger, ConfigurationManager config)
    {
        _logger = logger;
        this.config = config;
    }

    public void OnGet()
    {
        // Crie um teste simples (apagar e retirar config):
        using var connection = new SqlConnection(config.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
        try
        {
            connection.OpenAsync();
            TempData["Message"] = "Conectado com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Erro: {ex.Message}";
        }
        finally
        {
            connection.Close();
        }
    }
}
