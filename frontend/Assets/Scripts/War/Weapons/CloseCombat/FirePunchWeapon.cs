using Assets;
using Utils.Singleton;


namespace War.Weapons.CloseCombat {

    public class FirePunchWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.FirePunch,
                    The<WeaponIcons>.Get().FirePunch
                );
            }
        }

    }

}
