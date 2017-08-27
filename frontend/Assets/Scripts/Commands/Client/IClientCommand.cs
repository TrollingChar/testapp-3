using Net.Utils.IO;


namespace Commands.Client {

    public interface IClientCommand {

        void Serialize (EndianBinaryWriter writer);

    }

}
