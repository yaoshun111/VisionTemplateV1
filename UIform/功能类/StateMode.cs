using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionFram3
{
    public enum StateMode
    {
        Init = 0,
        InitDebug,
        InitOver,
        Login,
        LogSuccess,
        PointCheck,
        StartCheck1,
        StartChecking1,
        StartCheck2,
        StartChecking2,
        ReadyToAuto,
        AutoRun,
        ErrPC,
        ErrPLC,
        Stop,
        None
    }
}
