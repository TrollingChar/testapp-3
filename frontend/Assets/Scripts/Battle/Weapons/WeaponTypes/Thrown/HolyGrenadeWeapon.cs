using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.HolyGrenade)]
    public class HolyGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HolyGrenade,
                    The.WeaponIcons.HolyGrenade
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new LineCrosshair();
        }


        protected override void OnShoot () {}

    }

}
