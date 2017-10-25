using UnityEngine;


namespace Battle.Water {

    public class WaterGO : MonoBehaviour {
        private float _wind;

        [SerializeField] private GameObject _staticWaterPrefab;
        [SerializeField] private GameObject _frontWavesPrefab;
        [SerializeField] private GameObject _backWavesPrefab;

        [SerializeField] private float[] _backWavesOffsets;
        [SerializeField] private float[] _frontWavesOffsets;
        
        private SpriteRenderer _waves;

        // ширина спрайта воды
        public int Width { get; set; }

        // сила ветра по оси X
        public float Wind
        {
            get { return _wind; }
            set
            {
                if (_wind == value) return;
                _wind = value;
                // if (_waves != null) _waves.scaleY `set to` 0;
                // _waves = Instantiate(_wavesPrefab);
                // _waves.size = 0; 
                // _waves.scaleY `set to` 1;
            }
        }

        private void MakeWaves(GameObject prefab, float yOffset, float phase, int layer) {
            var waves = Instantiate(prefab, transform);
            var position = waves.transform.localPosition;
            position.y = yOffset;
            waves.transform.position = position;
            var spriteRenderer = waves.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = layer;
            var material = spriteRenderer.material;
            material.SetFloat("_Offset", phase);
            material.SetFloat("_Wind", 0.03f);
        }

        private void Awake() {
            bool b = false;
            int layer = 0;
            foreach (float y in _backWavesOffsets)  MakeWaves(_backWavesPrefab,  y, (b = !b) ? 0 : 0.5f, ++layer);
            foreach (float y in _frontWavesOffsets) MakeWaves(_frontWavesPrefab, y, (b = !b) ? 0 : 0.5f, ++layer);
        }
    }


    public class WavesGO : MonoBehaviour
    {
        // навешивается на каждый спрайт с волнами
        // на первое время пусть пока будет постоянная ширина спрайта, высота волны и скорость ветра

        private void Awake()
        {
            var material = GetComponent<SpriteRenderer>().material;
//            material.SetFloat("_Waves", 100);
            material.SetFloat("_Wind", 0.1f);
        }
    }

}
