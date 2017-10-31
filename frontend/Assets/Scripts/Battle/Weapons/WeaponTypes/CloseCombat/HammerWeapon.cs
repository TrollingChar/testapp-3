using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Hammer)]
    public class HammerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Hammer,
                    The.WeaponIcons.Hammer
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new LineCrosshair();
        }


        protected override void OnShoot () {}

    }

}
