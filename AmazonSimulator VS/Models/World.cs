using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using AmazonSimulator_VS;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<Object> worldObjects = new List<Object>();

        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        
<<<<<<< HEAD
        public World() {

            Shelf ab = CreateShelf(14, 2.15, 15);
            Shelf cd = CreateShelf(15, 2.15, 15);
            Shelf de = CreateShelf(16, 2.15, 15);

            Robot a = CreateRobot(2, 0.05, 0);
            a.Changedes(20, 0, 20);

            Graph g = new Graph();
            Node A = new Node("A", 2, 0);
            Node B = new Node("B", 2, 1);
            Node C = new Node("C", 4, 1);
            Node D = new Node("D", 4, 0);
            g.Add_vertex(A, new Dictionary<Node, int>() { { B, 1 }, { D, 2 } });

            g.Add_vertex(B, new Dictionary<Node, int>() { { A, 1 }, { C, 2 } });

            g.Add_vertex(C, new Dictionary<Node, int>() { { B, 2 }, { D, 1 } });
=======
        public World() {   
            Robot a = CreateRobot(1, 0, 1);
            Robot b = CreateRobot(0, 0, 1);
            Robot c = CreateRobot(3, 0,1);
            Robot d = CreateRobot(4, 0, 1);
            Robot e = CreateRobot(5, 0, 1);
            Robot f = CreateRobot(6, 0, 1);
            a.Changedes(20, 0, 2);
            b.Changedes(40, 8, 2);

        
            Truck truck1 = CreateTruck(0, 0,0);
            truck1.Move(-30, 0, 33);
            truck1.Changedes(25, 0, 0);
            //stellingen, moet nog even in een loop

            Shelf shelf1 = CreateShelf(5, 2.5, 10);
            Shelf shelf2 = CreateShelf(5, 2.5, 12);
            Shelf shelf3 = CreateShelf(5, 2.5, 14);
            Shelf shelf4 = CreateShelf(5, 2.5, 16);
            Shelf shelf5 = CreateShelf(5, 2.5, 18);
            Shelf shelf6 = CreateShelf(5, 2.5, 20);
            Shelf shelf7 = CreateShelf(5, 2.5, 22);
            Shelf shelf8 = CreateShelf(5, 2.5, 24);
>>>>>>> MergeTest

            g.Add_vertex(D, new Dictionary<Node, int>() { { C, 1 }, { A, 2 } });


            List<Node> route = g.Shortest_path(A, D);
            Node test = route[0];
            Console.WriteLine(test.GetNaam());
            foreach (Node punt in route)


            {
                //a.Changedes(punt.GetX(), 0.05, 0);
               
            }



            //DijkstraMove(g.Shortest_path("A", "B"), a);


            //g.Shortest_path("A", "D").ForEach(x => Console.WriteLine(x));
        }

        //private void DijkstraMove(List<string> lijst, Robot robocop)
        //{
        //    
        //}
        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }

        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf s = new Shelf(x, y, z);
            worldObjects.Add(s);
            return s;


        }

        private Truck CreateTruck(double x, double y, double z)
        {
            Truck t = new Truck(x, y, z,0,0,0);
            worldObjects.Add(t);
            return t;


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