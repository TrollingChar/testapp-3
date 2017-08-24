using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

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
