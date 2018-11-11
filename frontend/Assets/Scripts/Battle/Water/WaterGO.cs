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


}
