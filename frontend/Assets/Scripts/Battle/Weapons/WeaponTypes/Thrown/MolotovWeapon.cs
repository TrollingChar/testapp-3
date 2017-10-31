using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.Molotov)]
    public class MolotovWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Molotov,
                    The.WeaponIcons.Molotov
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new LineCrosshair();
        }


        protected override void OnShoot () {}

    }

}
