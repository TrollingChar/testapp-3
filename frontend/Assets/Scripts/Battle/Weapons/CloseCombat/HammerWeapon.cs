using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.CloseCombat {

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

    }

}
