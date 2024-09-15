using Microsoft.AspNetCore.Mvc;
using Tastys.BLL;
using Tastys.Domain;

namespace Tastys.Api.Controllers;

/// <summary>
/// Endpoints para obtener categorías y algunas recetas de cada una.
/// </summary>
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

    [HttpGet("all")]
    public IActionResult All([FromQuery] int page=0,[FromQuery] int pageSize = 100)
    {
        try
        {
            List<Categoria> categorias = _categoriaService.GetCategorias(pageSize,page);

            return Ok(categorias);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al traer categorias desde DB");
            return StatusCode(500);
        }
    }
    
    /// <summary>
    /// Obtener una lista de todas las categorías disponibles, incluyendo algunas recetas de cada una.
    /// </summary>
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

    /// <summary>
    /// Obtener el detalle de una categoría, incluyendo una lista de las recetas que contiene.
    /// Esta lista de recetas se puede ordenar y filtrar.
    /// </summary>
    /// <param name="id">El id de la categoría.</param>
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
