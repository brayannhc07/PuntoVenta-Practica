using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVenta.Models.Response {
	public class Respuesta {
		public Respuesta() {
			Exito = 0;
			Mensaje = "";
		}
		public int Exito { get; set; }
		public string Mensaje { get; set; }
		public object Data { get; set; }
	}
}
