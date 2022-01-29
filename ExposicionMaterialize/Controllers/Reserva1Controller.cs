using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExposicionMaterialize.Controllers
{
    public class Reserva1Controller : ApiController
    {

        private BDReservaEntities db = new BDReservaEntities();

        [Route("api/Reservas", Name = "GetAllReservas")]
        public HttpResponseMessage GetAllReservas()
        {
            try
            {
                var est = db.Reserva.ToList();

                var cur = (from t in db.Reserva

                           select new
                           {
                               t.IdReserva,
                               t.IdCliente,
                               t.FechaIngreso,
                               t.FechaSalida,
                               t.Destino,
                               t.NumeroPersonas



                           });

                if (est != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, cur);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resultado no encontrado");


            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }




        }



        // POST: api/Carreras
        [Route("api/Reservas", Name = "PostReserva")]
        public IHttpActionResult PostReserva(Reserva reserva)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            else if (reserva.IdCliente == null || reserva.FechaIngreso == null || reserva.FechaSalida == null || reserva.Destino == null || reserva.NumeroPersonas == 0 )
            {
                return BadRequest("Not a valid model");
            }
            else
            {

                db.Reserva.Add(new Reserva()
                {
                    IdCliente = reserva.IdCliente,
                    FechaIngreso = reserva.FechaIngreso,
                    FechaSalida = reserva.FechaSalida,
                    Destino = reserva.Destino,
                    NumeroPersonas = reserva.NumeroPersonas,
                    

                });

                db.SaveChanges();


                return Ok();
            }
        }
    }
}
