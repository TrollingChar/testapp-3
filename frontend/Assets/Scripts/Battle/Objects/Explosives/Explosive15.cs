using UnityEngine;


namespace Battle.Objects.Explosives {

    public class Explosive15 : Explosive {

        protected override void OnDetonate () {
            Debug.Log("типа взрыв, небольшой, урон 15");
        }

    }

}
