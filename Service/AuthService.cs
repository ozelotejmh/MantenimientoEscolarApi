using MantenimientoEscolarApi.Data;
using MantenimientoEscolarApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface IAuthService
{
    Task<UsuarioTokenResponse?> AutenticarAsync(string correo);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<UsuarioTokenResponse?> AutenticarAsync(string correo)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        if (usuario == null) return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.TipoUsuario)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
        );

        return new UsuarioTokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Nombre = usuario.Nombre,
            TipoUsuario = usuario.TipoUsuario
        };
    }
}
