using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Node
    {
        private int _x;
        private int _z;        
        private bool _stash = false;
        private bool _dropoff = false;
        private Shelf shelf=null;
        private bool _target = false;


        public int x { get { return _x;} }
        public int z { get { return _z;} }
        public bool checkshelf { get{ return (!(shelf == null)); } }
        public bool stash { get { return (_stash); } }
        public bool dropoff { get { return (_dropoff); } }
        public bool target { get { return (_target); } }

        public Node (int x, int z)
        {
            this._x = x;
            this._z = z;        
        }
        
        /// <summary>
        /// Voegt een shelf toe aan de node en maakt van de node een stash.
        /// </summary>
        /// <param name="shelf"></param>
        public void PushShelf(Shelf shelf)
        {
            this.shelf = shelf;
            shelf.Move(shelf.x, 2.15, (z + 1));
            _target = false;
            _stash = true;
        }
        /// <summary>
        /// Maakt shelf null, zet target uit en returned de shelf zodat hij aan de robot toegevoegd kan worden.
        /// </summary>
        /// <returns></returns>
        public Shelf PopShelf()
        {
            Shelf shelf2 = shelf;
            _target = false;
            shelf = null;
            _stash = true;
            return shelf2;
        }
        public void SetDropoff()
        {
            _dropoff = true;
        }
        public void FlipTarget()
        {
            _target = !(_target);
        }

    }
}
