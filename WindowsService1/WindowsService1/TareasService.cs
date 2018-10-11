using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ServicioDeTareas
{
    public partial class TareasService : ServiceBase
    {
        public TareasService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Set up a timer that triggers every minute.
            this.Log("Servicio iniciado...");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            this.Log("Servicio detenido");
        }

        public void Log(string mensaje)
        {
            DateTime fecha = DateTime.Now;
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            File.AppendAllText(path+ "\\log_tareas.txt", fecha.ToString() + ": " + mensaje + Environment.NewLine);
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            //eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            StreamReader objReader = new StreamReader(path+"\\tareas.txt");
            string Url = "";
            while (Url != null)
            {
                Url = objReader.ReadLine();
                if (Url != null)
                {
                    if (!Url.StartsWith("#"))
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    }
                }
            }
            objReader.Close();
        }
    }
}
