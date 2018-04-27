using Battle.Objects.Effects;
using Battle.Objects.Projectiles;
using Core;
using DataTransfer.Data;
using Geometry;
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
        public bool WaitFlag, OrientationFlag, GasFlag;

        
        protected override void DoUpdate (TurnData td) {
            Object.Velocity.X += The.World.Wind * WindCoeff;
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
                    Object.Velocity.WithLength(-10f)
                );
            }
            if (GasFlag) {
                const float radius = 30f;
                for (int i = 0; i < 2; i++) {
                    float v = 1 - RNG.Float() * RNG.Float() * RNG.Float();
                    Object.Spawn(
                        new PoisonGas(RNG.Float() * 3),
                        //                    new Smoke(RNG.Float() * 40), 
                        Object.Position,
                        XY.FromPolar(
                            v * radius * 2 / PoisonGasController.InvLerpCoeff,
                            RNG.Float() * 2 * Mathf.PI
                        )
                    );
                }
                
                // todo: дубликат кода да и вообще все что касается рандомного спавна вынести куда-то в одно место
            }
        }

    }

}
