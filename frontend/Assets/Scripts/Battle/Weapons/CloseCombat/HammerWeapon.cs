using Assets;
using Utils.Singleton;


namespace Battle.Weapons.CloseCombat {

    public class HammerWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Hammer,
                    The<WeaponIcons>.Get().Hammer
                );
            }
        }


    }

}
