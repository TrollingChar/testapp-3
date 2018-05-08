using Core;
using UnityEngine;


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
        public GameObject MeleeCrosshair;
        public GameObject PointCrosshair;
        [Space]
        public GameObject Worm;
        public GameObject Arrow;
        [Space]
        public GameObject WoodenCrate;
        public GameObject HealthCrate;
        [Space]
        public GameObject BazookaWeapon;
        public GameObject PlasmagunWeapon;
        public GameObject HomingMissileWeapon;
        public GameObject MultiLauncherWeapon;
        public GameObject MinegunWeapon;
        public GameObject GrenadeWeapon;
        public GameObject LimonkaWeapon;
        public GameObject PhantomGrenadeWeapon;
        public GameObject MachineGunWeapon;
        public GameObject BlasterWeapon;
        public GameObject PistolWeapon;
        public GameObject HeatPistolWeapon;
        public GameObject CrossbowWeapon;
        public GameObject UltraRifleWeapon;
        public GameObject GsomRaycasterWeapon;
        public GameObject FingerWeapon;
        public GameObject AxeWeapon;
        public GameObject HammerWeapon;
        public GameObject LandmineWeapon;
        public GameObject DynamiteWeapon;
        public GameObject MedikitWeapon;
        [Space]
        public GameObject BazookaShell;
        public GameObject PlasmaBall;
        public GameObject HomingMissile;
        public GameObject MultiLauncherShell;
        public GameObject Grenade;
        public GameObject Limonka;
        public GameObject LimonkaCluster;
        public GameObject PhantomGrenade;
        public GameObject Landmine;
        public GameObject Dynamite;
        public GameObject Meteor;
        [Space]
        public GameObject Smoke;
        public GameObject PoisonGas;
        public GameObject Flash;

        
        private void Awake () {
            The.BattleAssets = this;
        }

    }

}
