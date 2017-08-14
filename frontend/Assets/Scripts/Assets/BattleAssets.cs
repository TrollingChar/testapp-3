using UnityEngine;
using Utils.Singleton;


namespace Assets {

    public class BattleAssets : MonoBehaviour {

        public Texture2D LandTexture;

        public GameObject Worm;


        private void Awake () {
            The<BattleAssets>.Set(this);
        }

    }

}
