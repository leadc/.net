using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace ServicioDeTareas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TareasService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
