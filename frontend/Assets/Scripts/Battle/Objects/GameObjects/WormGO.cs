using System;
using UnityEngine;


namespace Battle.Objects.GameObjects {

    [Obsolete]
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

            bool flipped = _sprite.localScale.x < 0;
            bool deltaTooBig = Mathf.Abs(Mathf.DeltaAngle(angle, flipped ? 180 : 0)) > 90;
            _head.localEulerAngles = new Vector3(
                0,
                0,
                Mathf.LerpAngle(0, (deltaTooBig ? 180 : 0) + (flipped ? 180 - angle : angle), 0.5f)
            );
            _headRenderer.flipX = deltaTooBig;
        }

    }

}
