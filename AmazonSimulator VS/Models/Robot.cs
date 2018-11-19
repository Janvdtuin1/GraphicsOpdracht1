using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AmazonSimulator_VS;
using Controllers;

namespace Models {
    public class Robot : IUpdatable
    {
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
        private List<Node> _queueroute = null;
        private bool moving = false;
        private bool onroute = false;
        private bool _justdropped = false;
        private Shelf shelf=null;
        private Node _destination;
        private bool _busy = false;

        public string type { get; }
        public Guid guid { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }
        public bool route { get { return onroute; } }
        public bool busy { get { return _busy; } }

        public double targetX { get { return _tx; } }
        public double targetY { get { return _ty; } }
        public double targetZ { get { return _tz; } }
        public bool justdropped { get { return _justdropped; } }
        public bool checkshelf { get { return (!(shelf == null)); } }
        public Node destination { get { return _destination; } }
        public bool ismoving { get { return moving; } }

        private double speed = 0.10;

        public bool needsUpdate = true;

        public Robot(Node start) {
            this.type = "robot";
            this.guid = Guid.NewGuid();

            this._x = start.x;
            this._z = start.z;
            this._destination = start;

        }

        public virtual void Flipbusy()
        {
            _busy = !(_busy);
        }

        public void Changespeed(double speed)
        {
            this.speed = speed;
        }

        public virtual void Move(double x, double y, double z) {
            this._x = x;
            this._y = y;
            this._z = z;

            needsUpdate = false;
        }

        public bool CheckMove()
        {
            return (moving);
        }

        public virtual void Rotate(double rotationX, double rotationY, double rotationZ) {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = false;
        }

        /// <summary>
        /// De update functie word elke vijftig ticks uitgevoerd. De update functie voert verschillende functies uit die vaak geupdate moeten worden en returned daarna true om te laten zien dat hij alles geupdate heeft.
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public virtual bool Update(int tick)
        {
            if(needsUpdate) {
                Moving();
                ShelfInteractie();
                Route();
                MoveShelf();               
                if (!(_queueroute==null))
                {
                    Queueroute(_queueroute);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verplaatst het shelf object in deze robot telkens naar iets boven de robot zijn positie.
        /// </summary>
        public virtual void MoveShelf()
        {
            if(checkshelf)
            {
                shelf.Move(_x, (_y + 2.3), _z);
            }
        }

        //Verandert de huidige route variabele.
        public virtual void Changeroute(List<Node> route)
        {
            this._route = route;
            onroute = true;
            needsUpdate = true;
        }

        /// <summary>
        /// Slaat een route op en zet deze pas in de huidige route als de robot niet meer een route volgt.
        /// </summary>
        /// <param name="route"></param>
        public virtual void Queueroute(List<Node> route)
        {
            this._queueroute = route;
            if(!(onroute) && !(moving))
            {
                Changeroute(route);
                this._queueroute = null;
            }
        }

        /// <summary>
        /// Deze functie runt telkens en checkt of de robot net is gaan stil staan op een node met een shelf.
        /// Als dit zo is neemt dit de robot mee.
        /// Als dit niet zo is checkt hij of hij aan het einde van een route stilstaat op een dropoff point.
        /// Als dit zo is dropt hij hier zijn shelf.
        /// </summary>
        public virtual void ShelfInteractie()
        {
            if (!(moving))
            {
                if (checkshelf)
                {
                    if (_destination.dropoff && !busy)
                    {
                        shelf.Move(0, 10000, 0);
                        shelf = null;
                        _justdropped = true;
                    }
                }

                if (!(onroute))
                {

                    if(_destination.stash)
                    {

                        if(!(_destination.checkshelf))
                        {                           
                            _destination.PushShelf(PopShelf());
                            Flipbusy();
                        }

                        else if (_destination.checkshelf)
                        {
                            PushShelf(_destination.PopShelf());

                        }


                    }
                    
                }
            }
        }

        /// <summary>
        /// Als de robot onroute is en stilstaat word de destination verandert naar de volgende node in de route lijst.
        /// </summary>z
        public virtual void Route()
        {            
            if (onroute)
            {
                if(!moving)
                {
                    _destination = _route.Last();
                        Changedes(_destination.x, 0, _destination.z);
                        _route.RemoveAt(_route.Count()-1);
                    if(_route.Count()==0)
                    {

                        onroute = false;
                    }

                }
            }

        }
        
        public virtual void Changedes(double xdes, double ydes, double zdes)
        {
            this._tx = xdes;
            this._ty = ydes;
            this._tz = zdes;
            moving = true;
            needsUpdate = true;
        }
        /// <summary>
        /// Beweegt de robot op basis van zijn speed per 50 ticks naar de targetX en of targetZ.
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
                    moving = false;
                    needsUpdate = false;
                }
            }
        }

        public Shelf PopShelf()
        {
            Shelf shelf2 = shelf;
            shelf = null;
            return shelf2;
        }
        
        public void PushShelf(Shelf shelf)
        {
            this.shelf = shelf;
        }

        public void FlipDropped()
        {
            _justdropped = !(justdropped);
        }

    }
}