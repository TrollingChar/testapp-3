using System.IO;


namespace DataTransfer {

    public interface IDTO { // for structs that can't inherit from classes

        void ReadMembers (BinaryReader reader);
        void WriteMembers (BinaryWriter reader);

    }

}
