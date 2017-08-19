using Assets;
using Utils.Singleton;


namespace War.Weapons.Thrown {

    public class GrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Grenade,
                    The<WeaponIcons>.Get().Grenade
                );
            }
        }

    }

}
