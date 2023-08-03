using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos;
using Negocio_AdminContratos;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace AdministracionContractual.Controllers
{

    public class EstructAreas
    {
        public Guid id { get; set; }
        public String Text { get; set; }
        public String Estatus { get; set; }


    }

    public class CargasMasivasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files, Guid idDependencia)
        {
            long size = files.Sum(f => f.Length);
            var filePaths = new List<string>();
            String startupPath = String.Empty;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    startupPath = System.IO.Directory.GetCurrentDirectory() + @"\TempFiles\" + formFile.FileName;
                    filePaths.Add(startupPath);
                    using (var stream = new FileStream(startupPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            /////////////////////////////////////
            Carga_Masiva_negocio carga = new Carga_Masiva_negocio();
            String Log =  carga.ReadExcel(startupPath, idDependencia);
            return Ok(JsonConvert.DeserializeObject<List<ErrorLst>>(Log));
        }
        [HttpPost]
        public async Task<IActionResult> FileUploadProveedores(List<IFormFile> files, Guid idDependencia,Guid idInstancia)
        {
            long size = files.Sum(f => f.Length);
            var filePaths = new List<string>();
            String startupPath = String.Empty;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    startupPath = System.IO.Directory.GetCurrentDirectory() + @"\TempFiles\" + DateTime.Now.Minute.ToString()+"_" +DateTime.Now.Hour.ToString()+"_" + formFile.FileName;
                    filePaths.Add(startupPath);
                    using (var stream = new FileStream(startupPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            /////////////////////////////////////
            Carga_Masiva_negocio carga = new Carga_Masiva_negocio();
            String Log = carga.ReadExcelProveedor(startupPath, idDependencia,idInstancia);
            return Ok(JsonConvert.DeserializeObject<List<ErrorLst>>(Log));
        }

        public ActionResult DownLoadLayoutInterlocutores() 
        {
            try
            {
                var startupPath = System.IO.Directory.GetCurrentDirectory() + @"\TempFiles\MasivaProveedores Layout.xlsx";
                byte[] bytes = System.IO.File.ReadAllBytes(startupPath);
                return File(bytes, "application/octet-stream", "MasivaProveedores Layout.xlsx");
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
            
        }



    }
}