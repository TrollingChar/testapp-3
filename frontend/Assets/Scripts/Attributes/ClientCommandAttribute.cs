using System;


namespace Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class ClientCommandAttribute : Attribute {

        private readonly byte _code;


        public ClientCommandAttribute (byte code) {
            _code = code;
        }

    }

}
