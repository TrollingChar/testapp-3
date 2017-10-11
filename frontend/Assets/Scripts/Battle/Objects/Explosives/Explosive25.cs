using UnityEngine;
using Utils.Singleton;

namespace Battle.Objects.Explosives {
    public class Explosive25 : Explosive
    {
        protected override void OnDetonate() {
            Debug.Log("типа взрыв, средний, урон 25");
            The<World>.Get().DealDamage(25, Object.Position, 80f);
        }
    }
}