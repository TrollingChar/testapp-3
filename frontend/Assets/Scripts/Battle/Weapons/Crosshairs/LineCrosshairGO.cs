using UnityEngine;


namespace Battle.Weapons.Crosshairs {

    public class LineCrosshairGO : MonoBehaviour {

        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private GameObject _ringPrefab;
        [SerializeField] private float startXOffset = 250;
        [SerializeField] private float totalXOffset = 1250;
        [SerializeField] private int _points;
        private GameObject _ring;


        private void Awake () {
            _ring = Instantiate(_ringPrefab, transform);
            for (int i = 0; i < _points; i++) {
                Instantiate(
                    _pointPrefab,
                    new Vector2(Mathf.LerpUnclamped(startXOffset, totalXOffset, (float) i / (_points - 1)), 0),
                    Quaternion.identity,
                    transform
                );
            }
        }


        public bool RingVisible {
            set { _ring.SetActive(value); }
        }


        public float RingPosition {
            set {
                var pos = _ring.transform.position;
                pos.x = Mathf.LerpUnclamped(startXOffset, totalXOffset, value);
            }
        }

    }

}
