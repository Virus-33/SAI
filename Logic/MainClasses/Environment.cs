using Logic.Resources;
using Logic.Screatures;
using Logic.Utils;
using System;
using System.Collections.Generic;

namespace Logic.MainClasses
{
    public class Environment
    {
        #region params

        public List<BaseScreature> Creatures { get; private set; }

        public List<PointBase> Points { get; private set; }

        public List<PointBase> Spare { get; private set; }

        public int speed { get; private set; }

        public double spread { get; private set; }

        public int HearRadius { get; private set; }

        double baseX = 0;
        double baseY = 0;

        int phase = 1;

        int counter = 0;
        int ticks = 0;

        Random rnd;
        #endregion

        public Environment(int amount, double BaseX, double BaseY)
        {
            rnd = new();
            Points = new();
            Creatures = new();
            Points.Add(new Base(BaseX, BaseY, 2000));
            Creatures.Add(rnd.Next(1, 2) == 1 ? new ScreatureRed(BaseX, BaseY) : new ScreatureYellow(BaseX, BaseY));
            speed = 2;
            spread = 0.05;
            HearRadius = 20;
            baseX = BaseX;
            baseY = BaseY;
        }

        public void Move()
        {
            foreach (BaseScreature scr in Creatures)
            {
                scr.Step(speed, spread);
                ticks++;

                diePoint();
                
                foreach (PointBase res in Points)
                {
                    checkRes(res, scr);
                }

                int scream = phase == 1 ? scr.ScreamDestination(HearRadius) : scr.ScreamBase(HearRadius);
                foreach (BaseScreature s in Creatures)
                {
                    Scream(scr, s, scream);
                }
            }

            phase = phase == 1 ? 2 : 1;

            counter = counter > 4 ? 0 : counter + 1;

            if (Creatures.Count < 20 && counter == 4)
            {
                Creatures.Add(rnd.Next(1, 3) == 1 ? new ScreatureRed(baseX, baseY) : new ScreatureYellow(baseX, baseY));
            }

            foreach (PointBase pb in Spare)
            {
                if (pb is Base)
                {
                    pb.Harm(1);
                    break;
                }
            }
        }

        void diePoint()
        {
            Spare = new();
            Spare.AddRange(Points);
            foreach (PointBase pb in Spare)
            {
                if (pb.c_amount < 1)
                {
                    Points.Remove(pb);
                }
            }
        }

        void Scream(BaseScreature scr, BaseScreature s, int scream)
        {
            double distance = Math.Sqrt(Math.Pow(s.X - scr.X, 2) + Math.Pow(s.Y - scr.Y, 2));
            if (distance <= HearRadius) // ((scr.X - HearRadius <= s.X && s.X <= scr.X + HearRadius) && (scr.Y - HearRadius <= s.Y && s.Y <= scr.Y + HearRadius))
            {
                if (phase == 1)
                {
                    if ((scream < s.counterDestination) && ((s is ScreatureRed && scr is ScreatureRed) || (s is ScreatureYellow && scr is ScreatureYellow)))
                    {
                        s.HearD(scream, scr.X, scr.Y);
                    }
                }
                else
                {
                    if (scream < s.counterBase)
                    {
                        s.HearB(scream, scr.X, scr.Y);
                    }
                }
            }
        }

        void checkRes(PointBase res, BaseScreature scr)
        {
            double distance = Math.Sqrt(Math.Pow(res.X - scr.X, 2) + Math.Pow(res.Y - scr.Y, 2));
            if (distance <= 18) // (res.X - 12 <= scr.X && scr.X <= res.X + 12) && (res.Y - 12 <= scr.Y && scr.Y <= res.Y + 12)
            {
                if (res is Base && scr.CurDestination == ENDestination.Destination.Base)
                {
                    scr.drop();
                    res.Heal(25, scr is ScreatureRed ? 4 : 1);
                }
                else if (res is not Base && scr.CurDestination == res.CurDestination)
                {
                    scr.carry();
                    res.Harm(1);
                }
            }
            if (distance <= HearRadius && res is Base && scr.CurDestination == ENDestination.Destination.Base)
            {
                screamHome(scr);
            }
        }

        void screamHome(BaseScreature scr)
        {
            if (scr.CurDestination == ENDestination.Destination.Base)
            {
                scr.HearB(HearRadius, baseX, baseY);
            }
        }

        public void PlaceResource(double X, double Y, int type)
        {
            Points.Add(type == 1 ? new ResourceYellow(X, Y, rnd.Next(3, 9)) : new ResourceRed(X, Y, rnd.Next(1, 4)));
        }

        public void setSpred(double new_spread)
        {
            spread = new_spread;
        }

        public void setSpeed(int new_speed)
        {
            speed = new_speed;
        }

        public void setHearRadius(int new_radius)
        {
            HearRadius = new_radius;
        }

        public void NewBase(double X, double Y)
        {
            Spare = new();
            Spare.AddRange(Points);

            foreach (PointBase b in Spare)
            {
                if (b is Base)
                {
                    Points.Remove(b);
                    baseX = X;
                    baseY = Y;
                    break;
                }
            }
            Points.Add(new Base(X, Y, 2000));
        }
    }
}
