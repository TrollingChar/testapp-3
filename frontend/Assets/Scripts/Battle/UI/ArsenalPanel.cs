using Battle.Weapons;
using Battle.Weapons.WeaponTypes.Airstrikes;
using Battle.Weapons.WeaponTypes.CloseCombat;
using Battle.Weapons.WeaponTypes.Firearms;
using Battle.Weapons.WeaponTypes.Heavy;
using Battle.Weapons.WeaponTypes.Launched;
using Battle.Weapons.WeaponTypes.MovementUtils;
using Battle.Weapons.WeaponTypes.OtherUtils;
using Battle.Weapons.WeaponTypes.Spells;
using Battle.Weapons.WeaponTypes.Thrown;
using Core.UI;
using UnityEngine;


namespace Battle.UI {

    public class ArsenalPanel : Panel {

        [SerializeField] private GameObject _weaponButton;


        private void Start () {
            AddWeapon(BazookaWeapon.Descriptor);
            AddWeapon(PlasmagunWeapon.Descriptor);
            AddWeapon(HomingMissileWeapon.Descriptor);
            AddWeapon(MultiLauncherWeapon.Descriptor);
            AddWeapon(MinegunWeapon.Descriptor);
            AddWeapon(CryogunWeapon.Descriptor);
            AddWeapon(BirdLauncherWeapon.Descriptor);

            AddWeapon(GrenadeWeapon.Descriptor);
            AddWeapon(LimonkaWeapon.Descriptor);
            AddWeapon(MolotovWeapon.Descriptor);
            AddWeapon(GasGrenadeWeapon.Descriptor);
            AddWeapon(ControlledGrenadeWeapon.Descriptor);
            AddWeapon(PhantomGrenadeWeapon.Descriptor);
            AddWeapon(HolyGrenadeWeapon.Descriptor);

            AddWeapon(MachineGunWeapon.Descriptor);
            AddWeapon(BlasterWeapon.Descriptor);
            AddWeapon(PistolWeapon.Descriptor);
            AddWeapon(HeatPistolWeapon.Descriptor);
            AddWeapon(PoisonArrowWeapon.Descriptor);
            AddWeapon(UltraRifleWeapon.Descriptor);
            AddWeapon(GsomRaycasterWeapon.Descriptor);

            AddWeapon(FirePunchWeapon.Descriptor);
            AddWeapon(ExplosivePunchWeapon.Descriptor);
            AddWeapon(FingerWeapon.Descriptor);
            AddWeapon(AxeWeapon.Descriptor);
            AddWeapon(HammerWeapon.Descriptor);
            AddWeapon(FlamethrowerWeapon.Descriptor);
            AddWeapon(HarpoonWeapon.Descriptor);

            AddWeapon(LandmineWeapon.Descriptor);
            AddWeapon(DynamiteWeapon.Descriptor);
            AddWeapon(PoisonContainerWeapon.Descriptor);
            AddWeapon(TurretWeapon.Descriptor);
            AddWeapon(FrogWeapon.Descriptor);
            AddWeapon(MoleWeapon.Descriptor);
            AddWeapon(SuperfrogWeapon.Descriptor);

            AddWeapon(AirstrikeWeapon.Descriptor);
            AddWeapon(NapalmWeapon.Descriptor);
            AddWeapon(DropMoleWeapon.Descriptor);
            AddWeapon(DropFrogWeapon.Descriptor);
            AddWeapon(MineStrikeWeapon.Descriptor);
            AddWeapon(VacuumBombWeapon.Descriptor);
            AddWeapon(NukeWeapon.Descriptor);

            AddWeapon(ErosionWeapon.Descriptor);
            AddWeapon(PoisonCloudWeapon.Descriptor);
            AddWeapon(EarthquakeWeapon.Descriptor);
            AddWeapon(FloodWeapon.Descriptor);
            AddWeapon(BulletHellWeapon.Descriptor);
            AddWeapon(MindControlWeapon.Descriptor);
            AddWeapon(ArmageddonWeapon.Descriptor);

            AddWeapon(RopeWeapon.Descriptor);
            AddWeapon(JetpackWeapon.Descriptor);
            AddWeapon(ParachuteWeapon.Descriptor);
            AddWeapon(JumperWeapon.Descriptor);
            AddWeapon(TeleportWeapon.Descriptor);
            AddWeapon(MassTeleportWeapon.Descriptor);
            AddWeapon(WormSelectWeapon.Descriptor);

            AddWeapon(DrillWeapon.Descriptor);
            AddWeapon(GirderWeapon.Descriptor);
            AddWeapon(MagnetWeapon.Descriptor);
            AddWeapon(MedikitWeapon.Descriptor);
            AddWeapon(OverhealWeapon.Descriptor);
            AddWeapon(SkipTurnWeapon.Descriptor);
            AddWeapon(SurrenderWeapon.Descriptor);
        }


        private void Update () {
            if (Input.GetKeyDown(KeyCode.Q)) Toggle();
        }


        private void AddWeapon (WeaponDescriptor descriptor) {
            var button = Instantiate(_weaponButton, gameObject.transform, false);
            var component = button.GetComponent<WeaponButton>();
            component.Configure(descriptor);
            button.transform.SetAsLastSibling();
        }


        private void AddEmpty (int count = 1) {
            for (int i = 0; i < count; i++) {
                var button = Instantiate(_weaponButton, gameObject.transform, false);
                button.transform.SetAsLastSibling();
//                button.GetComponent<Button>().interactable = false;
            }
        }

    }

}
