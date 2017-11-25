using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.BirdLauncher)]
    public class BirdLauncherWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.BirdLauncher,
                    The.WeaponIcons.BirdLauncher
                );
            }
        }

    }

}
