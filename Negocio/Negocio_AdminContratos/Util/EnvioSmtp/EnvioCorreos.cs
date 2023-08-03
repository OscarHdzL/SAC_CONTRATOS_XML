using Modelos.Interfaz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio_contractual.Util.EnvioSmtp
{
    public class EnvioCorreos
    {
		private readonly ILoggerManager _logger;
		public EnvioCorreos()
		{
			_logger = new LoggerManager();
		}
		public void SendEmail(string SMTPHost, string remitenteSMTP, string SMTPPass, int Port, List<String> _emailDestino, String _subject, String _body)
		{
			try
			{
				NetworkCredential nwCredential = new NetworkCredential();
				//nwCredential.UserName = ConfigurationManager.AppSettings["remitenteSMTP"];
				String correos = "";
				nwCredential.UserName = remitenteSMTP;
				nwCredential.Password = SMTPPass;
				SmtpClient smtp = new SmtpClient();
				smtp.Port = Port;
				smtp.EnableSsl = true;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = nwCredential;
				smtp.Host = SMTPHost;
				MailMessage msg = new MailMessage();
				msg.From = new MailAddress(remitenteSMTP, "Sistema de Administración de Contratos y Proveedores");
				foreach (String cad in _emailDestino)
				{
					msg.To.Add(new MailAddress(cad));
					correos += cad + ',';
				}
				msg.CC.Add(new MailAddress(remitenteSMTP));
				msg.Subject = _subject;
				msg.Body = _body;
				msg.BodyEncoding = Encoding.UTF8;
				msg.IsBodyHtml = true;
				smtp.Send(msg);
				_logger.LogAdvertencia("Correo enviado correctamente a destinatarios "+correos);
			}
			catch (Exception ex)
			{
				_logger.LogError("EnvioCorreos - SendEmail",ex);

			}
		}
	}
}
