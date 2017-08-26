using Battle.Arsenal;
using Battle.Weapons;
using Battle.Weapons.Airstrikes;
using Battle.Weapons.CloseCombat;
using Battle.Weapons.Firearms;
using Battle.Weapons.Heavy;
using Battle.Weapons.Launched;
using Battle.Weapons.MovementUtils;
using Battle.Weapons.OtherUtils;
using Battle.Weapons.Spells;
using Battle.Weapons.Thrown;
using Core.UI;
using UnityEngine;


namespace Battle.UI {

    public class ArsenalPanel : Panel {

        [SerializeField] private GameObject _weaponButton;

        private void Start () {
            AddWeapon(BazookaWeapon.Descriptor);
            AddEmpty(2);
            AddWeapon(MultiLauncherWeapon.Descriptor);
            AddEmpty(3);
            
            AddWeapon(GrenadeWeapon.Descriptor);
            AddWeapon(LimonkaWeapon.Descriptor);
            AddWeapon(MolotovWeapon.Descriptor);
            AddWeapon(GasGrenadeWeapon.Descriptor);
            AddWeapon(ControlledGrenadeWeapon.Descriptor);
            AddEmpty();
            AddWeapon(HolyGrenadeWeapon.Descriptor);
            
            AddWeapon(MachineGunWeapon.Descriptor);
            AddEmpty();
            AddWeapon(PistolWeapon.Descriptor);
            AddWeapon(HeatPistolWeapon.Descriptor);
            AddWeapon(PoisonArrowWeapon.Descriptor);
            AddWeapon(UltraRifleWeapon.Descriptor);
            AddEmpty();
            
            AddWeapon(FirePunchWeapon.Descriptor);
            AddWeapon(ExplosivePunchWeapon.Descriptor);
            AddWeapon(FingerWeapon.Descriptor);
            AddEmpty();
            AddWeapon(HammerWeapon.Descriptor);
            AddEmpty(2);
            
            AddWeapon(LandmineWeapon.Descriptor);
            AddWeapon(DynamiteWeapon.Descriptor);
            AddEmpty(5);
            
            AddWeapon(AirstrikeWeapon.Descriptor);
            AddEmpty(3);
            AddWeapon(MineStrikeWeapon.Descriptor);
            AddEmpty(2);
            
            AddEmpty(3);
            AddWeapon(FloodWeapon.Descriptor);
            AddEmpty(3);
            
            AddWeapon(RopeWeapon.Descriptor);
            AddEmpty();
            AddWeapon(ParachuteWeapon.Descriptor);
            AddWeapon(JumperWeapon.Descriptor);
            AddWeapon(TeleportWeapon.Descriptor);
            AddEmpty();
            AddWeapon(WormSelectWeapon.Descriptor);
            
            AddEmpty();
            AddWeapon(GirderWeapon.Descriptor);
            AddWeapon(MagnetWeapon.Descriptor);
            AddEmpty();
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
