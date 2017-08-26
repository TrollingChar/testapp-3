using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    public class PoisonArrowWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonArrow,
                    The<WeaponIcons>.Get().PoisonArrow
                );
            }
        }

    }

}
