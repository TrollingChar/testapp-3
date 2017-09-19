using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.FirePunch)]
    public class FirePunchWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.FirePunch,
                    The<WeaponIcons>.Get().FirePunch
                );
            }
        }

    }

}
