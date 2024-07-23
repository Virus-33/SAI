using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Resources
{
    public class ResourceYellow : PointBase
    {

        public ResourceYellow(double spawnX, double spawnY, int stock)
        {
            CurDestination = Utils.ENDestination.Destination.ResourceYellow;
            X = spawnX;
            Y = spawnY;
            c_amount = stock;
            colour = "#dfe024";
        }

        public override void Heal(int amount, int type)
        {
            int a = 1;
        }
    }
}
