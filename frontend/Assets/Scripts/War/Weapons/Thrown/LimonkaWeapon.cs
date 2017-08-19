using Assets;
using Utils.Singleton;


namespace War.Weapons.Thrown {

    public class LimonkaWeapon : StandardWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Limonka,
                    The<WeaponIcons>.Get().Limonka
                );
            }
        }

    }

}
