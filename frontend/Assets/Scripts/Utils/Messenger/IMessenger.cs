using System;


namespace Utils.Messenger {

    public interface IMessenger {

        void Subscribe (Action handler);
        void Unsubscribe (Action handler);
        void Send ();

    }

    public interface IMessenger<T> {

        void Subscribe (Action<T> handler);
        void Unsubscribe (Action<T> handler);
        void Send (T t);

    }

}
