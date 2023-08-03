using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelos.Modelos;
using Negocio_SistemaAdquisiciones;
using Newtonsoft.Json;

namespace Microservicios_SistemaDeAdquisiciones.Controllers
{
    [Produces("application/json")]
    [Route("JuntaAclaracion")]
    [EnableCors("CorsPolicy")]
    public class juntaclaracionesController : ControllerBase
    {
        private tbl_juntaclaraciones_negocio Negocio = new tbl_juntaclaraciones_negocio();

        [HttpPost("Add_Obs")]
        public IActionResult Add_Obs([FromBody] tbl_solicitud_observador _tbl_solicitud_observador)
        {
            List<Crudresponse> Query = Negocio.Add_Obs(_tbl_solicitud_observador).Response;
            return Ok(Query);
        }
        [HttpPost("Add_Junta")]
        public IActionResult Add_Junta([FromBody] tbl_junta_aclaraciones _tbl_junta_aclaraciones)
        {
            List<Crudresponse> Query = Negocio.Add_Junta(_tbl_junta_aclaraciones).Response;
            return Ok(Query);
        }
        [HttpGet("Get_Obs/{id_sol}/{tipo_acta}/{prog}")]
        public IActionResult Get_Obs(string id_sol, string tipo_acta, string prog)
        {
            List<tbl_solicitud_observador_list> Query = Negocio.Get_Obs(id_sol, tipo_acta, prog).Response;
            return Ok(Query);
        }
        [HttpGet("Get_Juntas/{id_sol}")]
        public IActionResult Get_Juntas(string id_sol)
        {
            List<tbl_junta_aclaraciones_list> Query = Negocio.Get_Juntas(id_sol).Response;
            return Ok(Query);
        }
        [HttpDelete("Delete_Obs/{id_sol_obs}")]
        public IActionResult Delete_Obs(string id_sol_obs)
        {
            List<Crudresponse> Query = Negocio.Delete_Obs(id_sol_obs).Response;
            return Ok(Query);
        }
        [HttpDelete("Delete_Junta/{id_junta_acl}")]
        public IActionResult Delete_Junta(string id_junta_acl)
        {
            List<Crudresponse> Query = Negocio.Delete_Junta(id_junta_acl).Response;
            return Ok(Query);
        }
    }
}