using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace MailerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************");
            Console.WriteLine("*     Sending mail    *");
            Console.WriteLine("*     Please wait     *");
            Console.WriteLine("***********************");
            if(args.Length < 5)
            {
                Console.WriteLine("Faltan parámetros necesarios (remitente, contraseña, destino, asunto, mensaje)");
                Console.ReadKey();
            }
            else
            {
                //Mailer.SendOffice365Mail("reservas.auto.cl@applusglobal.com", "kAH$vZZt","leandro.caceres@applus.com","Subject","<br>HTML mail<br>");
                Mailer.SendOffice365Mail(args[0], args[1], args[2], args[3], args[4]);
                Console.WriteLine("Mail sent successfuly!");
            }
        }
    }

    public class Mailer
    {
        public static void SendOffice365Mail(string from_address, string pass, string to_address, string message_subject, string message_body)
        {
            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(from_address, pass);
            MailAddress from = new MailAddress(from_address, String.Empty, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(to_address);
            MailMessage message = new MailMessage(from, to);
            message.From = from;
            message.IsBodyHtml = true;
            message.Body = message_body;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = message_subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            client.Send(message);
        }
    }
}
