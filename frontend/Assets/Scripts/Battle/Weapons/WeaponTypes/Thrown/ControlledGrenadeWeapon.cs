using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.ControlledGrenade)]
    public class ControlledGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.ControlledGrenade,
                    The<WeaponIcons>.Get().ControlledGrenade
                );
            }
        }

        protected override void OnEquip()
        {
            CrossHair = new LineCrosshair();
        }

        protected override void OnShoot()
        {
        }

    }

}
