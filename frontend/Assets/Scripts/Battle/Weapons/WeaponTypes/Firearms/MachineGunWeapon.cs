using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.MachineGun)]
    public class MachineGunWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MachineGun,
                    The.WeaponIcons.MachineGun
                );
            }
        }


        protected override void OnEquip () {
            Shots = 30;
//            CrossHair = new LineCrosshair();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {}

    }

}
