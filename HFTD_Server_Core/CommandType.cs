using System;
using System.Collections.Generic;
using System.Text;

namespace HFTD_Server_Core
{
    public enum CommandType
    {
        InitialConnect,
        RedirectQPU,
        LinkQPU,
        AddNode,
        ConnectToPort,
        ConnectToPortOneUser,
        BruteForce,
        DownloadData,
        UploadData,
        EditData,
        RunManual
    }
}
