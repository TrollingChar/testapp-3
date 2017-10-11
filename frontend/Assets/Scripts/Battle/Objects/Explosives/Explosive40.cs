using UnityEngine;

namespace Battle.Objects.Explosives {
    public class Explosive40 : Explosive {
        protected override void OnDetonate()
        {
            Debug.Log("типа взрыв, сильный, урон 40");
        }
    }
}