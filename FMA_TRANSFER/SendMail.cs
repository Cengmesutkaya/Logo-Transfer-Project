using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FMA_TRANSFER
{
    class SendMail
    {

        public bool Send( string SLS_CODE,string NUMBER,string CL_CODE)
        {
            try
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("tunc.f@sumergida.com"); //gönderen
                mail.To.Add("tunc.f@sumergida.com"); //Alıcılar 
                mail.Subject = "FMA Sipariş Aktarım Hatası";
                mail.Body = SLS_CODE + " Satış Temsilcisinin,\n" + DateTime.Now + " tarihinde" + CL_CODE + " carisine atmış olduğu" + NUMBER + " nolu sipariş LOGO'ya aktarılırken bir sorun oluştu!";//İçeriğe yazılan mesaj
                SmtpClient sc = new SmtpClient();
                sc.Port = 25;
                sc.Host = "postaci.sumergida.com";
                sc.EnableSsl = false;
                sc.Send(mail);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
