using MantenimientoEscolarApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MantenimientoEscolarApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin login)
        {
            var result = await _authService.AutenticarAsync(login.Correo);
            if (result == null)
                return Unauthorized("Correo no encontrado.");

            return Ok(result);
        }
    }
}
