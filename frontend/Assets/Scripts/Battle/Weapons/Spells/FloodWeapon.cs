using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Spells {

    [Weapon(WeaponId.Flood)]
    public class FloodWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Flood,
                    The<WeaponIcons>.Get().Flood
                );
            }
        }

    }

}
