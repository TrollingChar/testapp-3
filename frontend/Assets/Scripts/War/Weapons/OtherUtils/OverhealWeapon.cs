using Assets;
using Utils.Singleton;


namespace War.Weapons.OtherUtils {

    public class OverhealWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Overheal,
                    The<WeaponIcons>.Get().Overheal
                );
            }
        }

    }

}
