using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Solucion_Negocio.Util.EnvioSmtp
{
    public class EnvioCorreos
    {
		public static void SendEmail(string SMTPHost, string remitenteSMTP, string SMTPPass, int Port, List<String> _emailDestino, String _subject, String _body)
		{
			try
			{
				

				NetworkCredential nwCredential = new NetworkCredential();
				//nwCredential.UserName = ConfigurationManager.AppSettings["remitenteSMTP"];
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
				}
				msg.CC.Add(new MailAddress(remitenteSMTP));
				msg.Subject = _subject;
				msg.Body = _body;
				msg.BodyEncoding = Encoding.UTF8;
				msg.IsBodyHtml = true;
				smtp.Send(msg);
			}
			catch (Exception ex)
			{

			}
		}
	}
}
