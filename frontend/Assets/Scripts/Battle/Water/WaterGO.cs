using System;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable AssignmentInConditionalExpression


namespace Battle.Water {

    public class WaterGO : MonoBehaviour {

        [SerializeField] private GameObject _staticWaterPrefab;
        [SerializeField] private GameObject _frontWavesPrefab;
        [SerializeField] private GameObject _backWavesPrefab;

        [SerializeField] private float[] _backWavesOffsets;
        [SerializeField] private float[] _frontWavesOffsets;

        private float _wind;
        private readonly List<WavesGO> _waves = new List<WavesGO>();


        public void SetWind (float wind) {
            if (_wind == wind) return;
            _wind = wind;
            if (_waves == null) return;
            foreach (var wave in _waves) wave.Wind = wind;
        }


        private void MakeWaves (GameObject prefab, float yOffset, float phase, int layer) {
            var waves = Instantiate(prefab, transform);
            
            var position = waves.transform.localPosition;
            position.y = yOffset;
            waves.transform.position = position;
            
            var spriteRenderer = waves.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = layer;
            
            var material = spriteRenderer.material;
            material.SetFloat("_Offset", phase);

            _waves.Add(waves.AddComponent<WavesGO>());
        }


        private void Awake () {
            int layer = 0;
            float f = (Mathf.Sqrt(5) - 1) * 0.5f;
            foreach (float y in  _backWavesOffsets) MakeWaves( _backWavesPrefab, y, layer * f, ++layer);
            foreach (float y in _frontWavesOffsets) MakeWaves(_frontWavesPrefab, y, layer * f, ++layer);
        }

    }


    public class WavesGO : MonoBehaviour {

        private float _wind;
        private float _initialWaves;
        private float _initialSpeed;
        private float _initialScale;
        private Material _material;


        private void Awake () {
            _material = GetComponent<SpriteRenderer>().material;
            // считаем что ветер 10
            _wind = 10;
            _initialScale = transform.localScale.y * 0.1f;
            _initialWaves = _material.GetFloat("_Waves") * 10f;
            _initialSpeed = _material.GetFloat("_Wind");
        }
        
        
        public float Wind {
            get { return _wind; }
            set {
                _wind = value;
                var scale = transform.localScale;
                scale.y = Mathf.Abs(Wind) * _initialScale;
                transform.localScale = scale;
                _material.SetFloat("_Wind", Wind < 0 ? -_initialSpeed : _initialSpeed);
                _material.SetFloat("_Waves", _initialWaves / Mathf.Abs(Wind));
            }
        }

    }

}
