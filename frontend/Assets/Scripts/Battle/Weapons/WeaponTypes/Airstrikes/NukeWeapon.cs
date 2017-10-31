using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.Nuke)]
    public class NukeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Nuke,
                    The.WeaponIcons.Nuke
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new AirstrikeCrosshair();
        }


        protected override void OnShoot () {}

    }

}
