using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.IO;

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
            //Mailer.SendOffice365Mail("turnos.auto.ar@applusglobal.com", "Jd_k9!?J02", "leandro.caceres@applus.com", "Subject", "<br>HTML mail<br>á  é  í");
            //return;
            if (args.Length < 5)
            {
                Console.WriteLine("Faltan parámetros necesarios (remitente, contraseña, destino, asunto, mensaje");
                StreamWriter sw = new StreamWriter("MailerApp_log.txt", true);
                sw.WriteLine("Envío erroneo:");
                for (int i = 0; i < args.Length; i++)
                {
                    sw.WriteLine(args[i].ToString());
                }
                sw.Close();
            }
            else
            {
                
                try
                {
                    Mailer.SendOffice365Mail(args[0], args[1], args[2], args[3], args[4]);
                }
                catch (Exception e)
                {
                    StreamWriter sw = new StreamWriter("MailerApp_log.txt", true);
                    sw.WriteLine("Envío erroneo:");
                    sw.WriteLine(DateTime.Now);
                    for (int i = 0; i < args.Length; i++)
                    {
                        sw.WriteLine(args[i].ToString());
                    }
                    sw.WriteLine(e.Message);
                    sw.WriteLine("---------------");
                    sw.Close();

                }
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
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = message_body;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Subject = message_subject;
            client.Send(message);
        }

        public static void SendApplusITVUYMail(string from_address, string pass, string to_address, string message_subject, string message_body)
        {
            SmtpClient client = new SmtpClient("webmail.applusitv.uy", 25);
            client.EnableSsl = false;
            client.Credentials = new System.Net.NetworkCredential(from_address, pass);
            MailAddress from = new MailAddress(from_address, String.Empty, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(to_address);
            MailMessage message = new MailMessage(from, to);
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = message_body;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Subject = message_subject;
            client.Send(message);
        }
    }
}
