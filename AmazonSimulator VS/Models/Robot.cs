using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AmazonSimulator_VS;
using Controllers;

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
        private List<Node> _route;
        private List<Node> _queueroute=null;
        private bool isMoving = false;
        private bool onRoute = false;
        public bool justDropped = false;
        private Shelf shelf;
        private Node Destination;


        public string type { get; }
        public Guid guid { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }
        public bool route { get { return onRoute; } }

        public double targetX { get { return _tx; } }
        public double targetY { get { return _ty; } }
        public double targetZ { get { return _tz; } }

        
        public double speed = 0.10;

        public bool needsUpdate = true;

        public Robot(Node start) {
            this.type = "robot";
            this.guid = Guid.NewGuid();

            this._x = start.GetX();
            this._z = start.GetZ();
            this.Destination = start;

        }

        public virtual void Move(double x, double y, double z) {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = false;
        }

        public bool CheckMove()
        {
            return (isMoving);
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
                Route();
                MoveShelf();
                if(!(_queueroute==null))
                {
                    Queueroute(_queueroute);
                }
                return true;
            }
            return false;
        }

        public virtual void MoveShelf()
        {
            if(!(shelf == null))
            {
                shelf.Move(_x, (_y + 2.3), _z);
            }
        }

        public virtual void Changeroute(List<Node> route)
        {
            this._route = route;
            onRoute = true;
            needsUpdate = true;
        }

        public virtual void Queueroute(List<Node> route)
        {
            this._queueroute = route;
            if(!(onRoute) && !(isMoving))
            {
                Changeroute(route);
                this._queueroute = null;
            }
        }

        public virtual void Route()
        {            
            if(!(isMoving))
            {
                if(!(shelf==null))
                {
                    if (Destination.CheckDropoff())
                    {
                        shelf.Move(0, 10000, 0);
                        shelf = null;
                        justDropped = true;
                    }
                }

                else if(!(onRoute))
                {
                    if (Destination.CheckShelf())
                    {
                        shelf = Destination.PopShelf();
                    }

                }


            }


            if (onRoute)
            {
                if(!isMoving)
                {
                   

                        Destination = _route.Last();
                        Changedes(Destination.GetX(), 0, Destination.GetZ());
                        _route.RemoveAt(_route.Count()-1);
                    if(_route.Count()==0)
                    {
                      
                        onRoute = false;
                    }

                }
            }

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
        
        public bool CheckShelf()
        {
            if(!(shelf == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Node GetDestination()
        {
            return this.Destination;
        }

        public Shelf PopShelf()
        {
            Shelf shelf2 = shelf;
            shelf = null;
            return shelf2;
        }

        public bool CheckDropped()
        {
            return justDropped;
        }

        public void SetDropped()
        {
            justDropped = !(justDropped);
        }

    }
}