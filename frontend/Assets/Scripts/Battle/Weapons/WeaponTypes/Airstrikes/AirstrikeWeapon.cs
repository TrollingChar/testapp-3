using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.Airstrike)]
    public class AirstrikeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Airstrike,
                    The<WeaponIcons>.Get().Airstrike
                );
            }
        }

        protected override void OnEquip()
        {
            CrossHair = new AirstrikeCrosshair();
        }

        protected override void OnShoot()
        {
        }
    }

}
