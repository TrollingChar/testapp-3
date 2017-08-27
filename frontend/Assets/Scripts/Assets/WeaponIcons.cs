using UnityEngine;
using Utils.Singleton;


namespace Assets {

    public class WeaponIcons : MonoBehaviour {

        public GameObject
            Bazooka, Plasmagun, HomingMissile, MultiLauncher, Minegun, Cryogun, BirdLauncher,
            Grenade, Limonka, Molotov, GasGrenade, ControlledGrenade, PhantomGrenade, HolyGrenade,
            MachineGun, Blaster, Pistol, HeatPistol, PoisonArrow, UltraRifle, GsomRaycaster,
            FirePunch, ExplosivePunch, Finger, Axe, Hammer, Flamethrower, Harpoon,
            Landmine, Dynamite, PoisonContainer, Turret, Frog, Mole, Superfrog,
            Airstrike, Napalm, DropMole, DropFrog, MineStrike, VacuumBomb, Nuke,
            Erosion, PoisonCloud, Earthquake, Flood, BulletHell, MindControl, Armageddon,
            Rope, Jetpack, Parachute, Jumper, Teleport, MassTeleport, WormSelect,
            Drill, Girder, Magnet, Medikit, Overheal, SkipTurn, Surrender;
            


        private void Awake () {
            The<WeaponIcons>.Set(this);
        }

    }

}
