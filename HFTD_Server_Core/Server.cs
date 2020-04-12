using System;
using System.Collections.Generic;
using System.Text;

namespace HFTD_Server_Core
{
    public class Server
    {
        public Server()
        {
            Ports = new List<Port>();
            TracingRoutes = new List<TracingRoute>();
            TracingSystems = new List<TracingSystem>();
        }
        public string SoftwareVersion { get; set; }
        public List<Port> Ports { get; set; }
        public List<TracingRoute> TracingRoutes { get; set; }
        public List<TracingSystem> TracingSystems { get; set; }
    }
}
