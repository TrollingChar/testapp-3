using UnityEngine;


namespace Assets {

    public class AssetsLoader : MonoBehaviour {

        [SerializeField] private GameObject
            // asset1,
            // asset2,
            worm;

        [SerializeField] private Texture2D
            motherboard;


        private void Awake () {
            Assets.worm = worm;
            Assets.motherboard = motherboard;
            Destroy(this);
        }

    }

}
