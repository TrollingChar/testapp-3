using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    public class MachineGunWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MachineGun,
                    The<WeaponIcons>.Get().MachineGun
                );
            }
        }

    }

}
