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
using Newtonsoft.Json;

using System.IO;
using Microsoft.AspNetCore.Http;
using Modelos.Interfaz;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{
    [Produces("application/json")]
    [Route("Files")]
    [EnableCors("CorsPolicy")]
    
    public class ControlController : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ControlController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Delete(String id)
        {
            NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
            String B64 = NAN.Base64Decode(id);
            String Normal = NAN.Decrypt(B64);
            List<ResponseFileManager> LST = NAN.remove(Normal).Response;
            return Ok(LST[0].RI);
        }


        [HttpPost("Upload")]
        public String Add(IFormFile file)
        {
            try
            {
                AdministracionArchivos FileManager_ = new AdministracionArchivos();
                Guid IDF = Guid.NewGuid();
                FileManager_.id = IDF;
                FileManager_.file_author = "NA";
                FileManager_.file_descripcion = "NA";
                FileManager_.file_name = file.FileName.Split('.')[0];
                int valor = file.FileName.Split('.').Length - 1;
                FileManager_.file_extension = file.FileName.Split('.')[valor];
                FileManager_.minutes = 0;
                FileManager_.versions_author = "NA";
                FileManager_.versions_description = "NA";
                FileManager_.versions_size = (file.Length / 1048576).ToString();
                FileManager_.versions_title = file.FileName;

                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                List<ResponseFileManager> LST = NAN.Get(FileManager_).Response;


                String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
                String ext = Path.GetExtension(file.FileName);
                String Name = Path.GetFileName(file.FileName);


                String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST[0].RI + "|" + IDF)) + ".dat";
                String path = Path.Combine(RutaFisica + Filename_end);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return NAN.Base64Encode(NAN.Encrypt(LST[0].RI));
            }
            catch (Exception ex)
            {
                _logger.LogError("Post", ex);
                return BadRequest(ex).ToString();
            }            
        }

        [HttpPost("Upload/{entrega}")]
        public String Add_Entrega(IFormFile file, string entrega)
        {
            try
            {
                AdministracionArchivos FileManager_ = new AdministracionArchivos();
                Guid IDF = Guid.NewGuid();
                FileManager_.id = IDF;
                FileManager_.file_author = "NA";
                FileManager_.file_descripcion = "NA";
                FileManager_.file_name = file.FileName.Split('.')[0];
                int valor = file.FileName.Split('.').Length - 1;
                FileManager_.file_extension = file.FileName.Split('.')[valor];
                FileManager_.minutes = 0;
                FileManager_.versions_author = "NA";
                FileManager_.versions_description = "NA";
                FileManager_.versions_size = (file.Length / 1048576).ToString();
                FileManager_.versions_title = file.FileName;

                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                List<ResponseFileManager> LST = NAN.GetNewEntrega(FileManager_, entrega).Response;


                String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
                String ext = Path.GetExtension(file.FileName);
                String Name = Path.GetFileName(file.FileName);


                String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST[0].RI + "|" + IDF)) + ".dat";
                String path = Path.Combine(RutaFisica + Filename_end);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                NAN.InsertTokenFile(NAN.Base64Encode(NAN.Encrypt(LST[0].RI)), FileManager_.id.ToString());
                return NAN.Base64Encode(NAN.Encrypt(LST[0].RI));
            }
            catch (Exception ex)
            {
                _logger.LogError("Post", ex);
                return BadRequest(ex).ToString();
            }
        }

        [HttpPost("Upload/{entrega}/{ubicacion}")]
        public String Add_Entrega_Ubicacion(IFormFile file, string entrega, string ubicacion)
        {



            AdministracionArchivos FileManager_ = new AdministracionArchivos();
            Guid IDF = Guid.NewGuid();
            FileManager_.id = IDF;
            FileManager_.file_author = "NA";
            FileManager_.file_descripcion = "NA";
            FileManager_.file_name = file.FileName.Split('.')[0];
            int valor = file.FileName.Split('.').Length - 1;
            FileManager_.file_extension = file.FileName.Split('.')[valor];
            FileManager_.minutes = 0;
            FileManager_.versions_author = "NA";
            FileManager_.versions_description = "NA";
            FileManager_.versions_size = (file.Length / 1048576).ToString();
            FileManager_.versions_title = file.FileName;

            NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
            List<ResponseFileManager> LST = NAN.GetNewUb(FileManager_, entrega, ubicacion).Response;


            String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
            String ext = Path.GetExtension(file.FileName);
            String Name = Path.GetFileName(file.FileName);


            String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST[0].RI + "|" + IDF)) + ".dat";
            String path = Path.Combine(RutaFisica + Filename_end);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            NAN.InsertTokenFile(NAN.Base64Encode(NAN.Encrypt(LST[0].RI)), FileManager_.id.ToString());
            return NAN.Base64Encode(NAN.Encrypt(LST[0].RI));
        }

        [HttpPost("Upload/{entrega}/{ubicacion}/{monitoreo}")]
        public String Add_Entrega_Monitoreo(IFormFile file, string entrega, string ubicacion, string monitoreo)
        {

            AdministracionArchivos FileManager_ = new AdministracionArchivos();
            Guid IDF = Guid.NewGuid();
            FileManager_.id = IDF;
            FileManager_.file_author = "NA";
            FileManager_.file_descripcion = "NA";
            FileManager_.file_name = file.FileName.Split('.')[0];
            int valor = file.FileName.Split('.').Length - 1;
            FileManager_.file_extension = file.FileName.Split('.')[valor];
            FileManager_.minutes = 0;
            FileManager_.versions_author = "NA";
            FileManager_.versions_description = "NA";
            FileManager_.versions_size = (file.Length / 1048576).ToString();
            FileManager_.versions_title = file.FileName;

            NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
            List<ResponseFileManager> LST = NAN.GetNewMonitoreo(FileManager_, entrega, ubicacion, monitoreo).Response;


            String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
            String ext = Path.GetExtension(file.FileName);
            String Name = Path.GetFileName(file.FileName);


            String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST[0].RI + "|" + IDF)) + ".dat";
            String path = Path.Combine(RutaFisica + Filename_end);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            NAN.InsertTokenFile(NAN.Base64Encode(NAN.Encrypt(LST[0].RI)), FileManager_.id.ToString());
            return NAN.Base64Encode(NAN.Encrypt(LST[0].RI));
        }

        [HttpPost("Upload/DocType/{idDT}/{idContrato}")]
        public ActionResult AddDocType(IFormFile file, String idDT, String idContrato)
        {
            try
            {

                AdministracionArchivos FileManager_ = new AdministracionArchivos();
                Guid IDF = Guid.NewGuid();
                FileManager_.id = IDF;
                FileManager_.file_author = "NA";
                FileManager_.file_descripcion = "NA";
                FileManager_.file_name = file.FileName.Split('.')[0];
                int valor = file.FileName.Split('.').Length - 1;
                FileManager_.file_extension = file.FileName.Split('.')[valor];
                FileManager_.minutes = 0;
                FileManager_.versions_author = "NA";
                FileManager_.versions_description = "NA";
                FileManager_.versions_size = (file.Length / 1048576).ToString();
                FileManager_.versions_title = file.FileName;

                FileManager_.file_tbl_tipo_documento_id = idDT;
                FileManager_.file_tbl_contrato_id = idContrato;

                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                //List<ResponseFileManager> LST = NAN.Get(FileManager_).Response;
                var LST = NAN.Get(FileManager_);
                if (LST.CurrentException != null) {
                    LST.CurrentException += "| Error al guardar la info del documento en bd";
                    return BadRequest(LST);
                }
                if (LST.Response.Count==0)
                {
                    LST.CurrentException = "| El sp para guardar info no regresó respuesta";
                    return BadRequest(LST);
                }
                try
                {
                    String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
                    String ext = Path.GetExtension(file.FileName);
                    String Name = Path.GetFileName(file.FileName);
                    String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST.Response[0].RI + "|" + IDF)) + ".dat";
                    String path = Path.Combine(RutaFisica + Filename_end);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(NAN.Base64Encode(NAN.Encrypt(LST.Response[0].RI)));
                }
                catch (Exception ex) {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPost("Upload/{id}")]
        public String Add_id(IFormFile file, String id)
        {
            NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();

            String Res64 = NAN.Base64Decode(id);
            String ResMd5 = NAN.Decrypt(Res64);

            String Resultado = NAN.Gettoken(ResMd5).Response[0].RI;



            AdministracionArchivos FileManager_ = new AdministracionArchivos();
            Guid IDF = Guid.Parse(Resultado);
            FileManager_.id = IDF;
            FileManager_.file_author = "NA";
            FileManager_.file_descripcion = "NA";
            FileManager_.file_name = file.FileName.Split('.')[0];
            int valor = file.FileName.Split('.').Length - 1;
            FileManager_.file_extension = file.FileName.Split('.')[valor];
            FileManager_.minutes = 0;
            FileManager_.versions_author = "NA";
            FileManager_.versions_description = "NA";
            FileManager_.versions_size = (file.Length / 1048576).ToString();
            FileManager_.versions_title = file.FileName;


            List<ResponseFileManager> LST = NAN.Get(FileManager_).Response;


            String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
            String ext = Path.GetExtension(file.FileName);
            String Name = Path.GetFileName(file.FileName);


            String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST[0].RI + "|" + IDF)) + ".dat";
            String path = Path.Combine(RutaFisica + Filename_end);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return NAN.Base64Encode(NAN.Encrypt(LST[0].RI));
        }


        [HttpPost("Upload/Details")]
        public String Add(IFormFile file,String Descripcion, String Autor, String Nombre)
        {
            AdministracionArchivos FileManager_ = new AdministracionArchivos();
            Guid IDF = Guid.NewGuid();
            FileManager_.id = IDF;
            FileManager_.file_author = Autor;
            FileManager_.file_descripcion = Descripcion;
            FileManager_.file_name = Nombre;
            int valor = file.FileName.Split('.').Length - 1;
            FileManager_.file_extension = file.FileName.Split('.')[valor];
            FileManager_.minutes = 0;
            FileManager_.versions_author = Autor;
            FileManager_.versions_description = Descripcion;
            FileManager_.versions_size = (file.Length / 1048576).ToString();
            FileManager_.versions_title = Nombre;

            NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
            List<ResponseFileManager> LST = NAN.Get(FileManager_).Response;


            String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
            String ext = Path.GetExtension(file.FileName);
            String Name = Path.GetFileName(file.FileName);


            String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST[0].RI + "|" + IDF)) + ".dat";
            String path = Path.Combine(RutaFisica + Filename_end);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return NAN.Base64Encode(NAN.Encrypt(LST[0].RI));
        }
        [HttpPost("Upload/Details/{token}")]
        public String Add(IFormFile file, String Descripcion, String Autor, String Nombre, String Token)
        {
            NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
            String Res64 = NAN.Base64Decode(Token);
            String ResMd5 = NAN.Decrypt(Res64);
            String Resultado = NAN.Gettoken(ResMd5).Response[0].RI;
            AdministracionArchivos FileManager_ = new AdministracionArchivos();
            Guid IDF = Guid.Parse(Resultado);
            FileManager_.id = IDF;
            FileManager_.file_author = Autor;
            FileManager_.file_descripcion = Descripcion;
            FileManager_.file_name = Nombre;
            int valor = file.FileName.Split('.').Length - 1;
            FileManager_.file_extension = file.FileName.Split('.')[valor];
            FileManager_.minutes = 0;
            FileManager_.versions_author = Autor;
            FileManager_.versions_description = Descripcion;
            FileManager_.versions_size = (file.Length / 1048576).ToString();
            FileManager_.versions_title = Nombre;

            List<ResponseFileManager> LST = NAN.Get(FileManager_).Response;


            String RutaFisica = _configuration.GetSection("RutaFisica").Value + @"\";
            String ext = Path.GetExtension(file.FileName);
            String Name = Path.GetFileName(file.FileName);


            String Filename_end = NAN.Base64Encode(NAN.Encrypt(LST[0].RI + "|" + IDF)) + ".dat";
            String path = Path.Combine(RutaFisica + Filename_end);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return NAN.Base64Encode(NAN.Encrypt(LST[0].RI));
        }
        

        [HttpGet("Viewer/Version/{Token}")]
        public IActionResult ViewerVersions(String Token)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                return Ok(NAN.documentary_information_GetListVersions(Token));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Viewer/Update/State")]
        public IActionResult ViewerUpdate([FromForm]String Token, [FromForm]String description_, [FromForm]int tipo)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                return Ok(NAN.documentary_information_update_state(Token, description_, tipo));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }

        [HttpGet("GeneraUrl/{id}/{exp}")]
        public IActionResult Index(String id, int exp)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                String B64 = NAN.Base64Decode(id);
                String Normal = NAN.Decrypt(B64);
                var LST = NAN.GetUri(Normal, exp, null);
                if (LST.CurrentException == null)
                {
                    if (LST.Response.Count > 0)
                    {
                        return Ok(LST.Response[0].RI);
                    }
                    else {
                        return BadRequest(LST);
                    }
                }
                else {
                    return BadRequest(LST);
                }
                //return Ok(LST[0].RI);
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpGet("DeleteUrl/{id}")]
        public IActionResult DeleteUrl(String id)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                NAN.DeleteUri(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
          
        }

        [HttpGet("GeneraUrlTipoDoc/{id}/{exp}/{tipo}")]
        public IActionResult GeneraUrlTipoDoc(String id, int exp, String tipo)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                String B64 = NAN.Base64Decode(id);
                String Normal = NAN.Decrypt(B64);
                List<ResponseFileManager> LST = NAN.GetUri(Normal, exp, tipo).Response;
                return Ok(LST[0].RI);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
      
        }

        [HttpGet("GeneraUrlDocXIdDoc/{idDocumento}/{exp}/{token}")]
        public IActionResult GeneraUrlDocXIdDoc(String idDocumento, int exp, String token)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                List<ResponseFileManager> LST = NAN.GetUriById(idDocumento, exp, token).Response;
                return Ok(LST[0].RI);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
    
        }

        [HttpGet("ObtenerListaDocumentosContrato/{id}")]
        public IActionResult GetByContract(String id)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                var response = NAN.GetByContract(id);
                if (response.CurrentException == null)
                {
                    return Ok(response.Response);
                }
                else
                {
                    return BadRequest(response.CurrentException);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpGet("DeleteFileContract/{id}")]
        public IActionResult DeleteFileContract(String id)
        {
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                var response = NAN.DeleteFileContract(id);
                if (response.CurrentException == null)
                {
                    return Ok(response.Response);
                }
                else
                {
                    return BadRequest(response.CurrentException);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("DeleteUrl/monitoreo/id/{token}")]
        public IActionResult DeletedFileArchivo(String token)
        {
            
            try
            {
                NegocioAdministracionContratos.NegocioAdministracionArchivos NAN = new NegocioAdministracionContratos.NegocioAdministracionArchivos();
                var response = NAN.deleted_file_archivo_monitoreo(token);
                return Ok(response.Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }

        }



    }
}
