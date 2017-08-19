using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using War.Weapons;
using War.Weapons.Airstrikes;
using War.Weapons.CloseCombat;
using War.Weapons.Firearms;
using War.Weapons.Heavy;
using War.Weapons.Launched;
using War.Weapons.MovementUtils;
using War.Weapons.OtherUtils;
using War.Weapons.Spells;
using War.Weapons.Thrown;


namespace UI.Panels {

    public class ArsenalPanel : MonoBehaviour {

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


        private void AddWeapon (WeaponDescriptor descriptor) {
            var button = Instantiate(_weaponButton, gameObject.transform, false);
            var component = button.GetComponent<WeaponButton>();
            
            component.SetImage(descriptor.Icon);
            component.SetAmmo(1);
            
            button.transform.SetAsLastSibling();
        }


        private void AddEmpty (int count = 1) {
            for (int i = 0; i < count; i++) {
                var button = Instantiate(_weaponButton, gameObject.transform, false);
                var component = button.GetComponent<WeaponButton>();
                component.SetAmmo(0);
                button.transform.SetAsLastSibling();
            }
        }

    }

}
