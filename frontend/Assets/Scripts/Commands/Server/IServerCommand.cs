using System;
using Net.Utils.IO;


namespace Commands.Server {

    public interface IServerCommand {
        
        void Deserialize (EndianBinaryReader reader);
        void Execute ();

    }

}
