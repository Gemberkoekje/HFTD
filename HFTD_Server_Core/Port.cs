using System;
using System.Collections.Generic;
using System.Text;

namespace HFTD_Server_Core
{
    public class Port
    {
        public Port()
        {
            Commands = new List<Command>();
        }
        public int PortNumber { get; set; }
        public int QPU { get; set; }
        public int MaxQPU { get; set; }
        public List<Command> Commands { get; set; }
    }
}
