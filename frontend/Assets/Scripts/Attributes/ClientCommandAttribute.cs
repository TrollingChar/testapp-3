using System;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class ClientCommandAttribute : Attribute {

        public readonly byte Id;


        public ClientCommandAttribute (byte id) {
            Id = id;
        }


        public ClientCommandAttribute (int id) {
            Id = (byte) id;
        }

    }

}
