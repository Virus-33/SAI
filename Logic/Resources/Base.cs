using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Resources
{
    public class Base : PointBase
    {
        public Base(double spawnX, double spawnY, int stock)
        {
            CurDestination = Utils.ENDestination.Destination.Base;
            X = spawnX;
            Y = spawnY;
            c_amount = stock;
            colour = "#c89117";
        }
    }
}
