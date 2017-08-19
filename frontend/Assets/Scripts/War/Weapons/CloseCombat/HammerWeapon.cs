using Assets;
using Utils.Singleton;


namespace War.Weapons.CloseCombat {

    public class HammerWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Hammer,
                    The<WeaponIcons>.Get().Hammer
                );
            }
        }


    }

}
