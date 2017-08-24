using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    public class MachineGunWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.MachineGun,
                    The<WeaponIcons>.Get().MachineGun
                );
            }
        }

    }

}
