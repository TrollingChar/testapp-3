using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    [Weapon(WeaponId.GasGrenade)]
    public class GasGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.GasGrenade,
                    The<WeaponIcons>.Get().GasGrenade
                );
            }
        }

    }

}
