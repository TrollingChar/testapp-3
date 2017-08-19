using Assets;
using Utils.Singleton;


namespace War.Weapons.Launched {

    public class MultiLauncherWeapon : StandardWeapon {


        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.MultiLauncher,
                    The<WeaponIcons>.Get().MultiLauncher
                );
            }
        }
        

    }

}
