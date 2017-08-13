using UnityEngine.SceneManagement;
using Utils.Singleton;


namespace Scenes {

    public class SceneSwitcher {

        public object[] Data { get; private set; }


        public SceneSwitcher () {
            The<SceneSwitcher>.Set(this);
        }
        

        public void Load (string scene, params object[] data) {
            Data = data;
            SceneManager.LoadScene(scene);
        }

    }

}
