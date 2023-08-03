using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelos.Modelos;
using Modelos.Modelos.ServidoresPublicos;
using Newtonsoft.Json;
using Solucion_Negocio;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("PlanEntrega_")]
    [EnableCors("CorsPolicy")]
    public class plan_entrega_ejecucionController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public plan_entrega_ejecucionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones

   

        #endregion




    }
}
