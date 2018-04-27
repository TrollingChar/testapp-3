using DataTransfer.Data;
using Geometry;
using UnityEngine;


namespace Battle.Objects.Controllers {

    public class LabelControllerRise : Controller {
        
        // если тут 0.5 то расстояние будет 2*v
        // если 0.1 то 10*v
        // если 0.05 то 20*v
        public const float InvLerpCoeff = 10;

        // todo: логику изменения размера надписи вынести в базовый класс
        private float _time;


        public LabelControllerRise (float time = 1.5f) {
            _time = time;
        }


        private float Time {
            get { return _time; }
            set {
                _time = value;
                if (value <= 0) {
                    Object.Despawn();
                    return;
                }
                float size = Mathf.Min(value, 0.25f);
                Object.GameObject.transform.localScale = new Vector3(size * 4f, size * 4f, 1);
            }
        }


        public override void OnAdd () {
            Time = _time;
        }


        protected override void DoUpdate (TurnData td) {
            Object.Velocity = XY.Lerp(Object.Velocity, XY.Zero, 1f / InvLerpCoeff);
            Time -= 1f / Core.Time.TPS;
        }

        

    }

}
