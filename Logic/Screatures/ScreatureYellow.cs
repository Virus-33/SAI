using System;
using System.Collections.Generic;
using Logic.Utils;

namespace Logic.Screatures
{
    public class ScreatureYellow : BaseScreature
    {
        #region locals

        Random PersonalRandom;
        #endregion

        public ScreatureYellow(double spawnX, double spawnY)
        {
            carrying = false;
            X = spawnX;
            Y = spawnY;
            counterBase = 0;
            counterDestination = 100;
            CurDestination = ENDestination.Destination.ResourceYellow;
            PersonalRandom = new();
            angle = PersonalRandom.NextDouble();
            colour = "#a1a146";
        }

        public override void carry()
        {
            carrying = true;
            counterDestination = 0;
            colour = "#c9c92d";
            CurDestination = ENDestination.Destination.Base;
            angle += Math.PI;
        }

        public override void drop()
        {
            carrying = false;
            counterBase = 0;
            colour = "#a1a146";
            CurDestination = ENDestination.Destination.ResourceYellow;
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
