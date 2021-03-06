﻿using UnityEngine;


namespace Battle.Weapons.Crosshairs {

    public class LineCrosshair : MonoBehaviour {

        // for launchers, grenades, guns - aimed weapons
        // o o o(o)o

        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private GameObject _ringPrefab;
        private GameObject _ring;
        [SerializeField] private float _firstPointOffset;
        [SerializeField] private float _lastPointOffset;
        [SerializeField] private int _points;


        private void Awake () {
            for (int i = 0; i < _points; i++) {
                float x = Mathf.LerpUnclamped(_firstPointOffset, _lastPointOffset, i / (_points - 1f));
                var point = Instantiate(_pointPrefab, new Vector3(x, 0, 0), Quaternion.identity);
                point.transform.SetParent(transform, false);
            }
            _ring = Instantiate(_ringPrefab, new Vector3(_firstPointOffset, 0, 0), Quaternion.identity);
            _ring.transform.SetParent(transform, false);
            _ring.SetActive(false); // not visible by default
        }


        public bool RingVisible {
            set { _ring.SetActive(value); }
        }


        public float RingPosition {
            set {
                var position = _ring.transform.localPosition;
                position.x = Mathf.LerpUnclamped(_firstPointOffset, _lastPointOffset, value);
                _ring.transform.localPosition = position;
            }
        }


        public float Angle {
            set {
                var eulerAngles = new Vector3(0, 0, value * Mathf.Rad2Deg);
                gameObject.transform.localRotation = Quaternion.Euler(eulerAngles);
            }
        }

    }

}
