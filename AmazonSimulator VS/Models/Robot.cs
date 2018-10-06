using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robot : IUpdatable {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _rX = 0;
        private double _rY = 0;
        private double _rZ = 0;
        private double _tx = 0;
        private double _ty = 0;
        private double _tz = 0;

        public string type { get; }
        public Guid guid { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }

        public double targetX { get { return _tx; } }
        public double targetY { get { return _ty; } }
        public double targetZ { get { return _tz; } }

        bool destinationreached = true;
        bool isMoving = false;
        public double speed = 0.10;

        public bool needsUpdate = true;

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ) {
            this.type = "robot";
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }

        public virtual void Move(double x, double y, double z) {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = false;
        }

        public virtual void Rotate(double rotationX, double rotationY, double rotationZ) {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = false;
        }

        public virtual bool Update(int tick)
        {
            if(needsUpdate) {
                Moving();                
                return true;
            }
            return false;
        }

        public virtual void Changedes(double xdes, double ydes, double zdes)
        {
            this._tx = xdes;
            this._ty = ydes;
            this._tz = zdes;
            isMoving = true;
            needsUpdate = true;
        }
        public virtual void Moving()
        {

            if (isMoving)
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
                else if(!(Convert.ToInt16(z) == Convert.ToInt16(targetZ)))
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
                    isMoving = false;
                    needsUpdate = false;
                }
            }
        }
    }
}