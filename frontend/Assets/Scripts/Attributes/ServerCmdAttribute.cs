using System;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class ServerCmdAttribute : IdAttribute {

        private readonly byte _id;


        public ServerCmdAttribute (byte id) {
            _id = id;
        }


        public override byte Id {
            get { return _id; }
        }

    }

}
