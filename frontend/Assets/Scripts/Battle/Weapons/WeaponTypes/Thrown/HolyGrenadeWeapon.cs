using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.HolyGrenade)]
    public class HolyGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HolyGrenade,
                    The<WeaponIcons>.Get().HolyGrenade
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
