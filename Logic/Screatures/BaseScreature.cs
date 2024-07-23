using System;
using Logic.Utils;

namespace Logic.Screatures
{
    public abstract class BaseScreature
    {
        #region locals
        public double X { get; protected set; }
        public double Y { get; protected set; }

        public double angle { get; protected set; }

        public int counterBase { get; protected set; }

        public int counterDestination { get; protected set; }

        public bool carrying { get; protected set; }

        public string colour { get; protected set; }

        Random PersonalRandom = new();

        public ENDestination.Destination CurDestination { get; protected set; }
        #endregion

        public int ScreamBase(int HearRadius)
        {
            return counterBase + HearRadius;
        }

        public int ScreamDestination(int HearRadius)
        {
            return counterDestination + HearRadius;
        }

        public void Step(int speed, double spread)
        {
            double next = PersonalRandom.NextDouble();

            double minimum = (spread * -1);

            angle += minimum + (next * (spread - minimum));

            if (X + speed < 700 && X + speed > 0)
            {
                X += speed * (Math.Sin(angle));
            }
            else
            {
                angle += Math.PI;
                X += speed * Math.Sin(angle);
            }
            if (Y + speed < 360 && Y + speed > 0)
            {
                Y += speed * Math.Cos(angle);
            }
            else
            {
                angle += Math.PI;
                Y += speed * Math.Cos(angle);
            }
            this.counterBase++;
            this.counterDestination++;
        }

        public virtual void HearD(int newC, double scrX, double scrY)
        {
            counterDestination = newC;
        }

        public virtual void HearB(int newC, double scrX, double scrY)
        {
            counterBase = newC;
        }

        public virtual void carry()
        {
            counterDestination = 0;
            carrying = true;
        }

        public virtual void drop()
        {
            counterBase = 0;
            carrying = false;
        }

        public void setAngle(double x, double y)
        {
			double tX = x;
			double tY = Y;
	
            double distance = Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));

            double new_angle = Math.Acos((y - Y) / distance);
            angle = x > X ? new_angle : (Math.Tau - new_angle);
        }

    }
}
