using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    [Weapon(WeaponId.Limonka)]
    public class LimonkaWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Limonka,
                    The<WeaponIcons>.Get().Limonka
                );
            }
        }

    }

}
