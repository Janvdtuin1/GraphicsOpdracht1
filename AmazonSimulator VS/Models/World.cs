using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using AmazonSimulator_VS;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<Object> worldObjects = new List<Object>();
        private List<Node> Nodes = new List<Node>();

        private List<IObserver<Command>> observers = new List<IObserver<Command>>();

        Graph g = new Graph();

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

            Robot a = CreateRobot(19, 0.05, 9);


            
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
            AH1.PushShelf(ab);
            Node AH2 = CreateNode(15, 9);
            AH2.PushShelf(cd);
            Node AH3 = CreateNode(12, 9);
            AH3.PushShelf(de);

            Node BG1 = CreateNode(18, 14);
            BG1.PushShelf(fg);
            Node BG2 = CreateNode(15, 14);
            BG2.PushShelf(hj);
            Node BG3 = CreateNode(12, 14);
            BG3.PushShelf(kl);

            Node CF1 = CreateNode(18, 19);
            CF1.PushShelf(mn);
            Node CF2 = CreateNode(15, 19);
            CF2.PushShelf(op);
            Node CF3 = CreateNode(12, 19);
            CF3.PushShelf(qr);


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


            OphaalTruck();


           

            
        }



        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }

        private Node CreateNode(int x, int z)
        {
            Node n = new Node(x, z);
            Nodes.Add(n);
            return n;
        }

        private Truck CreateTruck(double x, double y, double z)
        {
            Truck t = new Truck(x, y, z, 0, 0, 0);
            worldObjects.Add(t);
            return t;
        }

        private void OphaalTruck()
        {
            //Truck trucklolxdkekhaha=CreateTruck(0, 0, 0);
            //trucklolxdkekhaha.Move(-30, 0, 33);
            //trucklolxdkekhaha.Changedes(25, 0, 0);

            Truck t = CreateTruck(-30, 0, 33);
            t.Changedes(25, 0, 33);
            foreach(Robot r in worldObjects)
            {
                r.Changeroute(g.Shortest_path(GetDropoffLocation(0));
            }
        }

        private Node GetDropoffLocation(int e)
        {
            int i = e;
            Node n = Nodes[i];
            if(n.CheckDropoff())
            {
                return n;
            }
            else
            {
                return GetDropoffLocation(i + 1);
            }
        }

        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf s = new Shelf(x, y, z);
            worldObjects.Add(s);
            return s;


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
        
        public bool Update(int tick)
        {        
            for(int i = 0; i < worldObjects.Count; i++) {
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