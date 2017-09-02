using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    [Weapon(WeaponId.Grenade)]
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
