using Assets;
using Utils.Singleton;


namespace War.Weapons {

    public class BazookaWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get { return new WeaponDescriptor(1, The<WeaponIcons>.Get().Bazooka); }
        }

    }

}
