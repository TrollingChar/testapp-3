using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.ExplosivePunch)]
    public class ExplosivePunchWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.ExplosivePunch,
                    The<WeaponIcons>.Get().ExplosivePunch
                );
            }
        }

    }

}
