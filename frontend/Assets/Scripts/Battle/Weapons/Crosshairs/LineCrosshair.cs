using Assets;
using UnityEngine;
using Utils.Singleton;


namespace Battle.Weapons.Crosshairs {

    public class LineCrosshair : MonoBehaviour {

        // for launchers, grenades, guns - aimed weapons
        // o o o(o)o

        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private GameObject _ring;
        [SerializeField] private float _firstPointOffset;
        [SerializeField] private float _lastPointOffset;
        [SerializeField] private int _points;


        private void Awake () {
            for (int i = 0; i < _points; i++) {
                float x = Mathf.LerpUnclamped(_firstPointOffset, _lastPointOffset, i / (_points - 1f));
                Instantiate(_pointPrefab, new Vector3(x, 0, 0), Quaternion.identity, transform);
            }
        }


        public bool RingVisible {
            set { _ring.SetActive(value); }
        }

        public float RingValue {
            set {
                var position = _ring.transform.localPosition;
                position.x = Mathf.LerpUnclamped(_firstPointOffset, _lastPointOffset, value);
                _ring.transform.localPosition = position;
            }
        }

    }

}
