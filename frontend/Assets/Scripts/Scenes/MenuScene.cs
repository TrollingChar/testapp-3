using Net;
using UnityEngine;
using Utils.Singleton;
using War;


namespace Scenes {

    public class MenuScene : MonoBehaviour {

        private void Awake () {
            The<WSConnection>.Get().OnStartGame.Subscribe(StartGame);
        }


        private void StartGame (GameInitData gameInitData) {
//            SceneSwitcher
        }


        private void OnDestroy () {
            The<WSConnection>.Get().OnStartGame.Unsubscribe(StartGame);
        }

    }

}
