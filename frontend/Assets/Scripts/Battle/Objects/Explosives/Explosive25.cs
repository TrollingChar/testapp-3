using UnityEngine;

namespace Battle.Objects.Explosives {
    public class Explosive25 : Explosive
    {
        protected override void OnDetonate() {
            Debug.Log("типа взрыв, средний, урон 25");
        }
    }
}