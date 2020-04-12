using System;
using System.Collections.Generic;
using System.Text;

namespace HFTD_Server_Core
{
    public class Command
    {
        public int CommandId { get; set; }
        public CommandType Type { get; set; }
        public int Target { get; set; }
        public int Target2 { get; set; }
        public int Amount { get; set; }
        public int Cost { get; set; }
        public int CostPort { get; set; }
        public int MaxPerHack { get; set; }

        public string CommandText { get
            {
                string returnText = "";
                switch(Type)
                {
                    case CommandType.InitialConnect:
                        returnText = "Initial connect";
                        break;
                    case CommandType.RedirectQPU:
                        returnText = string.Format("Redirect up to {0} QPU from port {1} to port {2}",Amount,CostPort,Target);
                        break;
                    case CommandType.LinkQPU:
                        returnText = string.Format("Link {0} QPU to port {1} (Maximum of {2} times per hack)", Cost, Target, MaxPerHack);
                        break;
                    case CommandType.AddNode:
                        returnText = string.Format("Add {0} nodes to Trace Route {1}, costs {2} QPU linked to port {3} per node added", Amount, Target, Cost, CostPort);
                        break;
                    case CommandType.ConnectToPort:
                        returnText = string.Format("Connect to port {0}", Target);
                        break;
                    case CommandType.ConnectToPortOneUser:
                        returnText = string.Format("Connect to port {0} (can only connect one user per tick)", Target);
                        break;
                    case CommandType.BruteForce:
                        returnText = string.Format("Brute force security system {0}, {1} damage, costs {2} QPU linked to port {3}", Target, Amount,Cost,CostPort);
                        break;
                    case CommandType.DownloadData:
                        returnText = "Download data";
                        break;
                    case CommandType.UploadData:
                        returnText = "Upload data";
                        break;
                    case CommandType.EditData:
                        returnText = "Edit data";
                        break;
                    case CommandType.RunManual:
                        returnText = "Run manual password cracker.exe";
                        break;
                    default:
                        returnText = "";
                        break;
                }
                return returnText;
        } 
        }
    }
}
