using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Node
    {

        private string naam;
        private int x;
        private int z;

        public Node (string naam, int x, int z)
        {
            this.naam = naam;
            this.x = x;
            this.z = z;
        }

        public string GetNaam()
        {
            return naam;
        }
        public int GetX()
        {
            return x;
        }
        public int GetZ()
        {
            return z;
        }
        



    }
}
