using UnityEngine.SceneManagement;


public class SceneSwitcher {

    public object[] Data { get; private set; }


    public void Load (string scene, params object[] data) {
        Data = data;
        SceneManager.LoadScene(scene);
    }

}
