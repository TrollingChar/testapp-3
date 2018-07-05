using System;
using Geometry;
using UnityEngine;


namespace Battle.Objects.GameObjects {

    public class NewWormGO : MonoBehaviour {

        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform          _headTransform;
        [SerializeField] private Transform          _tailTransform;
        
        private Animator _animator;
        
        private bool _headLocked; // если true то червяк смотрит прямо перед собой
        
        
        // итак, чо ваще
        // наверно стоит выделить состояния червяка
        // 1 бездействие - червяк стоит и смотрит вперед
        // 2 смотрит в какую-то точку
        // 3 готовится к прыжку
        // 4 прыгает - тут хвост типа не видно, а спрайт головы идет с хвостом
        // 5 после прыжка


        private void Awake () {
            _animator = GetComponent <Animator> ();
        }


        public void OnAdd (Worm worm) {
            FacesRight = worm.FacesRight;
            LockHead ();
        }


//        private void Update () {
//            if (Input.GetKeyDown (KeyCode.LeftArrow)) FaceLeft ();
//            if (Input.GetKeyDown (KeyCode.RightArrow)) FaceRight ();

//            if (Input.GetKeyDown (KeyCode.S)) Stand ();
            
//            if (Input.GetKeyDown (KeyCode.L)) LockHead ();
//            if (Input.GetKeyDown (KeyCode.U)) UnlockHead ();
            
//            if (Input.GetKeyDown (KeyCode.P)) PrepareJump ();
//            if (Input.GetKeyDown (KeyCode.LeftBracket)) Jump ();
//            if (Input.GetKeyDown (KeyCode.RightBracket)) Land ();
            
//            SetWalking (Input.GetKey (KeyCode.W));

//            Vector2 point = _camera.ScreenToWorldPoint (Input.mousePosition);
//            SetHeadAngle (Mathf.Atan2 (point.y, point.x), false);
//        }


        public void SetWalking (bool value) {
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
            _animator.SetTrigger ("HeadStill");
            _animator.SetTrigger ("TailStill");
        }


        // анимация бездействия
        public void Idle1 () {}


        public void LockHead () {
            _headLocked = true;
            _headTransform.rotation = Quaternion.identity;
            _headTransform.localScale = Vector3.one;
        }


        public void UnlockHead () {
            _headLocked = false;
        }


        public void SetHeadAngle (float radians, bool forceUnlock) {
            if (forceUnlock) UnlockHead ();
            if (_headLocked) return;

            bool looksForward = _headTransform.localScale.x > 0;

            if (FacesRight ^ looksForward) {
                radians = radians < 0 ? -Mathf.PI - radians : Mathf.PI - radians;
            }

            // теперь у нас угол относительно положения червяка когда голова смотрит вправо

            if (Mathf.Abs (radians) * Mathf.Rad2Deg > 100) {
                // надо повернуть голову значит
                _headTransform.localScale = new Vector3 (-_headTransform.localScale.x, 1, 1);
                looksForward = !looksForward;
                radians = radians < 0 ? -Mathf.PI - radians : Mathf.PI - radians;
            }

            // 100 должно перейти в 90
            float sin = Mathf.Sin (radians * 0.9f);

            float degrees = sin * 45;
            if (FacesRight ^ looksForward) {
                degrees = -degrees;
            }
            _headTransform.rotation = Quaternion.Euler (0, 0, degrees);
        }


        // перед прыжком
        public void PrepareJump () {
            _animator.SetTrigger ("BeforeJump");
        }


        // сам прыжок
        public void Jump () {
            _animator.SetTrigger ("Jump");
        }


        // когда приземлился после прыжка
        public void Land () {
            _animator.SetTrigger ("AfterJump");
        }


        // после удара или когда сносит взрывом
        public void Fall () {
            throw new NotImplementedException();
        }


        // встать после падения
        public void Recover () {
            throw new NotImplementedException();
        }

    }

}