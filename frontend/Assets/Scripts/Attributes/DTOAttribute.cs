using System;
using DataTransfer;


namespace Attributes {

    public class DTOAttribute : Attribute {

        public DTOCode Code {
            get { return _code; }
        }

        private readonly DTOCode _code;


        public DTOAttribute (DTOCode code) {
            _code = code;
        }

    }

}
