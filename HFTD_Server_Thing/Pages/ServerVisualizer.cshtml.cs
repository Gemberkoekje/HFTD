using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HFTD_Server_Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HFTD_Server_Thing.Pages
{
    public class ServerVisualizerModel : PageModel
    {


        public Server Server { get; set; }
        public void OnPostAddPort()
        {
            Server = Load();
            int freenumber = 1;
            while(Server.Ports.Any(p => p.PortNumber == freenumber))
            {
                freenumber++;
            }
            Server.Ports.Add(new Port() { PortNumber = freenumber});
            Save(Server);
        }
        public void OnPostAddCommand(int portid)
        {
            Server = Load();
            var port = Server.Ports.Single(p => p.PortNumber == portid);
            int freenumber = 1;
            while (port.Commands.Any(c => c.CommandId == freenumber))
            {
                freenumber++;
            }
            port.Commands.Add(new Command() { CommandId = freenumber });
            Save(Server);
        }
        public void OnPostRemovePort(int id)
        {
            Server = Load();
            Server.Ports.Remove(Server.Ports.Single(p => p.PortNumber == id));
            Save(Server);
        }
        public void OnPostRemoveCommand(int id, int portid)
        {
            Server = Load();
            var port = Server.Ports.Single(p => p.PortNumber == portid);
            port.Commands.Remove(port.Commands.Single(c => c.CommandId == id));
            Save(Server);
        }
        public void OnPostEditServer(int id, string editname)
        {
            Server = Load();
            Server.SoftwareVersion = editname;
            Save(Server);
        }
        public void OnPostEditPort(int id, int editid, int editqpu, int editmaxqpu)
        {
            Server = Load();
            var port = Server.Ports.Single(p => p.PortNumber == id);
            if(id != editid && !Server.Ports.Any(p => p.PortNumber == editid))
            {
                port.PortNumber = editid;
            }
            port.QPU = editqpu;
            port.MaxQPU = editmaxqpu;
            Save(Server);
        }
        public void OnPostEditCommand(int id, int portid, CommandType editcommandtype, int edittarget, int edittarget2, int editamount, int editcost, int editcostport, int editmaxperhack)
        {
            Server = Load();
            var port = Server.Ports.Single(p => p.PortNumber == portid);
            var command = port.Commands.Single(c => c.CommandId == id);
            command.Type = editcommandtype;
            command.Target = edittarget;
            command.Target2 = edittarget2;
            command.Amount = editamount;
            command.Cost = editcost;
            command.CostPort = editcostport;
            command.MaxPerHack = editmaxperhack;
            Save(Server);
        }
        public void OnGet()
        {
            Server = Load();
            Save(Server);
        }

        public void Save(Server server)
        {
            HttpContext.Session.Set("Server", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(server)));
        }

        public Server Load()
        {
            
            byte[] serializedserver;
            if(!HttpContext.Session.TryGetValue("Server", out serializedserver))
            {
                var server = new Server();
                Save(server);
                return server;
            }
            string serializedserverstring = Encoding.UTF8.GetString(serializedserver);
            return JsonConvert.DeserializeObject<Server>(serializedserverstring);

        }
    }
}