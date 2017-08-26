using Assets;
using Utils.Singleton;


namespace Battle.Weapons.CloseCombat {

    public class FirePunchWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.FirePunch,
                    The<WeaponIcons>.Get().FirePunch
                );
            }
        }

    }

}
