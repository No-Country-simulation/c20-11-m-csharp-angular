using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Tastys.API.Middlewares;
using Tastys.BLL;
using Tastys.Domain;

namespace Tastys.API.Controllers;

/// <summary>
/// Endpoints para obtener recetas.
/// </summary>
[Route("/api/receta")]
[ApiController]
public class RecetaController : ControllerBase
{
    private readonly ILogger<RecetaController> _logger;
    private readonly IRecetaService _recetaService;

    public RecetaController(ILogger<RecetaController> logger, IRecetaService recetaService)
    {
        _logger = logger;
        _recetaService = recetaService;
    }

    /// <summary>
    /// Obtener recetas paginadas y ordenadas.
    /// </summary>
    /// <param name="page">El número de página (depende de pageSize, la primera es 0).</param>
    /// <param name="pageSize">La cantidad de recetas por página.</param>
    /// <param name="sort_by">"fav": Ordenar por cantidad de reviews. "createDate": Ordenar por fecha.</param>
    [HttpGet("order")]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType<List<RecetaDto>>(200)]
    public async Task<ActionResult<List<RecetaDto>>> GetOrderRecetas([FromQuery] int page = 0, [FromQuery] int pageSize = 10, [FromQuery] QueryOrdersRecetas sort_by = QueryOrdersRecetas.Fav)
    {
        List<RecetaDto> recetas = await _recetaService.GetOrderRecetas(page, pageSize, sort_by);

        return Ok(recetas);
    }

    /// <summary>
    /// Obtener una lista de todas las recetas, con la posibilidad de filtrarlas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType<List<RecetaDto>>(200)]
    public async Task<ActionResult<List<RecetaDto>>> GetAllRecetas([FromQuery] RecetasQuery queryParemeters)
    {
        List<RecetaDto> recetas = await _recetaService.GetAll(queryParemeters);

        return Ok(recetas);
    }

    /// <summary>
    /// Obtener una recetas según su id.
    /// </summary>
    [HttpGet(":id")]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType<RecetaDto>(200)]
    public async Task<ActionResult<RecetaDto>> GetRecetaByID(int ID)
    {
        RecetaDto receta = await _recetaService.GetByID(ID);

        return Ok(receta);
    }

    /// <summary>
    /// Crear una receta nueva.
    /// </summary>
    [HttpPost]
    [CheckToken]
    [CheckPermissions("user:user")]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType<Receta>(200)]
    [SwaggerRequestExample(typeof(NewRecetaDTO), typeof(RecetaRequestExample))]
    public async Task<ActionResult<Receta>> CreateReceta([FromBody] NewRecetaDTO recetaData)
    {
        try
        {
            if (HttpContext.Items["userdata"] is not UserDataToken userData)
            {
                return BadRequest("No se encontró información del usuario.");
            }
            Receta postReceta = await _recetaService.Create(
                new Receta
                {
                    Nombre = recetaData.receta.nombre,
                    ImageUrl = recetaData.receta.imageUrl,
                    Descripcion = recetaData.receta.descripcion
                }
                , recetaData.list_c, userData.authId);

            //te retorna el codigo 201 -created- cuando se crea
            //y ademas te dice en el header, che, encontras esta receta en la ruta /recetas/:id
            return CreatedAtAction(nameof(CreateReceta), new { id = postReceta.RecetaID }, postReceta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear la receta en DB");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Crear una receta nueva y asignarla a un usuario arbitrario.
    /// </summary>
    [HttpPost("admin")]
    [CheckToken]
    [CheckPermissions("user:admin")]
    [SwaggerRequestExample(typeof(NewRecetaDTO), typeof(RecetaRequestExample))]
    public async Task<ActionResult<Receta>> CreateRecetaAdmin([FromBody] NewRecetaDTO recetaData)
    {
        try
        {
            Receta postReceta = await _recetaService.Create(
                new Receta
                {
                    Nombre = recetaData.receta.nombre,
                    ImageUrl = recetaData.receta.imageUrl,
                    Descripcion = recetaData.receta.descripcion
                }, recetaData.list_c, recetaData.user_id);

            //te retorna el codigo 201 -created- cuando se crea
            //y ademas te dice en el header, che, encontras esta receta en la ruta /recetas/:id
            return CreatedAtAction(nameof(CreateReceta), new { id = postReceta.RecetaID }, postReceta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear la receta en DB");
            return StatusCode(404, ex);
        }
    }

    /// <summary>
    /// Eliminar una receta según su id.
    /// </summary>
    /// <param name="ID">El id de la receta.</param>
    [HttpDelete("{ID}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ProducesResponseType(200)]
    public async Task<ActionResult> DeleteReceta(int ID)
    {
        bool deleted = await _recetaService.DeleteById(ID);

        return Ok();
    }
}
