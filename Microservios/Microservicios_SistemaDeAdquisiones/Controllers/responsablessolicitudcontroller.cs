﻿using System;
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
using Negocio_SistemaAdquisiciones;
using Newtonsoft.Json;


namespace Microservicios_SistemaDeAdquisiciones.Controllers
{

    [Produces("application/json")]
    [Route("responsablessolicitud")]
    [EnableCors("CorsPolicy")]
    public class ResponsablesSolicitudController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public ResponsablesSolicitudController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Dropdown/{sigla_rol}/{instancia}")]
        public IActionResult GetDrop(String sigla_rol, String instancia)
        {
            responsablessolicitud_negocio Negocio = new responsablessolicitud_negocio();
            List<DropDownList> Query = Negocio.FillDrop(sigla_rol, instancia).Response;
            return Ok(Query);
        }
        #endregion
    }
}
