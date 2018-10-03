﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Shelf : IUpdatable
    {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
      

        public string type { get; }
        public Guid guid { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }

        public bool needsUpdate = true;

        public Shelf(double x, double y, double z)
        {
            this.type = "shelf";
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

        }

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

       
    }
}