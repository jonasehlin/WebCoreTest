using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCoreTest.Authorization;

namespace WebCoreTest.Controllers
{
	[Route("api/[controller]")]
	public class TokenController : Controller
	{
		private IConfiguration Configuration { get; }

		public TokenController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		[HttpPost("")]
		[AllowAnonymous]
		public IActionResult Login([FromBody] AuthRequest authUserRequest)
		{
			// TODO: Lookup user
			string user = "Jonas Ehlin";

			if (user != null)
			{
				bool checkPwd = authUserRequest.Password == "qwerty";
				if (checkPwd)
				{
					var claims = new[]
					{
						new Claim(JwtRegisteredClaimNames.Sub, "jonas"),
						new Claim(JwtRegisteredClaimNames.Jti, "1"),
					};

					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
					var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

					var token = new JwtSecurityToken(Configuration["Tokens:Issuer"],
						Configuration["Tokens:Issuer"],
						claims,
						expires: DateTime.Now.AddMinutes(30),
						signingCredentials: creds);

					return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
				}
			}

			return BadRequest("Could not create token");
		}
	}
}