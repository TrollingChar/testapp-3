using Net;
using Net.Utils.IO;
using UnityEngine;
using Utils.Singleton;


namespace Commands.Client {

    public abstract class ClientCommand {

        public virtual void Serialize (EndianBinaryWriter writer) {}


        public void Send () {
            The<WSConnection>.Get().Send(this);
        }

    }

}
