using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;
using War;
using War.Generation;


namespace Scenes {

    public class BattleScene : MonoBehaviour {

        [SerializeField] private Text _hint;

        private WSConnection _connection;
        private World _world;
        private GameStateController _state;
        private GameInitData _initData;
        private EstimatedLandGen _landGen;

        private bool _initialized;


        private void Awake () {
            _initData = (GameInitData) The<SceneSwitcher>.Get().Data[0];
            _connection = The<WSConnection>.Get();

//            The<World>.Set(_world = new World());
//            The<GameStateController>.Set(_state = new GameStateController());

            _landGen = new EstimatedLandGen(
                    new LandGen(
                        new byte[,] {
                            {0, 0, 0, 0, 0},
                            {0, 1, 1, 1, 0},
                            {0, 1, 0, 1, 0}
                        }
                    )
                ).SwitchDimensions()
                .Expand(7)
                .Cellular(0x01e801d0, 20)
                .Cellular(0x01f001e0)
                .Expand()
                .Cellular(0x01e801d0, 20)
                .Cellular(0x01f001e0)
                .Rescale(2000, 1000)
                .Cellular(0x01f001e0);
            _landGen.OnProgress.Subscribe(OnProgress);
            _landGen.OnComplete.Subscribe(OnComplete); // maybe the world should handle this?
            _landGen.Generate(this);
        }


        private void OnProgress (float progress) {
            _hint.text = "Прогресс генерации: " + (int) progress + "%";
        }


        private void OnComplete (LandGen gen) {
            _initialized = true;
            
            _landGen.OnProgress.Unsubscribe(OnProgress);
            _landGen.OnComplete.Unsubscribe(OnComplete);
            
            // show ground, spawn worms
            Debug.Log("done");
        }


        private void FixedUpdate () {
            if (!_initialized) return;
        }


        private void OnDestroy () {
            The<World>.Set(null);
            The<GameStateController>.Set(null);
        }

    }

}
