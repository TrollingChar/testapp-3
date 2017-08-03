using System;
// ReSharper disable DelegateSubtraction


namespace Utils.Messenger {

    public class Messenger : IMessenger {

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

    public class Messenger<T> : IMessenger<T> {

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

}
