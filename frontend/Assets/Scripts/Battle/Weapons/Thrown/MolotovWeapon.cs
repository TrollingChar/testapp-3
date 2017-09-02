using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    [Weapon(WeaponId.Molotov)]
    public class MolotovWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Molotov,
                    The<WeaponIcons>.Get().Molotov
                );
            }
        }

    }

}
