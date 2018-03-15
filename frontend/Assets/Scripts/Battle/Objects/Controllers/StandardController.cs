using Battle.Objects.Effects;
using Core;
using DataTransfer.Data;
using UnityEngine;
using Utils.Random;


namespace Battle.Objects.Controllers {

    public class StandardController : Controller {
        
        /* todo:
         * gravity affection
         * wind affection
         * magnet affection
         * wait flag
         * rotation
         * smoke trail
        **/
        
        public float GravityCoeff = 1, WindCoeff, MagnetCoeff, SmokeSize;
        public bool WaitFlag, OrientationFlag;

        
        protected override void DoUpdate (TurnData td) {
//            Object.Velocity.X += The.World.Wind * WindCoeff;
            Object.Velocity.Y += The.World.Gravity * GravityCoeff;
            if (WaitFlag) {
                Wait();
            }
            if (OrientationFlag) {
                Object.GameObject.transform.localEulerAngles = new Vector3(0, 0, Object.Velocity.Angle * Mathf.Rad2Deg);
            }
            if (SmokeSize > 0) {
                Object.Spawn(
                    new Smoke((RNG.Float() + 0.5f) * SmokeSize),
                    Object.Position,
                    Object.Velocity.WithLength(-10)
                );
            }
        }

    }

}
