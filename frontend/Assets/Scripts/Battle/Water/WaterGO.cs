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
    }


    public class WavesGO : MonoBehaviour
    {
        // навешивается на каждый спрайт с волнами
        // на первое время пусть пока будет постоянная ширина спрайта, высота волны и скорость ветра

        private void Awake()
        {
            var material = GetComponent<SpriteRenderer>().material;
            material.SetFloat("_Waves", 100);
        }
    }

}
