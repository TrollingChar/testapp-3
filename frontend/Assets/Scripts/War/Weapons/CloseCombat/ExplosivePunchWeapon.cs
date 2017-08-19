using Assets;
using Utils.Singleton;


namespace War.Weapons.CloseCombat {

    public class ExplosivePunchWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.ExplosivePunch,
                    The<WeaponIcons>.Get().ExplosivePunch
                );
            }
        }


    }

}
