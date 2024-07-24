using esta1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast.Selectors;

namespace esta1.Controllers
{
    public class UsuariosController : Controller
    {
        //Llamamos la cadena de conexion que esta plasmada en web config, pueden existir mas de 1 con diferentes nombres
        private string cn = ConfigurationManager.ConnectionStrings["bdpr1"].ConnectionString;

        public ActionResult Index()
        {
            //El nombre de la plantilla debe de ser el mismo que el del metodo

            //Creamos una lista con el viewModel de la entidad para llenarla y luego pasarla a la vista
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();

            //Creamos un objeto conexion
            using (SqlConnection con = new SqlConnection(cn))
            {
                //Definimos la QUERY a ejecutar
                using (SqlCommand cm = new SqlCommand("SELECT * FROM vw_usuarios_rol",con))
                {
                    //Abrimos la conexion
                    con.Open();

                    //Leemos los datos de la tabla
                    using (SqlDataReader reader = cm.ExecuteReader())
                    {
                        //Mientras esta leyendo los datos los agregamos a la lista
                        while (reader.Read())
                        {
                            usuarios.Add(new UsuarioViewModel
                            {
                                nombreUsuario= reader.GetString(reader.GetOrdinal("identificador")),
                                rol= reader.GetString(reader.GetOrdinal("rol Asignado")),
                                
                            });
                        }
                    } 
                }
            }
            //Retornamos la vista con la lista para ser iterada en ella
                return View(usuarios);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(usuarios usu)
        {
            if (ModelState.IsValid)
            {
                /*if (duiRep())
                {//

                    ModelState.AddModelError("DUI", "Este DUI ya está registrado");
                    return View(usu);
                }*/

                using (SqlConnection conn = new SqlConnection(cn))
                {
                    //creamos la conexion, llamamos el procedimiento almacenado o en su defecto, creamos la QUERY paara guardar
                    string procedure = "exec p_create_usuario2 @nombre";


                    using (SqlCommand cmd = new SqlCommand(procedure, conn))
                    {//ejecutamos el comando, pasandole los elementos necesarios
                        cmd.Parameters.AddWithValue("@nombre", usu.nombres);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
                //En caso de todo ser correcto, retornamos al index
                return RedirectToAction("Index");
            }

            //En caso de que no, retornamos un objeto Usuario con los requierds malos
            return View(usu);
            
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private bool duiRep()
        {
            /* using (SqlConnection conn = new SqlConnection(connectionString))
             {
                 string query = "SELECT COUNT(*) FROM t_reclamos WHERE DUI = @DUI";
                 using (SqlCommand cmd = new SqlCommand(query, conn))
                 {
                     cmd.Parameters.AddWithValue("@DUI", dui);
                     conn.Open();
                     int count = (int)cmd.ExecuteScalar(); OJO
                     return count > 0;
                 }
             }*/
            return false;
        }
    }
}
