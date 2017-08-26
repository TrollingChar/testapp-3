using Assets;
using Utils.Singleton;


namespace Battle.Weapons.CloseCombat {

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
