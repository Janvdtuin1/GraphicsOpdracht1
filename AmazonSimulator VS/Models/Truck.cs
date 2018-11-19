using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Truck : BaseObjects
    {
        private int inv = 0;
        private int _maxinv = 4;       

        public int inventory { get { return inv; } }
        public int maxinv { get { return _maxinv; } }
        public void PlusInv() {inv += 1;}

        public Truck(double x, double y, double z)
        {
            this._type = "truck";
            this._guid = Guid.NewGuid();
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public override bool Update(int tick)
        {           
            if (needsUpdate)
            {
                Moving();
                if (killme && !moving)
                {
                    _y = 1000;
                }
                return true;
            }
            return false;
        }       

        public void Kill()
        {
            killme = true;
            Changedes(60, 0, 0);           
        }
    }
}