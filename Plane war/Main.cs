using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plane_war
{
    internal class Mainspace
    {
        static void Main(string[] args)
        {
            //啟動位置
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
