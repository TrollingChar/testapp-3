using System;
using War.Generation;

// ReSharper disable DelegateSubtraction


namespace Utils.Messenger {

    public class Messenger {

        private Action _handlers = () => {};


        public void Subscribe (Action handler) {
            _handlers += handler;
        }


        public void Unsubscribe (Action handler) {
            _handlers -= handler;
        }


        public void Send () {
            _handlers.Invoke();
        }

    }


    public class Messenger<T> {

        private Action<T> _handlers = t => {};


        public void Subscribe (Action<T> handler) {
            _handlers += handler;
        }


        public void Unsubscribe (Action<T> handler) {
            _handlers -= handler;
        }


        public void Send (T t) {
            _handlers.Invoke(t);
        }

    }


    public class Messenger<T1, T2> {

        private Action<T1, T2> _handlers = (t1, t2) => {};


        public void Subscribe (Action<T1, T2> handler) {
            _handlers += handler;
        }


        public void Unsubscribe (Action<T1, T2> handler) {
            _handlers -= handler;
        }


        public void Send (T1 t1, T2 t2) {
            _handlers.Invoke(t1, t2);
        }

    }

}
