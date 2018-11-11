using System.Collections.Generic;
using System.Linq;
using Battle.Objects;
using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Collision = Collisions.Collision;
using Object = Battle.Objects.Object;


namespace Battle {

    public partial class World {

        public List <Object> Objects;


        private void PhysicsTick (TurnData td) {
            foreach (var o in Objects.Where (o => !o.Despawned)) o.PhysicsBegin ();

            for (int i = 0, iter = 5; i < iter; i++)
            for (int j = 0; j < Objects.Count; j++) {
                var o = Objects[j];
                if (o.Despawned) continue;
                o.PhysicsUpdate (i >= iter - 2);
            }
            
            Objects.RemoveAll (o => o.Despawned);
            foreach (var o in Objects) o.PhysicsEnd ();
        }

    }

}