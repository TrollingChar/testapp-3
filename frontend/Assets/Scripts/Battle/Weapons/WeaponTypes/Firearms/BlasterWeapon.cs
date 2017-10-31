using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Blaster)]
    public class BlasterWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Blaster,
                    The.WeaponIcons.Blaster
                );
            }
        }


        protected override void OnEquip () {
            Attacks = 2;
//            CrossHair = new LineCrosshair();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {}

    }

}
