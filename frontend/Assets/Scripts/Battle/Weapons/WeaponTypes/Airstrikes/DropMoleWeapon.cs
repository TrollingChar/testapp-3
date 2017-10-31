using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.DropMole)]
    public class DropMoleWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.DropMole,
                    The.WeaponIcons.DropMole
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new AirstrikeCrosshair();
        }


        protected override void OnShoot () {}

    }

}
