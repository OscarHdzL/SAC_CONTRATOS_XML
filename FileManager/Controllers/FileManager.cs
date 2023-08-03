using System;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace FileManager.Controllers
{
    [ApiController]
    [Route("FileManager")]
    [EnableCors("AllowOrigin")]
    public class FileManagerController : ControllerBase
    {
     
        public readonly IConfiguration _config;
        public FileManagerController(IConfiguration config)
        {
            this._config = config;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [HttpPost]
        public bool UploadFile(IFormFile file, String Nom)
        {
            String Nombre = HttpContext.Request.Headers["Nombre"].ToString();
            String Extension = HttpContext.Request.Headers["Extension"].ToString();
           
            if (file == null)
            {
                return false;
            }
            String path = String.Empty;
            if (Nombre == "")
            {
                path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\" + Nom;
            }
            else {
                path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\" + Nombre + "." + Extension ;
            }

            try {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return true;
            }
            catch (Exception ex) {
                return false;
            }
            
        }

        [HttpDelete]
        public bool DeleteFile(String archiv)
        {
            String path = String.Empty;
            path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\";
            try
            {
                if (System.IO.File.Exists(@path + archiv))
                {
                    System.IO.File.Delete(@path + archiv);
                    return true;
                }
                return false;
            }
            catch (System.IO.IOException e)
            {
                return false;
            } 
        }

 

        [HttpGet]
        [Route("Download/{id}")]
        public IActionResult Download(String id)
        {
            MySqlConnection conexion = new MySqlConnection(_config.GetSection("RutaFisica").GetSection("conn").Value);
            MySqlCommand cmd = new MySqlCommand("sp_get_filemanager");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id", id));
            cmd.Connection = conexion;
            conexion.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var resp = reader[0].ToString();
            var nombrearchivo = reader[1].ToString();
            cmd.Connection.Close();
            try
            {
                String file_tmp = resp.ToLower();
                if (System.IO.File.Exists(resp))
                {
                    if (file_tmp.Contains(".pdf") || file_tmp.Contains(".jpg") || file_tmp.Contains(".png") || file_tmp.Contains(".bmp"))
                    {
                        return Ok(System.IO.File.OpenRead(resp));
                    }
                    else
                    {
                        Stream stream = System.IO.File.Open(resp, FileMode.Open);
                        return File(stream, "application/octet-stream", nombrearchivo);
                    }
                }
                return BadRequest();
            }
            catch (System.IO.IOException e)
            {
                return BadRequest();
            }
        }

        [HttpPost("Add")] //listo
        public IActionResult Add(IFormFile file)
        {
            String ConnectionString = String.Empty;
            String ext = Path.GetExtension(file.FileName);          
            obj _obj = new obj();
            String path = String.Empty;
            ConnectionString = _config.GetSection("RutaFisica").GetSection("conn").Value;            
            path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\";
            string Nom = Guid.NewGuid().ToString();
            var add = UploadFile(file, Nom+"_"+file.FileName);  
            if (add)
            {

                RespuestaAdd objResponse = new RespuestaAdd();
                //var _Conexion = ADO(2, "sp_json_file_manager", ConnectionString, path+Nom + "_", file.FileName, Nom, ext);
                objResponse.msg = ADO(2, "sp_json_file_manager", ConnectionString, path + Nom + "_", file.FileName, Nom, ext);
                objResponse.token = Guid.Parse(Nom);
                return Ok(objResponse);
            }
            return BadRequest("No se guardo archivo");            
        }





        [HttpPost("PM/Unitario/{tbl_plan_moniotoreo_id}/{tbl_Ubicacion_id}/{tbl_obligacion_id}")]  
        public IActionResult FileEjecucion(IFormFile file, Guid tbl_plan_moniotoreo_id, Guid tbl_Ubicacion_id, Guid tbl_obligacion_id)
        {
            String ConnectionString = String.Empty;
            String ext = Path.GetExtension(file.FileName);
            obj _obj = new obj();
            String path = String.Empty;
            ConnectionString = _config.GetSection("RutaFisica").GetSection("conn").Value;
            path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\";
            string Nom = Guid.NewGuid().ToString();
            Boolean add = UploadFile(file, Nom + "_" + file.FileName);
            if (add)
            {
                var _Conexion = ADO(2, "sp_json_file_manager", ConnectionString, path + Nom + "_", file.FileName, Nom, ext);
            }

            Guid identificador = Guid.NewGuid();
            String Result_ = FilesADO(identificador, tbl_plan_moniotoreo_id, tbl_Ubicacion_id, tbl_obligacion_id, Guid.Parse(Nom));

            return Ok(Result_);
        }

        //[HttpPost("PM/Unitario/{tbl_plan_moniotoreo_id}/{tbl_Ubicacion_id}/{tbl_obligacion_id}")]
        //public IActionResult FileEjecucion(IFormFile file, Guid tbl_plan_moniotoreo_id, Guid tbl_Ubicacion_id, Guid tbl_obligacion_id)
        //{
        //    String ConnectionString = String.Empty;
        //    String ext = Path.GetExtension(file.FileName);
        //    obj _obj = new obj();
        //    String path = String.Empty;
        //    ConnectionString = _config.GetSection("RutaFisica").GetSection("conn").Value;
        //    path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\";
        //    string Nom = Guid.NewGuid().ToString();
        //    Boolean add = UploadFile(file, Nom + "_" + file.FileName);
        //    if (add)
        //    {
        //        var _Conexion = ADO(2, "sp_json_file_manager", ConnectionString, path + Nom + "_", file.FileName, Nom, ext);
        //    }

        //    Guid identificador = Guid.NewGuid();
        //    String Result_ = FilesADO(identificador, tbl_plan_moniotoreo_id, tbl_Ubicacion_id, tbl_obligacion_id, Guid.Parse(Nom));

        //    return Ok(Result_);
        //}

        [HttpDelete] //listo
        [Route("Delete/{id}")]
        public IActionResult Delete(String id)
        {
            MySqlConnection conexion = new MySqlConnection(_config.GetSection("RutaFisica").GetSection("conn").Value);
            MySqlCommand cmd = new MySqlCommand("sp_get_filemanager");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id", id));
            cmd.Connection = conexion;
            conexion.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();            
            var nombrearchivo = reader[1].ToString();
            cmd.Connection.Close();            
            try { 
                string ConnectionString = string.Empty;
                string path = string.Empty;
                ConnectionString = _config.GetSection("RutaFisica").GetSection("conn").Value;      
                path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\";                
                if (DeleteFile(id+"_"+ nombrearchivo))
                {
                    string _Conexion = ADO(4, "sp_json_file_manager", ConnectionString, path, "", id, "");
                    return Ok(_Conexion);
                }
                return BadRequest("Archivo no eliminado");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
             {
                return BadRequest(ex);
            }            
        }

        [HttpPut] //listo
        [Route("Update/{id}")]
        public IActionResult Update(IFormFile file, String id)
        {
            string ConnectionString = String.Empty;
            string path = String.Empty;
            String ext = Path.GetExtension(file.FileName);
            ConnectionString = _config.GetSection("RutaFisica").GetSection("conn").Value;
            path = _config.GetSection("RutaFisica").GetSection("path").Value + @"\";
            MySqlConnection conexion = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand("sp_get_filemanager");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id", id));
            cmd.Connection = conexion;
            conexion.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var nombrearchivo = reader[1].ToString();            
            cmd.Connection.Close();
            if (DeleteFile(id + "_" + nombrearchivo))
            {
                if (UploadFile(file, id + "_" + file.FileName)) 
                {
                    String _Conexion = ADO(3, "sp_json_file_manager", ConnectionString, path + id + "_", file.FileName, id, ext);
                    return Ok(_Conexion);
                }
            }
            return BadRequest("No se guardo archivo");
        }  


        public static String ADO(int opt, String sp, String ConnectionString, String path, String filename,String Nom, String ext)
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConnectionString);
                MySqlCommand cmd = new MySqlCommand(sp);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("p_opt", opt));
                cmd.Parameters.Add(new MySqlParameter("p_id", Nom));
                cmd.Parameters.Add(new MySqlParameter("p_nombre_archivo", filename));
                cmd.Parameters.Add(new MySqlParameter("p_direccion_fisica", path+ filename));
                cmd.Parameters.Add(new MySqlParameter("p_tipo_extencion", ext));
                cmd.Connection = conexion;
                conexion.Open();
                var reader = cmd.ExecuteReader();
                reader.Read();
                var resp = reader[1].ToString();
                cmd.Connection.Close();
                return resp;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return ex.Message;
            }
        }


        public String FilesADO(Guid id_, Guid tbl_plan_moniotoreo_id_, Guid tbl_Ubicacion_id_, Guid tbl_obligacion_id_, Guid token_)
        {
            String ConnectionString = _config.GetSection("RutaFisica").GetSection("conn").Value;
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConnectionString);
                MySqlCommand cmd = new MySqlCommand("sp_tbl_ArchivosMonitoreo");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("id_", id_.ToString()));
                cmd.Parameters.Add(new MySqlParameter("tbl_plan_moniotoreo_id_", tbl_plan_moniotoreo_id_.ToString()));
                cmd.Parameters.Add(new MySqlParameter("tbl_Ubicacion_id_", tbl_Ubicacion_id_.ToString()));
                cmd.Parameters.Add(new MySqlParameter("tbl_obligacion_id_", tbl_obligacion_id_.ToString()));
                cmd.Parameters.Add(new MySqlParameter("token_", token_.ToString()));
                cmd.Connection = conexion;
                conexion.Open();

                MySqlDataReader myreader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(myreader);

 
                cmd.Connection.Close();
                return token_.ToString();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return Guid.Empty.ToString();
            }
        }

        /*****************************************************************************************************************************/
        //public String FilesADO_PE(Guid id_, Guid tbl_plan_moniotoreo_id_, Guid tbl_Ubicacion_id_, Guid tbl_obligacion_id_, Guid token_)
        //{
        //    String ConnectionString = _config.GetSection("RutaFisica").GetSection("conn").Value;
        //    try
        //    {
        //        MySqlConnection conexion = new MySqlConnection(ConnectionString);
        //        MySqlCommand cmd = new MySqlCommand("sp_tbl_ArchivosMonitoreo");
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new MySqlParameter("id_", id_.ToString()));
        //        cmd.Parameters.Add(new MySqlParameter("tbl_plan_moniotoreo_id_", tbl_plan_moniotoreo_id_.ToString()));
        //        cmd.Parameters.Add(new MySqlParameter("tbl_Ubicacion_id_", tbl_Ubicacion_id_.ToString()));
        //        cmd.Parameters.Add(new MySqlParameter("tbl_obligacion_id_", tbl_obligacion_id_.ToString()));
        //        cmd.Parameters.Add(new MySqlParameter("token_", token_.ToString()));
        //        cmd.Connection = conexion;
        //        conexion.Open();

        //        MySqlDataReader myreader = cmd.ExecuteReader();
        //        DataTable dt = new DataTable();
        //        dt.Load(myreader);


        //        cmd.Connection.Close();
        //        return token_.ToString();
        //    }
        //    catch (MySql.Data.MySqlClient.MySqlException ex)
        //    {
        //        return Guid.Empty.ToString();
        //    }
        //}

    }
    public class obj { 
        public String p_id { get; set; }
        public String p_nombre_archivo { get; set; }
        public String p_direccion_fisica { get; set; }
        public String p_tipo_extencion { get; set; }
    }

    public class RespuestaAdd
    {
        public Guid token { get; set; }
        public String msg { get; set; }
        
    }
}
