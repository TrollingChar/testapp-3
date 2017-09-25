using System;
using Commands.Client;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    [Obsolete]
    public class ClientCmdAttribute : IdAttribute {

        private readonly byte _id;


        public ClientCmdAttribute (ClientCmdId id) {
            _id = (byte) id;
        }


        public override byte Id {
            get { return _id; }
        }

    }

}
