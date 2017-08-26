using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Spells {

    public class FloodWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.Flood,
                    The<WeaponIcons>.Get().Flood
                );
            }
        }

    }

}
