using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.Response;
using WSVenta.Models.Request;

namespace WSVenta.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class ClienteController: ControllerBase {
		[HttpGet]
		public IActionResult Get() {
			var oRespuesta = new Respuesta();
			try {
				using( var db = new VentaRealContext() ) {
					var lst = db.Cliente.OrderByDescending( d => d.Id ).ToList();
					oRespuesta.Exito = 1;
					oRespuesta.Data = lst;
				} 
			}
			catch( Exception ex ) {

				oRespuesta.Mensaje = ex.Message;
			}
			return Ok( oRespuesta );
		}
		[HttpPost]
		public IActionResult Create( ClienteRequest oModel ) {
			var oRespuesta = new Respuesta();
			try {
				using( var db = new VentaRealContext() ) {
					var oCliente = new Cliente() {
						Nombre = oModel.Nombre
					};
					db.Cliente.Add( oCliente );
					db.SaveChanges();
					oRespuesta.Exito = 1;

				}
			}
			catch( Exception ex ) {
				oRespuesta.Mensaje = ex.Message;
			}
			return Ok( oRespuesta );

		}
		[HttpPut]
		public IActionResult Update( ClienteRequest oModel ) {
			var oRespuesta = new Respuesta();
			try {
				using( var db = new VentaRealContext() ) {
					var oCliente = db.Cliente.Find( oModel.Id );
					oCliente.Nombre = oModel.Nombre;
					db.Entry( oCliente ).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					db.SaveChanges();
					oRespuesta.Exito = 1;

				}
			}
			catch( Exception ex ) {
				oRespuesta.Mensaje = ex.Message;
			}
			return Ok( oRespuesta );

		}
		[HttpDelete("{Id}")]
		public IActionResult Delete( int Id ) {
			var oRespuesta = new Respuesta();
			try {
				using( var db = new VentaRealContext() ) {
					var oCliente = db.Cliente.Find( Id );
					db.Remove( oCliente );
					db.SaveChanges();
					oRespuesta.Exito = 1;
				}
			}
			catch( Exception ex ) {
				oRespuesta.Mensaje = ex.Message;
			}
			return Ok( oRespuesta );

		}
	}
}
