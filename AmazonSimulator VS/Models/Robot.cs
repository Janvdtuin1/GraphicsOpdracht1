using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AmazonSimulator_VS;
using Controllers;

namespace Models {
    public class Robot : BaseObjects
    {
        private List<Node> _route;
        private List<Node> _queueroute = null;
        private bool onroute = false;
        private bool _justdropped = false;
        private Shelf shelf=null;
        private Node _destination;
        private bool _busy = false;

        public bool route { get { return onroute; } }
        public bool busy { get { return _busy; } }

        public bool justdropped { get { return _justdropped; } }
        public bool checkshelf { get { return (!(shelf == null)); } }
        public Node destination { get { return _destination; } }

        public virtual void Flipbusy() { _busy = !(_busy); }
        public void Changespeed(double speed) { this.speed = speed; }
        public void FlipDropped() { _justdropped = !(justdropped); }
        public void PushShelf(Shelf shelf) { this.shelf = shelf; }

        public Robot(Node start) {
            this._type = "robot";
            this._guid = Guid.NewGuid();
            speed = 0.10;

            this._x = start.x;
            this._z = start.z;
            this._destination = start;

        }

        /// <summary>
        /// De update functie word elke vijftig ticks uitgevoerd. De update functie voert verschillende functies uit die vaak geupdate moeten worden en returned daarna true om te laten zien dat hij alles geupdate heeft.
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override bool Update(int tick)
        {
            if(needsUpdate) {
                Moving();
                ShelfInteractie();
                Route();
                if (checkshelf)
                {
                    shelf.Move(_x, (_y + 2.3), _z);
                }
                if (!(_queueroute==null))
                {
                    Queueroute(_queueroute);
                }
                return true;
            }
            return false;
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

                    if (_destination.stash)
                    {

                        if (!(_destination.checkshelf))
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

        public Shelf PopShelf()
        {
            Shelf shelf2 = shelf;
            shelf = null;
            return shelf2;
        }
    }
}