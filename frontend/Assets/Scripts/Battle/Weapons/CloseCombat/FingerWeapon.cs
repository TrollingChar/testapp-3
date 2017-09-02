using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.CloseCombat {

    [Weapon(WeaponId.Finger)]
    public class FingerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Finger,
                    The<WeaponIcons>.Get().Finger
                );
            }
        }

    }

}
