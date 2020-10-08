using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Services;

namespace WSVenta.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class UserController: ControllerBase {

		private IUserService _userService;

		public UserController(IUserService userService) {
			this._userService = userService;
		}
		[HttpPost("login")]
		public IActionResult Autentificar([FromBody] AuthRequest model) {
			var respuesta = new Respuesta();

			var userResponse = _userService.Auth( model );

			if(userResponse == null ) {
				respuesta.Mensaje = "Datos incorrectos.";

				return BadRequest(respuesta);

			}
			respuesta.Exito = 1;
			respuesta.Data = userResponse;
			return Ok( respuesta );
		}
	}

}
