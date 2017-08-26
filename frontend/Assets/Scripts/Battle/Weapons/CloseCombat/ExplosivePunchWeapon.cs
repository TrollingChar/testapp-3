using Assets;
using Utils.Singleton;


namespace Battle.Weapons.CloseCombat {

    public class ExplosivePunchWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.ExplosivePunch,
                    The<WeaponIcons>.Get().ExplosivePunch
                );
            }
        }


    }

}
