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
        
        public World() {
            //Robot a = CreateRobot(1, 0, 0);
            //Robot b = CreateRobot(0, 0, 0);
            //Robot c = CreateRobot(3, 0, 0);
            //Robot d = CreateRobot(4, 0, 0);
            //Robot e = CreateRobot(5, 0, 0);
            //Robot f = CreateRobot(6, 0, 0);

            Shelf ab = CreateShelf(1, 1, 1);
            Shelf cd = CreateShelf(2, 2, 1);
            Shelf de = CreateShelf(3, 1, 2);
            //a.Changedes(20, 0, 0);
            //b.Move(15, 0, 0);




            Robot a = CreateRobot(1, 0, 1);
            Robot b = CreateRobot(0, 0, 1);
            Robot c = CreateRobot(3, 0,1);
            Robot d = CreateRobot(4, 0, 1);
            Robot e = CreateRobot(5, 0, 1);
            Robot f = CreateRobot(6, 0, 1);
            a.Changedes(20, 0, 2);
            b.Move(15, 0, 2);

            Graph g = new Graph();
            g.Add_vertex('A', new Dictionary<char, int>() { { 'B', 7 }, { 'C', 8 } });
            g.Add_vertex('B', new Dictionary<char, int>() { { 'A', 7 }, { 'F', 2 } });
            g.Add_vertex('C', new Dictionary<char, int>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
            g.Add_vertex('D', new Dictionary<char, int>() { { 'F', 8 } });
            g.Add_vertex('E', new Dictionary<char, int>() { { 'H', 1 } });
            g.Add_vertex('F', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'G', 9 }, { 'H', 3 } });
            g.Add_vertex('G', new Dictionary<char, int>() { { 'C', 4 }, { 'F', 9 } });
            g.Add_vertex('H', new Dictionary<char, int>() { { 'E', 1 }, { 'F', 3 } });

            g.Shortest_path('A', 'H').ForEach(x => Console.WriteLine(x));
        }

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