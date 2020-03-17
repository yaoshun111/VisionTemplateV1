using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Variables
{
    public class VrScan
    {
        public bool StartCheck1 = false;
        public bool StartCheck2 = false;
    }


   public class Mint
    {
        private int a;
        object intlock = new object();
        public int value
        {
            get
            {
                lock (intlock)
                {
                    return a;
                }
                
            }
            set
            {
                lock (intlock)
                {
                    a = value;
                }
            }
        }
    }



   public class Mstring
   {
       private string a;
       object intlock = new object();
       public string value
       {
           get
           {
               lock (intlock)
               {
                   return a;
               }
           }
           set
           {
               lock (intlock)
               {
                   a = value;
               }
           }
       }

   }
}
