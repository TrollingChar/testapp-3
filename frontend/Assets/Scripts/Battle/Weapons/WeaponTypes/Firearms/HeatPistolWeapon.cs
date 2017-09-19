using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.HeatPistol)]
    public class HeatPistolWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HeatPistol,
                    The<WeaponIcons>.Get().HeatPistol
                );
            }
        }

    }

}
