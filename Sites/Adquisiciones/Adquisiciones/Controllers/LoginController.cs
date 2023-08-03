using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeAdquisiciones.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SistemaDeAdquisiciones.Controllers
{
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            string Usuario = HttpContext.Session.GetString("IdUsuario");
            if (Usuario != null)
            {
                return RedirectToAction("Index", "Contrato");
            }

            return View();
        }

        [HttpGet]
        public ActionResult ObtenerLogin()
        {
            //var modelo = new AccesoSistemaVM() { Usuario = string.Empty };

            return PartialView("_login");
        }
        [HttpGet]
        public ActionResult logOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        //[HttpPost]
        //public ActionResult Validar(AccesoSistemaVM acceso)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Obtener usuario por medio del correo
        //        var usuario = NLogin.ObtenerUsuario(acceso.Usuario);

        //        //Si no existe el usuario
        //        if (usuario == null)
        //        {
        //            return Json(new { error = true, msg = "El usuario no existe." });
        //        }

        //        //Validar contraseña proporcionada
        //        bool tieneAcceso = NLogin.VerificarPassword(acceso.Contrasena, usuario.PASSWORD, usuario.SALTO);

        //        if (!tieneAcceso)
        //        {
        //            return Json(new { error = true, msg = "La contraseña es incorrecta." });
        //        }

        //        InicializarSesiones(usuario, acceso);

        //        return Json(new { error = false, url = Url.Action("Index", "Contrato") });
        //    }

        //    return Json(new { error = true, msg = "El correo no tiene formato válido." });
        //}

        [HttpPost]
        public IActionResult InicializarSesiones(string strUsuario)
        {
            SesionUsuario sesionUsuario = JsonConvert.DeserializeObject<SesionUsuario>(strUsuario);

            HttpContext.Session.SetString("IdUsuario", sesionUsuario.ID_USUARIO);
            HttpContext.Session.SetString("NombreUsuario", sesionUsuario.NOMBRE_USUARIO);
            HttpContext.Session.SetString("Correo", sesionUsuario.CORREO);
            HttpContext.Session.SetString("Password", sesionUsuario.PASSWORD);
            HttpContext.Session.SetString("IdInstancia", sesionUsuario.ID_INSTANCIA);
            HttpContext.Session.SetString("EsSuperUsuario", sesionUsuario.ES_SUPER_USUARIO.ToString());
            HttpContext.Session.SetString("IdDependencia", sesionUsuario.ID_DEPENDENCIA);
            HttpContext.Session.SetString("IdRol", sesionUsuario.ID_ROL);
            HttpContext.Session.SetString("IdRolUsuario", sesionUsuario.ID_ROL_USUARIO);
            HttpContext.Session.SetString("FaseAdquisiciones", "PASIG");
            //HttpContext.Session.SetString("IdRol", "820538fc-37e8-11ea-82d7-00155d1b3502");

            //List<UserDepRol> user = Log.SessionDep(usuario.ID_USUARIO_PK);
            //foreach (UserDepRol rolDep in user)
            //{
            //    Session["IdDependencia"] = rolDep.IdDependencia;
            //    Session["Rol"] = rolDep.Rol;
            //}

            return Json(HttpContext.Session.GetString("IdUsuario"));
        }
    }
}