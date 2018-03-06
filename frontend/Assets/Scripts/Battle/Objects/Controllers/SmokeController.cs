using DataTransfer.Data;
using Geometry;
using UnityEngine;


namespace Battle.Objects.Controllers {

    public class SmokeController : Controller {

        // если тут 0.5 то расстояние будет 2*v
        // если 0.1 то 10*v
        // если 0.05 то 20*v
        public const float InvLerpCoeff = 10;
        private float _size;


        public SmokeController (float size) {
            _size = size;
        }


        public override void OnAdd () {
            Size = _size;
        }


        private float Size {
            get { return _size; }
            set {
                _size = value;
                if (value <= 0) {
                    Object.Remove();
                    return;
                }
                Object.GameObject.transform.localScale = new Vector3(value, value, 1);
            }
        }


        protected override void DoUpdate (TurnData td) {
            Object.Velocity = XY.Lerp(Object.Velocity, new XY(0, 0.5f), 1 / InvLerpCoeff);
            Size -= 1f;
        }

    }

}
