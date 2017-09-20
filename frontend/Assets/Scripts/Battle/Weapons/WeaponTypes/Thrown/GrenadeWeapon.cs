using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.Grenade)]
    public class GrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Grenade,
                    The<WeaponIcons>.Get().Grenade
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
