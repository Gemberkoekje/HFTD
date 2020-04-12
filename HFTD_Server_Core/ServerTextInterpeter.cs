using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HFTD_Server_Core
{
    public static class ServerTextInterpeter
    {
        public static Server CreateServer(string serverText)
        {
            var server = new Server();
            serverText = serverText.ToLower();
            var lines = serverText.Split("\r\n");
            var CurrentPort = 0;
            foreach(var line in lines)
            {
                var trimmedline = line.Trim();
                if(trimmedline.StartsWith("port"))
                {
                    //Creating a new port
                    var startchar = trimmedline.IndexOf("port");
                    var port = trimmedline.Substring(startchar + 5, 1);
                    string CurQPE = "0";
                    string MaxQPE = "0";
                    if (trimmedline.Length > startchar + 7)
                    {
                        CurQPE = trimmedline.Substring(startchar + 8, 1);
                        MaxQPE = trimmedline.Substring(startchar + 10, 1);
                    }
                    int portnr;
                    int CurQPEnr;
                    int MaxQPEnr;
                    int.TryParse(port, out portnr);
                    int.TryParse(CurQPE, out CurQPEnr);
                    int.TryParse(MaxQPE, out MaxQPEnr);
                    server.Ports.Add(new Port() { PortNumber = portnr, QPU = CurQPEnr, MaxQPU = MaxQPEnr });
                    CurrentPort = portnr;
                    continue;
                }else if (CurrentPort == 0)
                {
                    server.SoftwareVersion = trimmedline;
                    continue;
                }
                if(trimmedline.Contains("initial"))
                {
                    var port = server.Ports.First(p => p.PortNumber == CurrentPort);
                    port.Commands.Add(new Command() { Type = CommandType.InitialConnect }) ;
                }
                if (trimmedline.Contains("connect to port"))
                {
                    var startchar = trimmedline.IndexOf("port");
                    var targetPort = trimmedline.Substring(startchar + 5, 1);
                    int targetPortnr;
                    int.TryParse(targetPort, out targetPortnr);
                    var port = server.Ports.First(p => p.PortNumber == CurrentPort);
                    var onlyoneconnection = trimmedline.Contains("can only connect one user per tick");
                    port.Commands.Add(new Command() { Type = onlyoneconnection ? CommandType.ConnectToPortOneUser : CommandType.ConnectToPort, Target = targetPortnr });
                }
                if (trimmedline.Contains("brute force"))
                {
                    var words = trimmedline.Split(" ");

                    var startchar = trimmedline.IndexOf("security system");
                    var targetSystem = trimmedline.Substring(startchar + 16, 1);
                    int targetSystemnr;
                    int.TryParse(targetSystem, out targetSystemnr);
                    var port = server.Ports.First(p => p.PortNumber == CurrentPort);
                    var onlyoneconnection = trimmedline.Contains("can only connect one user per tick");
                    port.Commands.Add(new Command() { Type = onlyoneconnection ? CommandType.ConnectToPortOneUser : CommandType.ConnectToPort, Target = targetSystemnr });
                }
            }

            return server;
        }
    }
}
