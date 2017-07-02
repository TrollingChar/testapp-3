using UnityEngine;


namespace Assets {

    public class AssetsLoader : MonoBehaviour {

        [SerializeField] private GameObject
            // asset1,
            // asset2,
            _worm;

        [SerializeField] private Texture2D
            _motherboard;


        private void Awake () {
            Assets.Worm = _worm;
            Assets.Motherboard = _motherboard;
            Destroy(this);
        }

    }

}
