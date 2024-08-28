using Microsoft.AspNetCore.Mvc;
using Tastys.BLL;

namespace Tastys.Api.Controllers;

[ApiController]
[Route("receta")]
public class RecetaController : ControllerBase
{
    private readonly ILogger<RecetaController> _logger;
    private readonly RecetaService _recetaService;

    public RecetaController(ILogger<RecetaController> logger, RecetaService recetaService)
    {
        _logger = logger;
        _recetaService = recetaService;
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType<RecetaDto[]>(200)]
    public ActionResult<RecetaDto[]> Get([FromQuery] RecetasQuery queryParameters)
    {
        // Los queryParameters se validan automáticamente
        // de acuerdo a las anotaciones en RecetasQuery

        try
        {
            var recetas = _recetaService.GetAllRecetas(queryParameters);

            return Ok(recetas);
        }
        catch
        {
            return StatusCode(500);
        }
    }
}
