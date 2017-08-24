using System;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class ServerCommandAttribute : Attribute {

        private readonly byte _code;


        public ServerCommandAttribute (byte code) {
            _code = code;
        }


    }

}
