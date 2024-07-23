using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Resources
{
    public class ResourceRed : PointBase
    {

        public ResourceRed(double spawnX, double spawnY, int stock)
        {
            CurDestination = Utils.ENDestination.Destination.ResourceRed;
            X = spawnX;
            Y = spawnY;
            c_amount = stock;
            colour = "#f40606";
        }

        public override void Heal(int amount, int type)
        {
            int a = 1;
        }
    }
}
