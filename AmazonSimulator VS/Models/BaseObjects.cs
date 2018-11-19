using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class BaseObjects : IUpdatable
    {
        protected double _x = 0;
        protected double _y = 0;
        protected double _z = 0;
        protected double _rX = 0;
        protected double _rY = 0;
        protected double _rZ = 0;
        protected double _tx = 0;
        protected double _ty = 0;
        protected double _tz = 0;
        protected double speed = 0.30;
        protected bool moving = false;
        protected bool destinationReached = true;
        protected bool killme = false;

        protected string _type;
        protected Guid _guid;

        public string type { get { return _type; }}
        public Guid guid { get { return _guid; }}
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }

        public double targetX { get { return _tx; } }
        public double targetY { get { return _ty; } }
        public double targetZ { get { return _tz; } }
        
        public bool needsUpdate = true;
        public bool ismoving { get { return moving; } }
        public bool checkdes { get { return destinationReached; } }

        public virtual void Move(double x, double y, double z)
        {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = true;
        }


        public virtual bool Update(int tick)
        {
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }

        public virtual void Changedes(double xdes, double ydes, double zdes)
        {
            this._tx = xdes;
            this._ty = ydes;
            this._tz = zdes;
            moving = true;
            needsUpdate = true;
            destinationReached = false;
        }

        /// <summary>
        /// Deze functie word elke 50 ticks aangeroepen door update.
        /// Als de is
        /// </summary>
        public virtual void Moving()
        {

            if (moving)
            {
                if (!(Convert.ToInt16(x) == Convert.ToInt16(targetX)))
                {
                    _rY = 92.69;
                    if (x < targetX)
                    {
                        _x += speed;
                    }
                    else if (x > targetX)
                    {
                        _x -= speed;
                    }

                }
                else if (!(Convert.ToInt16(z) == Convert.ToInt16(targetZ)))
                {
                    _rY = 0;
                    if (z < targetZ)
                    {
                        _z += speed;
                    }
                    else if (z > targetZ)
                    {
                        _z -= speed;
                    }
                }

                else
                {
                    moving = false;
                    needsUpdate = false;
                    destinationReached = true;
                }
            }
        }

    }

    
}
