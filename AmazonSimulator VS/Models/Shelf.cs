using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
    public class Shelf : BaseObjects
    {

        public Shelf(double x, double y, double z)
        {
            this._type = "shelf";
            this._guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

        }       
    }
}