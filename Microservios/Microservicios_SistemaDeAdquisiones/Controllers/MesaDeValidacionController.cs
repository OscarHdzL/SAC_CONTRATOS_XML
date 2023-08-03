using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Negocio_SistemaAdquisiciones;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Modelos.Modelos;

namespace Microservicios_SistemaDeAdquisiciones.Controllers
{
    [Produces("application/json")]
    [Route("MesaValidacion")]
    [EnableCors("CorsPolicy")]
    public class AreaControllere : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        static readonly String PasswordHash = "@PMS|FM0";
        static readonly String SaltKey = "S@LT&KEY";
        static readonly String VIKey = "@6D65GKJFZA4GSSD";
        #endregion
        public AreaControllere(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones

        [HttpPost("Init/{Solicitud}/{Token}")]
        public IActionResult Init(String Solicitud,String Token)
        {
            String Res64 = Base64Decode(Token);
            String ResMd5 = Decrypt(Res64);
            MesaValidacion_Negocio Neg = new MesaValidacion_Negocio();
            return Ok(Neg.MesaValidacion_(Solicitud, ResMd5));
        }
        [HttpGet("GetNumSol/{Solicitud}")]
        public IActionResult Init(Guid Solicitud)
        {
            MesaValidacion_Negocio Neg = new MesaValidacion_Negocio();
            return Ok(Neg.get_num_sol(Solicitud));
        }
        [HttpGet("get_mesa/{Solicitud}")]
        public IActionResult get_mesa(Guid Solicitud)
        {
            MesaValidacion_Negocio Neg = new MesaValidacion_Negocio();
            return Ok(Neg.get_mesa(Solicitud));
        }
        [HttpGet("get_mesa_solicitante/{Solicitud}")]
        public IActionResult get_mesa_solicitante(Guid Solicitud)
        {
            MesaValidacion_Negocio Neg = new MesaValidacion_Negocio();
            List<DocumentsFileManager> DF = Neg.get_mesa_solicitante(Solicitud).Response;
            List<DocumentsFileManager> Response_ = new List<DocumentsFileManager>();
            foreach (DocumentsFileManager DFM in DF)
            {
                DFM.Token = Base64Encode(Encrypt(DFM.Token)); ;
                Response_.Add(DFM);
            }
            return Ok(Response_);
        }

        public String Encrypt(String plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public String Decrypt(String encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public String Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public String Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion
    }
}