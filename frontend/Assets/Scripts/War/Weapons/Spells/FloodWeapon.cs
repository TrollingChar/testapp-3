using Assets;
using Utils.Singleton;


namespace War.Weapons.Spells {

    public class FloodWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Flood,
                    The<WeaponIcons>.Get().Flood
                );
            }
        }

    }

}
