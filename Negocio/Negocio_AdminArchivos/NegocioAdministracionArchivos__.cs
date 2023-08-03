using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Area;
using Modelos.Response;
using Utilidades.Log4Net;

namespace NegocioAdministracionContratos
{
    public class NegocioAdministracionArchivos
    {
        private readonly ILoggerManager _logger;

        static readonly String PasswordHash = "@PMS|FM0";
        static readonly String SaltKey = "S@LT&KEY";
        static readonly String VIKey = "@6D65GKJFZA4GSSD";

        public NegocioAdministracionArchivos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<ResponseFileManager>> Download(String ID)
        {
            String decode64 = Base64Decode(ID);
            String decodeMD5 = Decrypt(ID);
            String[] Array = decodeMD5.Split('|');
            String MD5BDatos = Array[1];

            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            return new ResponseGeneric<List<ResponseFileManager>>("");
        }

        public ResponseGeneric<List<ResponseFileManager>> Get(AdministracionArchivos AdministracionArchivos_)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.Get(AdministracionArchivos_);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        //public ResponseGeneric<List<ResponseFileManager>> GetNew(AdministracionArchivos AdministracionArchivos_, string entrega)
        //{
        //    AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
        //    try
        //    {
        //        return FM.GetNew(AdministracionArchivos_, entrega);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<ResponseFileManager>>(ex);
        //    }
        //}

        public ResponseGeneric<List<ResponseFileManager>> GetNewEntrega(AdministracionArchivos AdministracionArchivos_, string entrega)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.GetNewEntrega(AdministracionArchivos_, entrega);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileManager>> GetNewUb(AdministracionArchivos AdministracionArchivos_, string entrega, string ubicacion)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.GetNewUbi(AdministracionArchivos_, entrega, ubicacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileManager>> GetNewMonitoreo(AdministracionArchivos AdministracionArchivos_, string entrega, string ubicacion, string monitoreo)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.GetNewMonitoreo(AdministracionArchivos_, entrega, ubicacion, monitoreo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<Token_confirmacion>> InsertTokenFile(string token, string fileManagerId)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.sp_insert_token(token, fileManagerId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Token_confirmacion>>(ex);
            }
        }

        //public ResponseGeneric<List<ResponseFileManager>> GetVersions(AdministracionArchivos AdministracionArchivos_)
        //{
        //    AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
        //    try
        //    {
        //        return FM.Get(AdministracionArchivos_);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneric<List<ResponseFileManager>>(ex);
        //    }
        //}

        public ResponseGeneric<List<ResponseFileManager>> Gettoken(String uri)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.Gettoken(uri);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }
        public ResponseGeneric<List<ResponseFileManager>> GetBuffer(Guid id)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.GetBuffer(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }
        public ResponseGeneric<List<ResponseFileManager>> GetBufferVersion(Guid id, int version)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.GetBufferVersion(id,version);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }
        public ResponseGeneric<List<ResponseFileManagerInfo>> GetDocInfo(Guid id)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.GetDocInfo(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManagerInfo>>(ex);
            }
        }
        public ResponseGeneric<List<ResponseFileManager>> remove(String Token_)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                ResponseGeneric<List<ResponseFileManager>> OBJ = FM.Remove(Token_);
 
                return OBJ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }


        public ResponseGeneric<List<ResponseFileManager>> GetUri(String Token_, int expiration_, String tipo_documento_)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                var OBJ = FM.GetUri(Token_, expiration_, tipo_documento_);
                if (OBJ.CurrentException != null) 
                { 
                    return new ResponseGeneric<List<ResponseFileManager>>(OBJ.CurrentException);
                }
                if (OBJ.Response.Count == 0) {
                    return new ResponseGeneric<List<ResponseFileManager>>(Token_+" | "+expiration_.ToString()+" | "+tipo_documento_);
                }
                String RFM = Encrypt(Token_ + "|" + OBJ.Response[0].RI);
                OBJ.Response[0].RI = Base64Encode(RFM);
                return OBJ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public dynamic DeleteUri(String id)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                FM.DeleteUri(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return false;
            }
        }


        public ResponseGeneric<List<ResponseFileManager>> GetUriById(String id_document_, int expiration_, String Token_)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                ResponseGeneric<List<ResponseFileManager>> OBJ = FM.GetUriById(id_document_, expiration_);

                String RFM = Encrypt(Token_ + "|" + OBJ.Response[0].RI);
                OBJ.Response[0].RI = Base64Encode(RFM);
                return OBJ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileList>> GetByContract(String idContract_)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.GetByContract(idContract_);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileList>>(ex);
            }
        }

        public ResponseGeneric<dynamic> DeleteFileContract(String idFile)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                return FM.DeleteFileContract(idFile);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<dynamic>(ex);
            }

        }

        public ResponseGeneric<dynamic> deleted_file_archivo_monitoreo(string token)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                ResponseGeneric<dynamic> response = FM.deleted_file_archivo_monitoreo(token);

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
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

        public List<DocumentsFileManagerVersion> documentary_information_GetListVersions(String Token)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
            try
            {
                List<DocumentsFileManagerVersion> obj = FM.documentary_information_GetListVersions(Decrypt(Base64Decode(Token))).Response;
                List<DocumentsFileManagerVersion> result = new List<DocumentsFileManagerVersion>();
                foreach (DocumentsFileManagerVersion DFM in obj)
                {
                    DFM.Token = Base64Encode(Encrypt(DFM.Token));
                    result.Add(DFM);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new List<DocumentsFileManagerVersion>();
            }
        }


        public String documentary_information_update_state(String Token, String description_, int tipo)
        {
            AccesoDatos_AdministracionArchivos.AdministracionArchivos_ FM = new AccesoDatos_AdministracionArchivos.AdministracionArchivos_();
       
                List<CrudresponseNum> obj = FM.documentary_information_update_state(
                        Decrypt(Base64Decode(Token)), 
                        description_,
                        tipo
                    ).Response;
            

                return obj[0].cod;
            
        }

    }
}
