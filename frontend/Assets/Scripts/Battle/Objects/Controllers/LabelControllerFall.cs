using DataTransfer.Data;
using UnityEngine;


namespace Battle.Objects.Controllers {

    public class LabelControllerFall : Controller {

        private float _time;


        public LabelControllerFall (float time = 1f) {
            _time = time;
        }


        private float Time {
            get { return _time; }
            set {
                _time = value;
                if (value <= 0) {
                    Object.Remove();
                    return;
                }
                float size = Mathf.Min(value, 0.5f);
                Object.GameObject.transform.localScale = new Vector3(size * 2f, size * 2f, 1);
            }
        }


        public override void OnAdd () {
            Time = _time;
        }


        protected override void DoUpdate (TurnData td) {
            Object.Velocity.Y -= 0.33f;
            Time -= 1f / Core.Time.TPS;
        }

    }

}
