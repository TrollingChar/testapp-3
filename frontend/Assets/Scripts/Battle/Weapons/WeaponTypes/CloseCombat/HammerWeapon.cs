using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Hammer)]
    public class HammerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Hammer,
                    The<WeaponIcons>.Get().Hammer
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
