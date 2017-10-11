using UnityEngine;

namespace Battle.Objects.Explosives {
    public class Explosive10 : Explosive {
        protected override void OnDetonate()
        {
            Debug.Log("типа взрыв, маленький, урон 10");
        }
    }
}