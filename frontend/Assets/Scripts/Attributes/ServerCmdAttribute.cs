using System;
using Commands.Server;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class ServerCmdAttribute : IdAttribute {

        private readonly byte _id;


        public ServerCmdAttribute (ServerCmdId id) {
            _id = (byte) id;
        }


        public override byte Id {
            get { return _id; }
        }

    }

}
