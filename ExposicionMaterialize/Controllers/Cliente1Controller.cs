using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExposicionMaterialize.Controllers
{
    public class Cliente1Controller : ApiController
    {
        private BDReservaEntities db = new BDReservaEntities();

        [Route("api/Clientes", Name = "GetAllClients")]
        public HttpResponseMessage GetAllClients()
        {
            try
            {
                var est = db.Cliente.ToList();

                var cur = (from t in db.Cliente

                           select new
                           {
                               t.IdCliente,
                               t.Nombre,
                               t.Correo,
                               t.Telefono,
                               t.Usuario,
                               t.Contrasena



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

        [Route("api/Clientes/Para/", Name = "GetClientsId")]
        public HttpResponseMessage GetClientsId(string usuario)
        {
            try
            {
                var est = db.Cliente.ToList();

                var cur = (from t in db.Cliente where t.Usuario == usuario

                           select new
                           {
                               t.IdCliente,
                       
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
        [Route("api/Clientes", Name = "PostCliente")]
        public IHttpActionResult PostCliente(Cliente cliente)
        {
            var cur = (from t in db.Cliente where t.IdCliente==cliente.IdCliente

                       select new
                       {
                           t.IdCliente,
                           t.Nombre,
                           t.Correo,
                           t.Telefono,
                           t.Usuario,
                           t.Contrasena



                       }).ToList();

            if (!ModelState.IsValid) { 
                return BadRequest("Not a valid model");
            } else if(cliente.IdCliente ==null || cliente.Nombre==null || cliente.Correo==null || cliente.Telefono ==null || cliente.Usuario == null || cliente.Contrasena == null)
            {
                return BadRequest("Not a valid model");
            }
            else if (cur.Count>0)
            {
                return BadRequest("Not a valid model");
            }
            else { 

            db.Cliente.Add(new Cliente()
                {
                    IdCliente = cliente.IdCliente,
                    Nombre = cliente.Nombre,
                    Correo = cliente.Correo,
                    Telefono = cliente.Telefono,
                    Usuario= cliente.Usuario,
                    Contrasena= cliente.Contrasena

                });

                db.SaveChanges();
            

            return Ok();
            }
        }

        [Route("api/Clientes/Login", Name = "LoginCliente")]
        public IHttpActionResult LoginCliente(Login log)
        {


            var cur = (from t in db.Cliente
                       where t.Usuario == log.Usuario && t.Contrasena == log.Contrasena

                       select new
                       {

                           t.Usuario,
                           t.Contrasena

                       }).ToList();


            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            else if (log.Usuario == null || log.Contrasena == null)
            {
                return BadRequest("Not a valid model");
            }
            else if (cur.Count>0)
            {
                return Ok();
            }
            else
            {

                return BadRequest();
            }
        }

    }
}
