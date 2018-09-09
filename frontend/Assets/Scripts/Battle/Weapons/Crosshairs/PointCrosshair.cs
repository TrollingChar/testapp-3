using System;
using UnityEngine;


namespace Battle.Weapons.Crosshairs {

    public class PointCrosshair : MonoBehaviour {

        public enum Direction { None, Left, Right }


        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private float _width;
        [SerializeField] private int _arrowCount = 4;
        [SerializeField] private float _speed = 10f;
        
        private GameObject[] _arrows;
        private SpriteRenderer[] _renderers;
        private float _x;
        private float _currentSpeed;


        private void Awake () {
            _arrows = new GameObject[_arrowCount];
            _renderers = new SpriteRenderer[_arrowCount];
            for (int i = 0; i < _arrowCount; i++) {
                _arrows[i] = Instantiate(_arrowPrefab, transform, false);
                _renderers[i] = _arrows[i].GetComponentInChildren<SpriteRenderer>();
            }
            Type = Direction.None;
        }


        public void Update () {
            float arrowOffset = _width / _arrowCount;
            _x = Mathf.Repeat(_x + _currentSpeed * Time.deltaTime, arrowOffset);
            for (int i = 0; i < _arrowCount; i++) {
                float x = _x + i * arrowOffset - _width * 0.5f;
                _arrows[i].transform.localPosition = new Vector3(x, 0, 0);
                float scale = Mathf.Clamp01((_width * 0.5f - Mathf.Abs(x)) / arrowOffset);
                _arrows[i].transform.localScale = new Vector3(scale, scale, 1f);
            }
        }


        public Direction Type {
            set {
                switch (value) {
                    case Direction.None:
                        for (int i = 0; i < _arrowCount; i++) _arrows[i].SetActive(false);
                        _currentSpeed = 0f;
                        break;
                    case Direction.Left:
                        for (int i = 0; i < _arrowCount; i++) {
                            _arrows[i].SetActive(true);
                            _renderers[i].flipX = true;
                            _currentSpeed = -_speed;
                        }
                        break;
                    case Direction.Right:
                        for (int i = 0; i < _arrowCount; i++) {
                            _arrows[i].SetActive(true);
                            _renderers[i].flipX = false;
                            _currentSpeed = _speed;
                        }
                        break;
                    default: throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
        }

    }

}
