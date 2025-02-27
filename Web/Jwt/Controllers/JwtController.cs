using Core.Dtos.Orders;
using Core.Dtos.Responses;
using Core.Types.ConfigurationTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Orders.Controllers;

namespace Web.Jwt.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [ProducesResponseType(typeof(JwtTokenDto), 200)]  // OK response
    [Route("api/[controller]")]
    public class JwtController(ILogger<OrderController> logger, IMediator mediator, JwtOptions jwtOptions) : ControllerBase
    {
        [HttpGet("generate-token")]
        public IActionResult GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Sub, "test-user"),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: credentials
            );
            logger.LogInformation($"Token has been genereated, and it's beeing sent back.");
            return Ok(new JwtTokenDto(new JwtSecurityTokenHandler().WriteToken(token)));
        }

    }
}
