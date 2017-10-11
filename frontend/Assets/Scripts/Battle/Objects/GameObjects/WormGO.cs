using UnityEngine;
using UnityEngine.UI;


namespace Battle.Objects.GameObjects {

    public class WormGO : MonoBehaviour {

        [SerializeField] private SpriteRenderer _headRenderer;
        [SerializeField] private SpriteRenderer _tailRenderer;
        private float _headAngle;

        [SerializeField] private Transform _sprite;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _tail;


        public bool FacesRight {
            set {
                if (_sprite.localScale.x < 0 ^ value) return;
                var v3 = _sprite.localScale;
                v3.x = -v3.x;
                _sprite.localScale = v3;

                Look(_headAngle);
            }
        }


        public void OnAdd (Worm worm) {
            FacesRight = worm.FacesRight;
            Look(worm.FacesRight ? 0 : 180);
        }


        public void Look (float angle) {
            _headAngle = angle;
            if (_sprite.localScale.x > 0) {
                if (Mathf.Abs(angle) < 90) {
                    _headRenderer.flipX = false;
                    _head.localEulerAngles = new Vector3(0, 0, angle * 0.5f);
                } else {
                    _headRenderer.flipX = true;
                    _head.localEulerAngles = new Vector3(0, 0, angle * 0.5f + (angle < 0 ? 90 : -90));
                }
            } else {
                if (Mathf.Abs(angle) > 90) {
                    _headRenderer.flipX = false;
                    _head.localEulerAngles = new Vector3(0, 0, angle * -0.5f - (angle < 0 ? 90 : -90));
                } else {
                    _headRenderer.flipX = true;
                    _head.localEulerAngles = new Vector3(0, 0, angle * -0.5f);
                }
            }
            
            
//            _headAngle = angle;
//            if (_sprite.localScale.x > 0) {
//                if (Mathf.Abs(angle) < 100) {
//                    _headRenderer.flipX = false;
//                    _head.localEulerAngles = new Vector3(0, 0, angle * 0.5f);
//                } else {
//                    _headRenderer.flipX = true;
//                    _head.localEulerAngles = new Vector3(0, 0, angle * 0.5f + (angle < 0 ? 90 : -90));
//                }
//            } else {
//                if (Mathf.Abs(angle) > 80) {
//                    _headRenderer.flipX = false;
//                    _head.localEulerAngles = new Vector3(0, 0, angle * -0.5f - (angle < 0 ? 90 : -90));
//                } else {
//                    _headRenderer.flipX = true;
//                    _head.localEulerAngles = new Vector3(0, 0, angle * -0.5f);
//                }
//            }
        }

    }

}
