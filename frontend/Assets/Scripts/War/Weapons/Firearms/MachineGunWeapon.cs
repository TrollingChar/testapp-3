using Assets;
using Utils.Singleton;


namespace War.Weapons.Firearms {

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
