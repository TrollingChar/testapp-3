using UnityEngine;


namespace Battle.Water {

    public class WavesGO : MonoBehaviour {

        private float    _wind;
        private float    _initialWaves;
        private float    _initialSpeed;
        private float    _initialScale;
        private Material _material;


        private void Awake () {
            _material = GetComponent<SpriteRenderer>().material;
            // считаем что ветер 10
            _wind         = 10;
            _initialScale = transform.localScale.y * 0.1f;
            _initialWaves = _material.GetFloat("_Waves") * 10f;
            _initialSpeed = _material.GetFloat("_Wind");
        }
        
        
        public float Wind {
            get { return _wind; }
            set {
                _wind = value;
                var scale = transform.localScale;
                scale.y              = Mathf.Abs(Wind) * _initialScale;
                transform.localScale = scale;
                _material.SetFloat("_Wind",  Wind < 0 ? -_initialSpeed : _initialSpeed);
                _material.SetFloat("_Waves", _initialWaves / Mathf.Abs(Wind));
            }
        }

    }

}