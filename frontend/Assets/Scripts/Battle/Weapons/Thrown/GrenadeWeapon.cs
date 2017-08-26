using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    public class GrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Grenade,
                    The<WeaponIcons>.Get().Grenade
                );
            }
        }

    }

}
