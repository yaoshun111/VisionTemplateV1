using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCtr
{
    public enum VarType
    {
        Int32,
        Int64,
        Single,
        Double,
        String,
        Boolean,
    }

    class VarStructure
    {




    }


    public enum StateMode
    {
        None = 0,
        Init = 1,
        InitDebug = 2,
        InitOver = 3,
        Login = 4,
        LogSuccess = 5,
        PointCheck = 6,
        StartCheck1 = 7,
        StartChecking1 = 8,
        StartCheck2 = 9,
        StartChecking2 = 10,
        ReadyToAuto = 11,
        AutoRun = 12,
        ErrPC = 13,
        ErrPLC = 14,
        Stop = 15,
    }


}
