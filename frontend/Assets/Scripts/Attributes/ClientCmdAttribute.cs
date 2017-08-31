using System;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class ClientCmdAttribute : IdAttribute {

        private readonly byte _id;


        public ClientCmdAttribute (byte id) {
            _id = id;
        }


        public override byte Id {
            get { return _id; }
        }

    }

}
