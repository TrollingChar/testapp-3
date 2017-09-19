using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.MultiLauncher)]
    public class MultiLauncherWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MultiLauncher,
                    The<WeaponIcons>.Get().MultiLauncher
                );
            }
        }

    }

}
