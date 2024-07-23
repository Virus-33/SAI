using Logic.Utils;

namespace Logic.Resources
{
    public abstract class PointBase
    {
        public double X { get; protected set; }

        public double Y { get; protected set; }

        public int c_amount { get; protected set; }

        public string colour { get; protected set; }

        public ENDestination.Destination CurDestination { get; protected set; }

        public virtual void Harm(int amount)
        {
            if (c_amount - amount >= 0)
            {
                c_amount -= amount;
            }
            else
            {
                c_amount = 0;
            }
        }

        public virtual void Heal(int amount, int type)
        {
            if (c_amount + (amount*type) <= 100)
            {
                c_amount += (amount * type);
            }
            else
            {
                c_amount = 100;
            }
        }

    }
}
