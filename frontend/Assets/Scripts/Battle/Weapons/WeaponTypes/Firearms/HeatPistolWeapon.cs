using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.HeatPistol)]
    public class HeatPistolWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HeatPistol,
                    The.WeaponIcons.HeatPistol
                );
            }
        }


        protected override void OnEquip () {
            Shots = 5;
//            CrossHair = new LineCrosshair();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {}

    }

}
