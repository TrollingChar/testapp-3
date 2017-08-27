using System;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class ServerCommandAttribute : Attribute {

        public readonly byte Id;


        public ServerCommandAttribute (byte id) {
            Id = id;
        }


    }

}
