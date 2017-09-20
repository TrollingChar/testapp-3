using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.GasGrenade)]
    public class GasGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.GasGrenade,
                    The<WeaponIcons>.Get().GasGrenade
                );
            }
        }


        protected override void OnEquip () {
            CrossHair = new LineCrosshair();
        }


        protected override void OnShoot () {}

    }

}
