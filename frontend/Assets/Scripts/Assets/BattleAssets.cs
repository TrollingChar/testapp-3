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
        public GameObject LineCrosshair;
        public GameObject PointCrosshair;
        [Space]
        public GameObject Worm;
        public GameObject Arrow;
//        public GameObject NameField;
//        public GameObject HPField;
        [Space]
        public GameObject BazookaWeapon;
        public GameObject MultiLauncherWeapon;
        public GameObject GrenadeWeapon;
        public GameObject LimonkaWeapon;
        public GameObject PhantomGrenadeWeapon;
        [Space]
        public GameObject BazookaShell;
        public GameObject MultiLauncherShell;
        public GameObject Grenade;
        public GameObject Limonka;
        public GameObject LimonkaCluster;
        public GameObject PhantomGrenade;

        private void Awake () {
            The<BattleAssets>.Set(this);
        }

    }

}
