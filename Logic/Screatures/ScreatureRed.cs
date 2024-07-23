using System;
using System.Collections.Generic;
using Logic.Utils;

namespace Logic.Screatures
{
    public class ScreatureRed : BaseScreature
    {
        #region locals

        Random PersonalRandom;
        #endregion

        public ScreatureRed(double spawnX, double spawnY)
        {
            counterBase = 0;
            counterDestination = 100;
            carrying = false;
            X = spawnX;
            Y = spawnY;
            CurDestination = ENDestination.Destination.ResourceRed;
            PersonalRandom = new();
            angle = PersonalRandom.NextDouble();
            colour = "#741919";
        }

        public override void carry()
        {
            carrying = true;
            counterDestination = 0;
            colour = "#b10a0a";
            CurDestination = ENDestination.Destination.Base;
            angle += Math.PI;
        }

        public override void drop()
        {
            carrying = false;
            counterBase = 0;
            colour = "#741919";
            CurDestination = ENDestination.Destination.ResourceRed;
            angle += Math.PI;
        }

        public override void HearD(int newC, double scrX, double scrY)
        {
            counterDestination = newC;
            if (CurDestination == ENDestination.Destination.ResourceYellow)
            {
                setAngle(scrX, scrY);
            }
        }

        public override void HearB(int newC, double scrX, double scrY)
        {
            counterBase = newC;
            if (CurDestination == ENDestination.Destination.Base)
            {
                setAngle(scrX, scrY);
            }
        }
    }
}
