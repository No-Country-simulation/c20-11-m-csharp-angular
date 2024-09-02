using Microsoft.AspNetCore.Mvc;
using Tastys.BLL;

namespace Tastys.Api.Controllers;

[Route("/api/categorias")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ILogger<CategoriaController> _logger;
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ILogger<CategoriaController> logger, ICategoriaService categoriaService)
    {
        _logger = logger;
        _categoriaService = categoriaService;
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType<CategoriaConRecetasDto[]>(200)]
    public async Task<ActionResult<CategoriaConRecetasDto[]>> Get([FromQuery] CategoriasQuery queryParameters)
    {
        // Los queryParameters se validan automáticamente
        // de acuerdo a las anotaciones en CategoriasQuery

        try
        {
            var recetas = await _categoriaService.GetCategorias(queryParameters);

            return Ok(recetas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al traer categorias desde DB");
            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType<CategoriaConRecetasDto>(200)]
    public async Task<ActionResult<CategoriaConRecetasDto>> GetCategoriaByID([FromRoute] int id, [FromQuery] CategoriaByIdQuery queryParameters)
    {
        try
        {
            var receta = await _categoriaService.GetCategoriaById(id, queryParameters);

            if (receta == null)
            {
                return NotFound();
            }

            return Ok(receta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al traer la categoría {id}", id);
            return StatusCode(500);
        }
    }
}
