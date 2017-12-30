using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ventas.Models;

namespace Ventas.Controllers
{
    public class HomeController : Controller
    {
        VENTASEntities cnx;
        public HomeController()
        {
            cnx = new VENTASEntities();
        }
        public ActionResult Formulario()
        {
            return View();
        }
        public ActionResult Listado()
        {
            return View("Listado", ListadoProductos());
        }
        private List<Ventas.Models.PRODUCTO> ListadoProductos()
        {
            cnx.Database.Connection.Open();
            List<Ventas.Models.PRODUCTO> produc = cnx.PRODUCTO.ToList();

            cnx.Database.Connection.Close();

            return produc;
        }
        public ActionResult Guardar(int codigobarra, string nombre, string familia, int precio, int descuentotope, string descripcion)
        {
            PRODUCTO pro = new PRODUCTO()
            {
                CodigoBarra = codigobarra,
                Nombre = nombre,
                Familia = familia,
                Precio = precio,
                DescuentoTope = descuentotope,
                Descripcion = descripcion
            };

            cnx.PRODUCTO.Add(pro);
            cnx.SaveChanges();
            return View("Listado", ListadoProductos());
        }
        public ActionResult Ficha(int codigobarra)
        {

            return View(BuscarProducto(codigobarra));
        }
        public PRODUCTO BuscarProducto(int codigobarra)
        {
            PRODUCTO nueva = new PRODUCTO();
            foreach (PRODUCTO pro in cnx.PRODUCTO.ToList())
            {
                if (pro.CodigoBarra.Equals(codigobarra))
                {
                    nueva = pro;
                }
            }
            return nueva;
        }
        public ActionResult Ver(int codigobarra)
        {
            return View("Ficha", null);
        }
    }
}