using System;
using UnityEngine;


namespace Battle.Objects.GameObjects {

    public class NewWormGO : MonoBehaviour {

        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _tailTransform;
        
        private Animator _animator;
        
        private bool _headLocked; // если true то червяк смотрит прямо перед собой
        private bool _headLockedInternal;


        // сюда надо добавлять новые триггеры!
        private void ResetAllTriggers () {
            _animator.ResetTrigger ("HeadStill");
            _animator.ResetTrigger ("HeadIdle");
            _animator.ResetTrigger ("TailStill");
            _animator.ResetTrigger ("TailWalk");
            _animator.ResetTrigger ("BeforeJump");
            _animator.ResetTrigger ("Jump");
            _animator.ResetTrigger ("AfterJump");
            _animator.ResetTrigger ("Fall");
        }


        private void ResetTailTriggers () {
            _animator.ResetTrigger ("TailStill");
            _animator.ResetTrigger ("TailWalk");
            _animator.ResetTrigger ("BeforeJump");
            _animator.ResetTrigger ("AfterJump");
        }


        private void Awake () {
            _animator = GetComponent <Animator> ();
        }


        public void OnAdd (Worm worm) {
            FacesRight = worm.FacesRight;
            LockHead ();
        }


        public void SetWalking (bool value) {
            ResetTailTriggers ();
            _animator.SetTrigger (value ? "TailWalk" : "TailStill");
        }


        public bool FacesLeft {
            get {
                return transform.localScale.x < 0;
            }
            set {
                if (value) {
                    FaceLeft ();
                }
                else {
                    FaceRight ();
                }
            }
        }


        public bool FacesRight {
            get {
                return transform.localScale.x > 0;
            }
            set {
                if (value) {
                    FaceRight ();
                }
                else {
                    FaceLeft ();
                }
            }
        }


        public void FaceLeft () {
            if (FacesLeft) return;
            transform.localScale = new Vector3 (-1, 1, 1);
            
            if (_headLocked) return;
            _headTransform.localScale = new Vector3(-_headTransform.localScale.x, 1, 1);
            _headTransform.rotation = Quaternion.Inverse (_headTransform.rotation);
        }


        public void FaceRight () {
            if (FacesRight) return;
            transform.localScale = new Vector3 (1, 1, 1);
            
            if (_headLocked) return;
            _headTransform.localScale = new Vector3(-_headTransform.localScale.x, 1, 1);
            _headTransform.rotation = Quaternion.Inverse (_headTransform.rotation);
        }


        // червяк просто стоит и все
        public void Stand () {
            UnlockHeadInternal ();
            ResetAllTriggers ();
            _animator.SetTrigger ("HeadStill");
            _animator.SetTrigger ("TailStill");
            _tailTransform.gameObject.SetActive (true);
        }


        public void LockHead () {
            _headLocked = true;
            _headTransform.rotation = Quaternion.identity;
            _headTransform.localScale = Vector3.one;
        }


        public void UnlockHead () {
            _headLocked = false;
        }


        private void LockHeadInternal () {
            _headLockedInternal = true;
            _headTransform.rotation = Quaternion.identity;
            _headTransform.localScale = Vector3.one;
        }


        private void UnlockHeadInternal () {
            _headLockedInternal = false;
        }


        public void SetHeadAngle (float radians) {
            if (_headLocked || _headLockedInternal) return;

            bool looksForward = _headTransform.localScale.x > 0;

            if (FacesRight ^ looksForward) {
                radians = radians < 0 ? -Mathf.PI - radians : Mathf.PI - radians;
            }

            // теперь у нас угол относительно положения червяка когда голова смотрит вправо

//            if (Mathf.Abs (radians) * Mathf.Rad2Deg > 100) {
            if (Mathf.Abs (radians) * 2 > Mathf.PI) {
                // надо повернуть голову значит
                _headTransform.localScale = new Vector3 (-_headTransform.localScale.x, 1, 1);
                looksForward = !looksForward;
                radians = radians < 0 ? -Mathf.PI - radians : Mathf.PI - radians;
            }

            // 100 должно перейти в 90
//            float sin = Mathf.Sin (radians * 0.9f);
            
            float sin = Mathf.Sin (radians);

            float degrees = sin * 45;
            _headTransform.rotation = Quaternion.Euler (0, 0, FacesRight ^ looksForward ? -degrees : degrees);
        }


        // перед прыжком
        public void PrepareJump () {
            LockHeadInternal ();
            ResetAllTriggers ();
            _animator.SetTrigger ("BeforeJump");
            _tailTransform.gameObject.SetActive (true);
        }


        // сам прыжок
        public void Jump () {
            LockHeadInternal ();
            ResetAllTriggers ();
            _animator.SetTrigger ("Jump");
            _tailTransform.gameObject.SetActive (false);
        }


        // когда приземлился после прыжка
        public void Land () {
            LockHeadInternal ();
            ResetAllTriggers ();
            _animator.SetTrigger ("AfterJump");
            _tailTransform.gameObject.SetActive (true);
        }


        // после удара или когда сносит взрывом
        public void Fall () {
            LockHeadInternal ();
            ResetAllTriggers ();
            _animator.SetTrigger ("Fall");
            _tailTransform.gameObject.SetActive (false);
        }


        // встать после падения
        public void Recover () {
            throw new NotImplementedException();
        }


        private void Update () {
            if (Input.GetKeyDown (KeyCode.B)) {
                Debug.Log ("b");
                Fall ();
                SetWalking (false);
            }
        }

    }

}