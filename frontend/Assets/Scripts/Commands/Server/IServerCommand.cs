using System;
using Net.Utils.IO;


namespace Commands.Server {

    public interface IServerCommand {
        
        void Deserialize (EndianBinaryReader reader);
        void Execute (); // maybe use static Messenger for each command instead of CommandExecutor class

    }

}
