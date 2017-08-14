using UnityEngine.SceneManagement;
using Utils.Singleton;


namespace Scenes {

    public class SceneSwitcher {

        public SceneSwitcher () {
            The<SceneSwitcher>.Set(this);
        }


        public object[] Data { get; private set; }


        public void Load (string scene, params object[] data) {
            Data = data;
            SceneManager.LoadScene(scene);
        }

    }

}
