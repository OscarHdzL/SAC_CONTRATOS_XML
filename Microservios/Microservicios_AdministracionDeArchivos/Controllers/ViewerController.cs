using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Area;
using NegocioAdministracionContratos;
using Newtonsoft.Json;
using Utilidades.Log4Net;

namespace Microservicios_AdministracionArchivos.Controllers
{

    [ApiController]
    [Route("Files")]
    [EnableCors("AllowOrigin")]
    public class ViewerController : ControllerBase
    {

        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ViewerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }

        [HttpGet("Viewer/{URI}")]
        public IActionResult Viewer(String URI)
        {
            try
            {
                String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();

                String B64 = NAN.Base64Decode(URI);
                String MD5Decode = NAN.Decrypt(B64);
                List<ResponseFileManager> LST = NAN.GetBuffer(Guid.Parse(MD5Decode.Split('|')[1])).Response;
                if (LST.Count <= 0)
                {
                    return NotFound("No se encontró el uri "+ URI);
                }
                ResponseFileManagerInfo info = NAN.GetDocInfo(Guid.Parse(MD5Decode.Split('|')[1])).Response[0];
                String File_encrypt = NAN.Base64Encode(NAN.Encrypt(LST[0].RI)) + ".dat";


      
                String file_tmp = Path.Combine(RutaFisica + File_encrypt);
                if (System.IO.File.Exists(file_tmp))
                {
                    if (info.ext.ToLower().Contains("pdf") || info.ext.ToLower().Contains("jpg") || info.ext.ToLower().Contains("png") || info.ext.ToLower().Contains("bmp") || info.ext.ToLower().Contains("txt"))
                    {
                        return Ok(System.IO.File.OpenRead(file_tmp));
                    }
                    else
                    {
                        // Stream stream = System.IO.File.Open(file_tmp, FileMode.Open);

                        DateTime dt = DateTime.Now;
                        String tempfile = RutaFisica + info.Name + "_" + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + "." + info.ext;                  
                        Stream streamII = System.IO.File.Open(file_tmp, FileMode.Open);      
                        String type_ = GetContentType(tempfile);
                        //streamII.Close();                       
                        FileStreamResult FSR = File(streamII, type_, info.Name + "." + info.ext);       
                        return Ok(FSR);
                    }
                }
                else{
                    return BadRequest("No existe la ruta  "+file_tmp);
                }
            }
            catch (System.IO.IOException e)
            {
                _logger.LogError("Get", e);
                return BadRequest(e);
            }

        }
        private string GetContentType(string path)
        {
            
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        [HttpGet("Viewer/{URI}/{Version}")]
        public IActionResult Viewer(String URI, int Version)
        {
            try
            {
                String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();

                String B64 = NAN.Base64Decode(URI);
                String MD5Decode = NAN.Decrypt(B64);
                List<ResponseFileManager> LST = new List<ResponseFileManager>();
                LST = NAN.GetBufferVersion(Guid.Parse(MD5Decode.Split('|')[1]),Version).Response;
                ResponseFileManagerInfo info = NAN.GetDocInfo(Guid.Parse(MD5Decode.Split('|')[1])).Response[0];
                String File_encrypt = NAN.Base64Encode(NAN.Encrypt(LST[0].RI)) + ".dat";



                String file_tmp = RutaFisica + File_encrypt;
                if (System.IO.File.Exists(file_tmp))
                {
                    if (info.ext.Contains("pdf") || info.ext.Contains("jpg") || info.ext.Contains("png") || info.ext.Contains("bmp"))
                    {
                        return Ok(System.IO.File.OpenRead(file_tmp));
                    }
                    else
                    {
                        Stream stream = System.IO.File.Open(file_tmp, FileMode.Open);
                        return File(stream, "application/octet-stream", info.Name);
                    }
                }
                return BadRequest();
            }
            catch (System.IO.IOException e)
            {
                _logger.LogError("Get", e);
                return NotFound();
            }

        }
     

        

    }
}