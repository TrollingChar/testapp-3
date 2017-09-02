using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    [Weapon(WeaponId.HolyGrenade)]
    public class HolyGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HolyGrenade,
                    The<WeaponIcons>.Get().HolyGrenade
                );
            }
        }

    }

}
