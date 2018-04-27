using Geometry;
using UnityEngine;


namespace Battle.Objects {

    internal class NullObject : Object {

        public override void OnDespawn () {
            Debug.LogError("attempt to remove null object");
        }


        public override void ReceiveBlastWave (XY impulse) {}

    }

}
