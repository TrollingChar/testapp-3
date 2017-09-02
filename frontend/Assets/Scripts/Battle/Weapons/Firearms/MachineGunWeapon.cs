using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    [Weapon(WeaponId.MachineGun)]
    public class MachineGunWeapon : StandardWeapon {

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
