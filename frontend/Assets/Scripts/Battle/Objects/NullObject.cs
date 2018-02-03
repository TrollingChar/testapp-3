using UnityEngine;


namespace Battle.Objects {

    internal class NullObject : Object {

        public override void OnRemove () {
            Debug.LogError("attempt to remove null object");
        }

    }

}
