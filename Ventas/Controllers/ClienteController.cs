using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ventas.Models;

namespace Ventas.Controllers
{
    public class ClienteController : Controller
    {
        VENTASEntities cnx; 
        public ClienteController()
        {
            cnx = new VENTASEntities();
        }
        public ActionResult Cliente()
        {
            return View();
        }
        public ActionResult Guardar(int rut, string nombre, string apellido, string direccion, string tipocliente)
        {

            Ventas.Models.CLIENTE cliente = new Ventas.Models.CLIENTE
            {

                Rut = rut,
                Nombre = nombre,
                Apellido = apellido,
                Direccion = direccion,
                Tipo = tipocliente
            };

            cnx.CLIENTE.Add(cliente);
            cnx.SaveChanges();


            return View("Listado", ListadoClientes());
        }
        public ActionResult Ficha(int rut)
        {
            return View(cnx.CLIENTE.Where(x => x.Rut == rut).First());
        }
        public ActionResult Listado()
        {
            return View("Listado", ListadoClientes());
        }
        private List<Ventas.Models.CLIENTE> ListadoClientes()
        {
            cnx.Database.Connection.Open();
            List<Ventas.Models.CLIENTE> cli = cnx.CLIENTE.ToList();
            cnx.Database.Connection.Close();
            return cli;
        }
        public ActionResult Eliminar(int rut)
        {
            cnx.CLIENTE.Remove(cnx.CLIENTE.Where(x => x.Rut.Equals(rut)).First());
            cnx.SaveChanges();

            return View("Listado", cnx.CLIENTE.ToList());

        }
        
    }
}

