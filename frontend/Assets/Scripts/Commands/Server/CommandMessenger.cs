using System;
using DataTransfer.Server;
using Utils.Messenger;


namespace Commands.Server {

    [Obsolete("use static messengers in commands instead")]
    public static class CommandExecutor<T> where T : ServerCommand {

        private static readonly Messenger<T> _onReceived = new Messenger<T>();


        public static void AddHandler (Action<T> handler) {
            _onReceived.Subscribe(handler);
        }


        public static void RemoveHandler (Action<T> handler) {
            _onReceived.Unsubscribe(handler);
        }


        public static void Execute (T cmd) {
            _onReceived.Send(cmd);
        }

    }

}
