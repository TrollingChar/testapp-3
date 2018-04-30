using Geometry;


namespace Battle.Objects {

    public class Effector : Object {
        
        public override void ReceiveBlastWave (XY impulse) {}
        public override bool WillSink { get { return false; } }

    }

}
