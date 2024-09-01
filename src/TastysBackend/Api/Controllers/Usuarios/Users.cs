using Microsoft.AspNetCore.Mvc;
using Tastys.BLL;
using Tastys.Domain;
using Tastys.API.Middlewares;

namespace Tastys.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController:ControllerBase
{
    private readonly UserServices _userService;
    public UserController(UserServices userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [SetToken]
    [CheckToken]
    public ActionResult PostUsers([FromBody]Usuario user)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto = _userService.PostUser(user);

            return Ok(usuarioPublicDto);
            
        }
        catch (System.Exception)
        {

            throw;
        }
    }
    [HttpGet("all")]
    public ActionResult GetUser()
    {
        try
        {

            List<Usuario> usuarioPublicDto= _userService.GetAllUsers();

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception)
        {
            throw;
        }
    }
    [HttpGet("email")]
    [CheckToken]
    public ActionResult GetUser([FromQuery]string email)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto= _userService.GetUserByEmail(email);

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpGet]
    [SetToken]
    [CheckToken]
    public ActionResult GetUserAuth()
    {
        try
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null)
            {
                string token = authorizationHeader.Substring("Bearer ".Length).Trim();
                UsuarioPublicDto usuarioPublicDto= _userService.GetUserAuth0(token);

                return Ok(usuarioPublicDto);
            }else
            {
                return BadRequest("No se proporciono el token para obtener el usuairo");
            }


        }
        catch (System.Exception)
        {
            throw;
        }
    }
    [HttpDelete]
    public ActionResult DeleteUser([FromQuery] string Auth0Id)
    {
        try
        {

            UsuarioPublicDto usuarioPublicDto= _userService.AuthDeleteUser(Auth0Id);

            return Ok(usuarioPublicDto);

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    
    [HttpPut]
    public ActionResult PutUser([FromBody]Usuario user)
    {
        try
        {
            UsuarioPublicDto usuarioPublicDto= _userService.PutUser(user);

            return Ok(usuarioPublicDto);
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}