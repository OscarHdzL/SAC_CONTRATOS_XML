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
    [Route("areas")]
    [EnableCors("CorsPolicy")]
    public class AreaController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public AreaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Dropdown/{Dependencia}")]
        public IActionResult GetDrop(String Dependencia)
        {
            tbl_areas_negocio Negocio = new tbl_areas_negocio();
            List<DropDownList> Query = Negocio.FillDrop(Dependencia).Response;
            return Ok(Query);
        }
        #endregion
    }
}
