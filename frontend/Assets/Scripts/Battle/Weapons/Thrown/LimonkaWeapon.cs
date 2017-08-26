using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

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
