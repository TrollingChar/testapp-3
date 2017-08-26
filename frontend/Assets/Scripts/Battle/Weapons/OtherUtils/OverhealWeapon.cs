using Assets;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    public class OverhealWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Overheal,
                    The<WeaponIcons>.Get().Overheal
                );
            }
        }

    }

}
