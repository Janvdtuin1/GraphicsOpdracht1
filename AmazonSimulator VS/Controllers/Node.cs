using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Node
    {
        private int x;
        private int z;
        private bool hasShelf=false;
        private bool isStash = false;
        private bool isDropoff = false;
        private Shelf shelf;
        private bool isTarget = false;

        public Node (int x, int z)
        {
            this.x = x;
            this.z = z;         
        }

        public int GetX()
        {
            return x;
        }
        public int GetZ()
        {
            return z;
        }
        
        public void PushShelf(Shelf shelf)
        {
            this.shelf = shelf;
            hasShelf = true;
            isStash = true;
        }
        public Shelf PopShelf()
        {
            Shelf shelf2 = shelf;
            hasShelf = false;
            shelf = null;
            isTarget = false;
            return shelf2;
        }
        public void SetDropoff()
        {
            isDropoff = true;
        }


        public bool CheckDropoff()
        {
            return isDropoff;
        }
        public bool CheckShelf()
        {
            return hasShelf;
        }
        public bool CheckStash()
        {
            return isStash;
        }

        public bool CheckTarget()
        {
            return isTarget;
        }
        public void SetTarget()
        {
            isTarget = !(isTarget);
        }

    }
}
