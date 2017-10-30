using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.BirdLauncher)]
    public class BirdLauncherWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.BirdLauncher,
                    The<WeaponIcons>.Get().BirdLauncher
                );
            }
        }

    }

}
