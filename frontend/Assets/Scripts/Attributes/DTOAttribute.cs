using System;
using DataTransfer;


namespace Attributes {

    public class DTOAttribute : Attribute {

        private readonly DTOCode _code;


        public DTOAttribute (DTOCode code) {
            _code = code;
        }


        public DTOCode Code {
            get { return _code; }
        }

    }

}
