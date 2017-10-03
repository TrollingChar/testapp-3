using UnityEngine;
using Utils.Singleton;


namespace Assets {

    public class BattleAssets : MonoBehaviour {

        public GameObject Text;
        
        [Space]
        public GameObject TopCanvas;
        public GameObject CenterCanvas;
        public GameObject BottomCanvas;
        
        [Space]
        public Texture2D LandTexture;

        [Space]
        public GameObject Worm;
        public GameObject Arrow;
        public GameObject NameField;
        public GameObject HPField;


        private void Awake () {
            The<BattleAssets>.Set(this);
        }

    }

}
