using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using AmazonSimulator_VS;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<Object> worldObjects = new List<Object>();
        private List<Node> nodes = new List<Node>();
        private List<Robot> robots = new List<Robot>();

        private List<IObserver<Command>> observers = new List<IObserver<Command>>();

        private bool OphaalEvent = false;

        private Truck t = null;
        Graph g = new Graph();
        private int GoalInv=0;

        public World() {

            

            Shelf ab = CreateShelf(12, 2.15, 10);
            Shelf cd = CreateShelf(15, 2.15, 10);
            Shelf de = CreateShelf(18, 2.15, 10);

            Shelf fg = CreateShelf(12, 2.15, 15);
            Shelf hj = CreateShelf(15, 2.15, 15);
            Shelf kl = CreateShelf(18, 2.15, 15);

            Shelf mn = CreateShelf(12, 2.15, 20);
            Shelf op = CreateShelf(15, 2.15, 20);
            Shelf qr = CreateShelf(18, 2.15, 20);

            


            
            Node A = CreateNode(19, 9);            
            Node B = CreateNode(19, 14);
            Node C = CreateNode(19, 19);
            Node D = CreateNode(19, 33);
            D.SetDropoff();

            Node E = CreateNode(10, 33);
            E.SetDropoff();
            Node F = CreateNode(10, 19);
            Node G = CreateNode(10, 14);
            Node H = CreateNode(10, 9);

            Node AH1 = CreateNode(18, 9);
            AH1.PushShelf(de);
            Node AH2 = CreateNode(15, 9);
            AH2.PushShelf(cd);
            Node AH3 = CreateNode(12, 9);
            AH3.PushShelf(ab);

            Node BG1 = CreateNode(18, 14);
            BG1.PushShelf(kl);
            Node BG2 = CreateNode(15, 14);
            BG2.PushShelf(hj);
            Node BG3 = CreateNode(12, 14);
            BG3.PushShelf(fg);

            Node CF1 = CreateNode(18, 19);
            CF1.PushShelf(qr);
            Node CF2 = CreateNode(15, 19);
            CF2.PushShelf(op);
            Node CF3 = CreateNode(12, 19);
            CF3.PushShelf(mn);


            g.Add_vertex(A, new Dictionary<Node, int>() { { B, 5 }, { AH1, 1 } });
            g.Add_vertex(B, new Dictionary<Node, int>() { { A, 5 }, { C, 5 }, { BG1, 1 } });
            g.Add_vertex(C, new Dictionary<Node, int>() { { B, 5 }, { D, 14 }, { CF1, 1 } });
            g.Add_vertex(D, new Dictionary<Node, int>() { { C, 11 } });

            g.Add_vertex(E, new Dictionary<Node, int>() { { F, 11 } });
            g.Add_vertex(F, new Dictionary<Node, int>() { { CF3, 2 }, { G, 5 }, { E, 14 } });
            g.Add_vertex(G, new Dictionary<Node, int>() { { F, 5 }, { BG3, 2 }, { H, 5 } });
            g.Add_vertex(H, new Dictionary<Node, int>() { { AH3, 2 }, { G, 5 } });

            g.Add_vertex(AH1, new Dictionary<Node, int>() { { A, 1 }, { AH2, 3 } });
            g.Add_vertex(AH2, new Dictionary<Node, int>() { { AH1, 3 }, { AH3, 3 } });
            g.Add_vertex(AH3, new Dictionary<Node, int>() { { H, 2 }, { AH2, 3 } });

            g.Add_vertex(BG1, new Dictionary<Node, int>() { { B, 1 }, { BG2, 3 } });
            g.Add_vertex(BG2, new Dictionary<Node, int>() { { BG1, 3 }, { BG3, 3 } });
            g.Add_vertex(BG3, new Dictionary<Node, int>() { { BG2, 3 }, { G, 2 } });

            g.Add_vertex(CF1, new Dictionary<Node, int>() { { C, 1 }, { CF2, 3 } });
            g.Add_vertex(CF2, new Dictionary<Node, int>() { { CF1, 3 }, { CF3, 3 } });
            g.Add_vertex(CF3, new Dictionary<Node, int>() { { CF2, 3 }, { F, 2 } });

            Robot a = CreateRobot(A);
            Robot b = CreateRobot(G);
            Robot c = CreateRobot(H);
            c.Changespeed(0.5);
            OphaalTruck();





        }


        /// <summary>
        /// Maakt een object r met als start positie de meegegeven node.
        /// Voegt deze vervolgens toe aan de lijsten worldobjects en robots.
        /// Returned deze robot.
        /// </summary>
        /// <param name="start"></param>
        /// <returns>r</returns>
        private Robot CreateRobot(Node start) {
            Robot r = new Robot(start);
            worldObjects.Add(r);
            robots.Add(r);
            return r;
        }

        /// <summary>
        /// Maakt een Node op de gegeven coords, voegt deze toe aan de lijst nodes en returned vervolgens de Node.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private Node CreateNode(int x, int z)
        {
            Node n = new Node(x, z);
            nodes.Add(n);
            return n;
        }

        /// <summary>
        /// Maakt een truck binnen de al eerder gedclareerde variabele t, voegt deze toe aan worldobjects en returned t daarna.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private Truck CreateTruck(double x, double y, double z)
        {
            t = new Truck(x, y, z, 0, 0, 0);
            worldObjects.Add(t);
            return t;
        }      

        /// <summary>
        /// Maakt een shelf op de gegeven coords, voegt de shelf toe aan worldobjects en returned vervolgens de shelf.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf s = new Shelf(x, y, z);
            worldObjects.Add(s);
            return s;
        }

        /// <summary>
        /// Zoekt recursief door de lijst nodes om een node te vinden en returnen waar robots shelves neer mogen zetten. 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Node GetDropoffLocation(int e)
        {
            int i = e;
            Node n = nodes[i];
            if(n.dropoff)
            {
                return n;
            }
            else
            {
                return GetDropoffLocation(i + 1);
            }
        }

        /// <summary>
        /// Zoekt recursief door de lijst nodes om een node te vinden en returnen waar robots shelves kunnen ophalen. 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Node GetShelfLocation(int e)
        {
            int i = e;
            Node n = nodes[i];
            if (n.checkshelf && !(n.target))
            {
                n.FlipTarget();
                GoalInv += 1;
                return n;
            }
            else
            {
                return GetShelfLocation(i + 1);
            }
        }

        /// <summary>
        /// Stuurt de meegegeven robot naar een node met een shelf en zet alvast de route naar de dropoff node in de robots queue.
        /// </summary>
        /// <param name="r"></param>
        public void TakeShelfToDropoff(Robot r)
        {
                Node n = GetShelfLocation(0);
                r.Changeroute(g.Shortest_path(r.destination, n));
                r.Queueroute(g.Shortest_path(n, GetDropoffLocation(0)));
        }

        /// <summary>
        /// Start het ophaaltruck event. De bijbehorende variabelen worden goedgezet en alle robots worden naar de TakeShelfToDropoff functie gestuurd.
        /// </summary>
        private void OphaalTruck()
        {
            OphaalEvent = true;
            GoalInv = 0;
            t = CreateTruck(-30, 0, 33);
            t.Changedes(25, 0, 33);
            foreach (Robot r in robots)
            {
                TakeShelfToDropoff(r);
            }
        }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer)) {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c) {
            for(int i = 0; i < this.observers.Count; i++) {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs) {
            foreach(Object m3d in worldObjects) {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }

            
        }
        
        /// <summary>
        /// De update functie word elke vijftig ticks uitgevoerd. Deze functie voert code van huidige events uit en zorgt ervoor dat de visuele representaties van de worldobjects 
        /// geedit kan worden.
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public bool Update(int tick)
        {
            
            if (OphaalEvent)
            {                
                foreach (Robot r in robots)
                {
                    if (r.justdropped)
                    {                       
                        r.FlipDropped();
                        t.PlusInv();                       
                        if (!(GoalInv >= t.maxinv))
                        {
                            TakeShelfToDropoff(r);
                            
                        }
                        
                        if(t.inventory>=t.maxinv)
                        {
                            t.Kill();
                            OphaalEvent = false;
                            
                        }
                    }
                }
            }

            for (int i = 0; i < worldObjects.Count; i++) {
                Object u = worldObjects[i];
                if(u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if(needsCommand) {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }                        
            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() 
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}