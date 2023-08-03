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
using Modelos.Response;
using Negocio_SistemaAdquisiciones;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microservicios_SistemaDeAdquisiciones.Controllers
{
    [Produces("application/json")]
    [Route("Fallo")]
    [EnableCors("CorsPolicy")]
    public class FalloController : ControllerBase
    {
        private tbl_fallo_negocio Negocio = new tbl_fallo_negocio();

        [HttpGet("Proposiciones/Get/{id_sol}")]
        public IActionResult Proposiciones_Get(Guid id_sol)
        {            
            List<tbl_fallo> LST = Negocio.Proposiciones_Get(id_sol.ToString()).Response;
            return Ok(LST);
        }

        //[HttpPost]
        //[Route("OperaContrato/{id_sol}")]
        //public IActionResult GoFallo(Guid id_sol)
        //{            
        //    List<tbl_fallo> LST = Negocio.Proposiciones_Get(id_sol.ToString()).Response;

        //    List<sp_config_contrato> contrato = Negocio.Get_New_Con(id_sol.ToString()).Response; // return

        //    List<tbl_firmantes> rescon = Negocio.Get_Firm_Sol(id_sol.ToString()).Response;

        //    List<tbl_Responsable> rescon_ = Negocio.Get_Res_Sol(id_sol.ToString()).Response;

        //    //Lista proveedores
        //    List<string> listaprov = new List<string>(); // return
        //    foreach (tbl_fallo prov_ in LST)
        //    {
        //        if (prov_.ganador == true) {
        //           tbl_Proveedores lista_p = Negocio.Get_lista_prov(prov_.rfc.ToString()).Response;
        //            listaprov.Add(lista_p.id_proveedor.ToString());
        //        }       
        //    }
        //    // Lista firmantes y responsable
        //    Responsable_contrato Resp_con = new Responsable_contrato(); // return    
        //    List<string> lista_firms = new List<string>();

        //    foreach (tbl_firmantes firm_ in rescon)
        //    {
        //        lista_firms.Add(firm_.firmantes);
        //    }
        //    Resp_con.firmantes = lista_firms;
        //    Resp_con.Responsable = rescon_[0].Responsable;

        //    Crudresponse Query = Negocio.Fase_0_ADD(contrato[0]).Response[0];
        //    Resp_con.Contrato = Query.msg;
        //    Negocio.responsables_contrato(Resp_con);
        //    Negocio.add_contrato_proveedor(listaprov, Guid.Parse(Query.msg));
        //    return Ok(Query);
        //}

        [HttpPost]
        [Route("OperaContrato/{id_sol}")]
        public IActionResult GenerarFallo(Guid id_sol)
        //public IActionResult GenerarFallo(Guid id_sol, [FromForm]String LstPres)
        {
            var LstPres = "";
            Crudresponse Query = Negocio.GenerarFallo(id_sol, LstPres).Response;
            return Ok(Query);
        }
    }
}