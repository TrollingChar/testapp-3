namespace Geometry {

    public struct OffsetNormal {

        public XY Offset;
        public XY Normal;


        public OffsetNormal (XY offset, XY normal) {
            Offset = offset;
            Normal = normal;
        }


        public bool IsEmpty {
            get { return Normal.IsNaN; }
        }

    }

}
