using UnityEngine;
using Utils;


namespace UI {

    public class CoreEvents : MonoBehaviour {

        public UnityEvent_string SetGameTime;
        public UnityEvent_string SetHint;

        // methods called from non-monobehaviour classes

        public UnityEvent_string SetTurnTime;

    }

}
