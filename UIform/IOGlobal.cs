using FastCtr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIform
{
    public class IOGlobal
    {
        public static TaskFrameGlobal Global;

        public static StateMode Istate
        {
            get
            {

                return (StateMode)(Global.G_GetVar("vrstate"));
            }
            set
            {
                Global.G_SetVar("vrstate", value);
            }
        }


   
        public static bool Ostart = false;
    

    }
}
